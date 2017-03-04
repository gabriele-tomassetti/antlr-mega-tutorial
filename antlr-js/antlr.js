const http = require('http');
const antlr4 = require('antlr4/index');
const ChatLexer = require('./ChatLexer');
const ChatParser = require('./ChatParser');
const HtmlChatListener = require('./HtmlChatListener').HtmlChatListener;
const ChatErrorListener = require('./ChatErrorListener').ChatErrorListener;

http.createServer((req, res) => {
   
   res.writeHead(200, {
       'Content-Type': 'text/html',        
   });

   res.write('<html><head><meta charset="UTF-8"/></head><body>');
   
   var input = "john SHOUTS: hello @michael /pink/this will work/ :-) \n";
   var chars = new antlr4.InputStream(input);
   var lexer = new ChatLexer.ChatLexer(chars);
   var tokens  = new antlr4.CommonTokenStream(lexer);
   var parser = new ChatParser.ChatParser(tokens);
      
   parser.buildParseTrees = true;   
   var tree = parser.chat();   
   var htmlChat = new HtmlChatListener(res);
   antlr4.tree.ParseTreeWalker.DEFAULT.walk(htmlChat, tree);
   
   res.write('</body></html>');
   res.end();

}).listen(1337);