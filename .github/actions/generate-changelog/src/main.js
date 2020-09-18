const config = require('./config')

const { bump } = require('./bumper')
const { createOrUpdateChangelog } = require('./changelog')
const {
    createTaggedCommit,
    getReleaseCommits
} = require('./git')
const Bluebird = require('bluebird')

const buildBumpArgs = (context, action) => commits => ({
    commits,
    context,
    globSearcherExpression: action.getInput('csproj-searcher')
})

const generateChangelog = (context, action, client) => {
    return Bluebird.resolve(context)
        .then(getReleaseCommits(client))
        .tap(() => action.info('PR commits retrived, bump file version 😁😁😁'))
        .then(buildBumpArgs(context, action))
        .then(bump)
        .tap(newVersion => action.info(`bump version to ${newVersion} 😜😜😜`))
        .then(createOrUpdateChangelog(action, config))
        .tap(() => action.info(`changelog file generated, prepare push 😘😘😘`))
        .tap(createTaggedCommit(config, action))
        .then(newVersion => action.info(`Changelog generated with success to version ${newVersion} 🎉🎉🎉`))
        .catch(error => action.setFailed(`An error ocurrs when generate changelog\n${error.message}\n${error.stack} 😭😭😭`))
}

module.exports = generateChangelog