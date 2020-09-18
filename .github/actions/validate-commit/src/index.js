const core = require('@actions/core')
const { context, getOctokit } = require('@actions/github')

require('./main')(getOctokit(core.getInput('token')), context)
