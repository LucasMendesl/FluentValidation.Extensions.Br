const Bluebird = require('bluebird')
const core = require('@actions/core')

const { format } = require('@commitlint/format')
const { lintCommitMessages } = require('./linter')
const { getLastCommit, getPullRequestCommits } = require('./commit')

const mapResultOutput = ({
    hash,
    lintResult: { valid, errors, warnings, input },
  }) => ({
    hash,
    message: input,
    valid,
    errors: errors.map(item => item.message),
    warnings: warnings.map(item => item.message),
})

const validateLintedResult = lintedCommits => {
    const formatedErrors = format(
        { results: lintedCommits.map(commit => commit.lintResult) },
        { color: true }
    )

    const output = lintedCommits.map(mapResultOutput)
    core.setOutput('results', JSON.stringify(output))
    
    if (output.some(item => !item.valid)) {
        core.setFailed(`You have commit messages with errors\n\n${formatedErrors}`)
    } else {
        core.info('Commit message is OK 😉🎉');
    }
}

const getCommitByEvent = client => event => ({
    push: ({ sha }) => getLastCommit(sha),
    pull_request: ({ issue }) => getPullRequestCommits(client)(issue)
})[event]

const commitValidador = (octokitClient, context) => {    
    return Bluebird.resolve(context)
        .tap(({ sha }) => core.debug(`Commit Message SHA:${sha}`))
        .then(getCommitByEvent(octokitClient)(context.eventName))
        .tap(messages => core.debug(`Commit Message Found:\n${messages}`))
        .then(lintCommitMessages)
        .then(validateLintedResult)
        .catch(error => core.setFailed(`An error ocurrs when validate message\n${error.message}\n${error.stack}`))
}

module.exports = commitValidador