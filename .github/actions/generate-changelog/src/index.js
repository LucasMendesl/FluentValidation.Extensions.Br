const core = require('@actions/core')
const { context, getOctokit } = require('@actions/github')

const client = getOctokit(core.getInput('token'))
require('./main')(context, core, client)