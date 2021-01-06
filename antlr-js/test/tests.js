import assert from 'assert'
import { createServer } from 'http';
import antlr4 from 'antlr4';
const { CommonTokenStream, InputStream } = antlr4;
import ChatLexer from '../ChatLexer.js';
import ChatParser from '../ChatParser.js';
import HtmlChatListener from '../HtmlChatListener.js';
import ChatErrorListener from '../ChatErrorListener.js';
import stream from 'stream'; 

function prepare(input) {       
    var data = {};
    //var stream = require("stream");
    data.res = new stream.PassThrough();
    var res2 = new stream.PassThrough();

    // if you want to see errors on stdout
    //data.res.pipe(process.stdout); 

    data.buffers = [];
    data.res.on('data', function(feed) {
        data.buffers.push(feed);
    });       
    
    var chars = new InputStream(input ,true);
    data.lexer = new ChatLexer(chars);
    var tokens  = new CommonTokenStream(data.lexer);
    data.parser = new ChatParser(tokens);
   
    data.errorListener = new ChatErrorListener(data.res);    
    data.lexer.removeErrorListeners();
    // uncomment if you want to see errors from the lexer
    //data.lexer.addErrorListener(data.errorListener); 
    data.parser.removeErrorListeners();
    data.parser.addErrorListener(data.errorListener); 

    data.parser.buildParseTrees = true;   
    data.htmlChat = new HtmlChatListener(res2);   

    return data;             
}

describe('ChatParser', function() {
    describe('Valid inputs', function() {        
        describe('Valid name', function() {
            it('It should return no error for a valid name', function() {
                var setup = prepare("john ");
                var tree = setup.parser.name();      
                antlr4.tree.ParseTreeWalker.DEFAULT.walk(setup.htmlChat, tree);            

                assert.equal(setup.errorListener.symbol, '');            

                setup.res.end();                            
            });
        });
        describe('Valid link', function() {
            it('It should return no error for a valid name', function() {
                var setup = prepare("[okay-now](awesome link, even with spaces)");
                var tree = setup.parser.link();      
                antlr4.tree.ParseTreeWalker.DEFAULT.walk(setup.htmlChat, tree);            

                assert.equal(setup.errorListener.symbol, '');            

                setup.res.end();                
            });
        });
    });
    
    describe('Invalid input', function() {
        describe('Invalid name', function() {
            it('The error listener should contain the symbol -', function() {
                var setup = prepare("john-");
                var tree = setup.parser.name();      
                antlr4.tree.ParseTreeWalker.DEFAULT.walk(setup.htmlChat, tree);            
    
                // note that setup.errorListener.symbol could be '<no token>'
                // when ANTLR doesn't recognize the token or there is no errror.
                // In such cases check the output of errorListener
                //console.log(Buffer.concat(setup.buffers).toString());
                assert.equal(setup.errorListener.symbol, '-');                    
                setup.res.end();                
            });
        });
        describe('Invalid link', function() {
            it('The error listener should contain the symbol -', function() {
                var setup = prepare("[okay-now]-(awesome link, even with spaces)");
                var tree = setup.parser.link();      
                antlr4.tree.ParseTreeWalker.DEFAULT.walk(setup.htmlChat, tree);            

                assert.equal(setup.errorListener.symbol, '-');                    
                setup.res.end();                
            });
        });    
      });
  });