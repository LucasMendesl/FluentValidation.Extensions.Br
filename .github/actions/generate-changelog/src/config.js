module.exports = Object.freeze({
    changelogFile: 'CHANGELOG.md',
    tagPrefix: 'v',        
    types: [
        { type: 'feat', section: '🎉 Features' },
        { type: 'fix',  section: '🐞 Bug Fixes' },
        { type: 'chore', hidden: true },
        { type: 'test', hidden: true },
        { type: 'docs', hidden: true },
        { type: 'build', hidden: true },
        { type: 'perf', hidden: true},
        { type: 'style', hidden: true },
        { type: 'ci', hidden: true },
        { type: 'refactor', hidden: true },
    ],
    releaseCommitMessageFormat: 'chore(release): {{currentTag}} [skip ci]',
    header: '# Changelog\n\nAll notable changes to this project will be documented in this file. See [commit-conventions](https://www.conventionalcommits.org/en/v1.0.0/#specification) for commit guidelines.\n'
})