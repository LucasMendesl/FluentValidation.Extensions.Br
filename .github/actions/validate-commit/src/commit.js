const Bluebird = require('bluebird')
const { promisify } = require('util')
const { exec } = require('child_process')

const execAsync = promisify(exec)

const getPullRequestCommits = client => ({ number, owner, repo }) => {
    const buildPullRequestCommitMessages = ({ data: commits }) => {
        return commits.map(({ sha, commit: { message } }) => ({ sha, message }))
    }

    const listCommitsPayload = {
        owner,
        repo,
        pull_number: number
    }

    return client.pulls.listCommits(listCommitsPayload)
        .then(buildPullRequestCommitMessages)
}

const getLastCommit = commitHash => {
    const buildLastCommitMessage = ({ stdout: stream, stderr }) => {
        if (stderr) return Bluebird.reject(stderr)
    
        const [_, message] = stream.trim().split('\n');        
        return [{ sha: commitHash, message }]
    }
    
    const gitCommand = `git rev-list --format=%B --max-count=1 ${commitHash}`
    
    return Bluebird.resolve(gitCommand)
        .then(execAsync)
        .then(buildLastCommitMessage)        
}

module.exports = { 
    getLastCommit, 
    getPullRequestCommits 
}
