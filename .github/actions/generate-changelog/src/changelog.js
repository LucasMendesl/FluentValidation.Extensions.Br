const Bluebird = require('bluebird')
const streamToPromise = require('stream-to-promise')
const spec = require('conventional-changelog-config-spec')
const conventionalChangelog = require('conventional-changelog')
const {
  statAsync, 
  readFileAsync, 
  writeFileAsync,
} = Bluebird.promisifyAll(require('fs'))

const configurePreset = args => {
  const defaultPreset = require.resolve('conventional-changelog-conventionalcommits')
  const presetConfig = args.preset || defaultPreset

  if (presetConfig !== defaultPreset) return presetConfig

  const preset = { name: defaultPreset }

  return Object.keys(spec.properties).reduce((acc, curr) => {
      if (args[curr]) acc[curr] = args[curr]
      return acc  
  }, preset)
}

const createFileIfNotExists = ({ changelogFile }) => {
  return statAsync(changelogFile)
    .catch(() => writeFileAsync(changelogFile, '', 'utf8'))
}

const extractOldContent = changelogFile => {
  const startContentPosition = changelogFile.search(/(^#+ \[?[0-9]+\.[0-9]+\.[0-9]+|<a name=)/m)
  
  if (startContentPosition !== -1) return changelogFile.substring(startContentPosition)
  return changelogFile
}

const mergeChangelogContent = (args, newVersion) => oldContent => {
  const newChangelogVersion = { version: newVersion }
  const conventionalChangelogConfig = {
    debug: console.info.bind(console, 'conventional-changelog'),
    preset: configurePreset(args),
    tagPrefix: args.tagPrefix
  }

  return streamToPromise(conventionalChangelog(
    conventionalChangelogConfig, 
    newChangelogVersion, 
    { merges: null, path: args.path })
  )
  .then(newContent => [newContent.toString('utf8') + oldContent, newContent.toString('utf8')])
}

const createOrUpdateChangelog = (actionContext, args) => newVersion => {
  const setOutputs = ([_, newContent]) => {
    actionContext.info('setting version and release notes outputs 🥳🥳🥳')
    actionContext.setOutput('newVersion', newVersion)
    actionContext.setOutput('tagVersion', `${args.tagPrefix}${newVersion}`)
    actionContext.setOutput('releaseNotes', newContent)
    actionContext.setOutput('releaseName', `${args.tagPrefix}${newVersion} (${new Date().toISOString().split('T').shift()})`)
  }

  return createFileIfNotExists(args)
    .then(() => readFileAsync(args.changelogFile, 'utf8'))
    .then(extractOldContent)
    .then(mergeChangelogContent(args, newVersion))
    .tap(([mergedContent]) => writeFileAsync(args.changelogFile, args.header + '\n' + mergedContent.replace(/\n+$/, '\n')))
    .tap(setOutputs)
    .then(() => newVersion)
}

module.exports = { createOrUpdateChangelog }