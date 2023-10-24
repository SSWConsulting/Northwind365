const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:44376';

const PROXY_CONFIG = [
  {
    context: [
      "/api",
      "/_configuration",
      "/.well-known",
      "/connect",
      "/Identity",
      "/Account",
      "/login",
      "/register",
      "/refresh",
      "/swagger",
      "_vs",
      "_framework"
   ],
    proxyTimeout: 10000,
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
