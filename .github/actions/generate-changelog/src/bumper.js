const glob = require('glob')
const semver = require('semver')
const Bluebird = require('bluebird')

const { load } = require('cheerio')
const { resolve } = require('path')
const { readFile, writeFile } = require('fs')

const globAsync  = Bluebird.promisify(glob)
const readAsync  = Bluebird.promisify(readFile)
const writeAsync = Bluebird.promisify(writeFile)

const getReleaseType = commits => {
  const isMajor = commits.some(({ message }) => {
      const [type] = message.match(/[a-zA-Z]+/i)
      return type.includes('!') || message.includes('BREAKING CHANGE')
  })

  const isMinor = commits.some(({ message }) => {
    const [type] = message.match(/[a-zA-Z]+/i)
    return type.includes('feat')
  })

  return isMajor ? 'major' : 
         isMinor ? 'minor' : 
         'patch'  
}

const applyNewVersion = (commits, context) => content => {
    const $ = load(content.fileContent, {
        xmlMode: true,
        decodeEntities: false
    })

    const releaseType = getReleaseType(commits)    
    const version =  $('PropertyGroup > Version').text()
    const newVersion = semver.inc(version, releaseType)
    const buildVersion = String(context.runId).substring(0, 4)

    $('PropertyGroup > Version').text(newVersion)
    $('PropertyGroup > PackageVersion').text(newVersion)    
    $('PropertyGroup > FileVersion').text(`${newVersion}.${buildVersion}`)
    $('PropertyGroup > AssemblyVersion').text(`${newVersion}.${buildVersion}`)
    
    return { ...content, fileContent: $.xml(), newVersion }
}

const readFiles = file => {
    return readAsync(file, { encoding: 'utf8' })
        .then(content => ({ path: file, fileContent: content }))
}

const bump = ({ globSearcherExpression, commits, context }) => {   
    return globAsync(resolve(process.cwd(), globSearcherExpression))
        .then(files => Bluebird.all(files.map(readFiles)))
        .then(contents => contents.map(applyNewVersion(commits, context)))
        .tap(contents => Bluebird.all(contents.map(item => writeAsync(item.path, item.fileContent))))
        .then(([{ newVersion }]) => newVersion)
}

module.exports = { bump }