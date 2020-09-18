const Bluebird = require('bluebird') 

const { resolve } = require('path')
const { default: lint } = require('@commitlint/lint')
const { default: load } = require('@commitlint/load')

const buildConfigOptions = ({ parserPreset, plugins, ignores, defaultIgnores }) => ({
    parserOpts: parserPreset != null && parserPreset.parserOpts != null ? parserPreset.parserOpts : {},
    plugins: plugins != null ? plugins : {},
    ignores: ignores != null ? ignores : [],
    defaultIgnores: defaultIgnores != null ? defaultIgnores : true,
})

const lintCommitMessages = commitCollection => {    
    const applyLinter = config => commit => {
        return lint(commit.message, config.rules, buildConfigOptions(config))
            .then(lintResult => ({ lintResult, hash: commit.sha }))
    }

    return Bluebird.resolve([{}, { file: resolve('.github/actions/validate-commit/commitlint.config.js') }])
        .spread(load)
        .then(config => Bluebird.all(commitCollection.map(applyLinter(config))))
}

module.exports = { lintCommitMessages }
