const Bluebird = require('bluebird')

const { exec } = require('child_process')
const { 
    GITHUB_REF,
    GITHUB_REPOSITORY    
} = process.env

const execAsync = Bluebird.promisify(exec)

const execCommand = (command, options = {}) => {
    const handleExecResult = ({ stdout, stderr }) => {
        if (stderr) return Bluebird.reject(stderr)
        return Bluebird.resolve(stdout)
    }

    return execAsync(`git ${command}`, options)
        .then(handleExecResult)
}

const createTaggedCommit = ({ releaseCommitMessageFormat, tagPrefix }, actionContext) => newVersion => {
    const currentTag = `${tagPrefix}${newVersion}`
    const message = releaseCommitMessageFormat.replace(/{{currentTag}}/g, newVersion)

    return Bluebird.all([        
        execCommand(`config user.name "${actionContext.getInput('git-name')}"`),
        execCommand(`config user.email "${actionContext.getInput('git-email')}"`),
        execCommand(`remote set-url origin https://x-access-token:${actionContext.getInput('token')}@github.com/${GITHUB_REPOSITORY}.git`)
    ])
    .tap(() => actionContext.info('setup git config 👌👌'))
    .then(() => execCommand('add .'))
    .then(() => execCommand(`commit -m "${message}"`))
    .then(() => execCommand(`tag -a ${currentTag} -m "${message}"`))    
    .then(() => execCommand(`push origin ${GITHUB_REF.replace('refs/heads/', '')} --follow-tags`))
    .tap(() => actionContext.info(`changelog and tags pushed with success 🙏🙏`))
}

const getReleaseCommits = client => ({ sha, issue }) => {    
    const extractTagsPayload = {
        owner: issue.owner,
        repo: issue.repo
    }
    
    const getCommitsByLastTag = ({ data: [{ commit }] }) => {
        return client.repos.compareCommits({ base: commit.sha, head: sha, ...extractTagsPayload })
        .then(({ data: { commits } }) => commits.map(({ sha, commit }) => ({ sha, message: commit.message })))
    }

    return Bluebird.resolve(extractTagsPayload)
        .then(client.repos.listTags)
        .then(getCommitsByLastTag)
}

module.exports = { 
    createTaggedCommit,
    getReleaseCommits 
}