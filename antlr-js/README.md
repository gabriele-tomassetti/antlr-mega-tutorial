# ANTLR4 Tutorial - Javascript Project

If you need help to setup everything you can read the [Javascript Setup section of the ANTLR Mega Tutorial](https://tomassetti.me/antlr-mega-tutorial/#javascript-setup)

The commands you need to know:
```
// to install dependencies and devDependencies
npm install 
// to generate parser and lexer
antlr4 -Dlanguage=JavaScript Chat.g4 
// to launch node. The[js] suffix is usually added only in debian-derived distributions
// then point your browser to the proper location, usually http://localhost:1337/
node[js] antlr.js 
// to run the tests
./node_modules/nodeunit/bin/nodeunit nodeunit.js
```