# MicroFrontend
This project is focusing on implementing a micro front-end example using angular and module federation along with a dotnet 6 API.

# Prerequisits
Node.js: https://nodejs.org/en/
Angular version 13+: https://angular.io/start
.Net 6 framework: https://dotnet.microsoft.com/en-us/download

# Getting started:
 - start the API using the `run-server.bat` file.
 - go to `.\src\front-end\` path and run `npm install` on the terminal.
 - build the shared library `ng build --project shared`.
 - run the remote micro front-end first `ng serve --project user-details`.
 - run the shell (main application) using `ng serve -o --project shell`.

# Resources:
 - ASP.NET clean architecture repository: https://github.com/jasontaylordev/CleanArchitecture
 - Angular module federation example: https://github.com/manfredsteyer/module-federation-plugin-example

