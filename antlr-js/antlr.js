import { createServer } from 'http';
import antlr4 from 'antlr4';
const { CommonTokenStream, InputStream } = antlr4;
import ChatLexer from './ChatLexer.js';
import ChatParser from './ChatParser.js';
import HtmlChatListener from './HtmlChatListener.js';

createServer((req, res) => {
   
   res.writeHead(200, {
       'Content-Type': 'text/html',        
   });

   res.write('<html><head><meta charset="UTF-8"/></head><body>');
   
   var input = "john SHOUTS: hello @michael /pink/this will work/ :-) \n";
   var chars = new InputStream(input, true)   
   var lexer = new ChatLexer(chars);
   var tokens  = new CommonTokenStream(lexer);
   var parser = new ChatParser(tokens);
      
   parser.buildParseTrees = true;   
   var tree = parser.chat();   
   var htmlChat = new HtmlChatListener(res);
   antlr4.tree.ParseTreeWalker.DEFAULT.walk(htmlChat, tree);
   
   res.write('</body></html>');
   res.end();

}).listen(1337);