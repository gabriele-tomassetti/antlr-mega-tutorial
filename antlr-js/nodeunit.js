
const http = require('http');
const antlr4 = require('antlr4/index');
const ChatLexer = require('./ChatLexer');
const ChatParser = require('./ChatParser');
const HtmlChatListener = require('./HtmlChatListener').HtmlChatListener;
const ChatErrorListener = require('./ChatErrorListener').ChatErrorListener;

function prepare(input) {
    var stream = require("stream");
    this.res = new stream.PassThrough();
    var res2 = new stream.PassThrough();

    // if you want to see errors on stdout
    //this.res.pipe(process.stdout); 

    this.buffers = [];
    this.res.on('data', function(data) {
        buffers.push(data);
    });       
    
    var chars = new antlr4.InputStream(input);
    this.lexer = new ChatLexer.ChatLexer(chars);
    var tokens  = new antlr4.CommonTokenStream(this.lexer);
    this.parser = new ChatParser.ChatParser(tokens);
   
    this.errorListener = new ChatErrorListener(this.res);    
    this.lexer.removeErrorListeners();
    // uncomment if you want to see errors from the lexer
    //this.lexer.addErrorListener(this.errorListener); 
    this.parser.removeErrorListeners();
    this.parser.addErrorListener(this.errorListener); 

    this.parser.buildParseTrees = true;   
    this.htmlChat = new HtmlChatListener(res2);   

    return this;             
}

module.exports = {    
    //setUp: function (callback) {
    //    callback();
    //},
    //tearDown: function (callback) {                
    //    callback();
    //},
    testInvalidName: function (test) {
        var setup = prepare("john-");
        var tree = setup.parser.name();      
        antlr4.tree.ParseTreeWalker.DEFAULT.walk(setup.htmlChat, tree);            

        // note that setup.errorListener.symbol could be '<no token>'
        // when ANTLR doesn't recognize the token or there is no errror.
        // In such cases check the output of errorListener
        //console.log(Buffer.concat(setup.buffers).toString());
        test.equals(setup.errorListener.symbol, '-');            
                
        setup.res.end();                
        test.done();
    },

    testValidName: function (test) {
        var setup = prepare("john ");
        var tree = setup.parser.name();      
        antlr4.tree.ParseTreeWalker.DEFAULT.walk(setup.htmlChat, tree);            

        test.equals(setup.errorListener.symbol, '');            
                
        setup.res.end();
        
        test.done();
    },

    testInvalidLink: function (test) {
        var setup = prepare("[okay-now]-(awesome link, even with spaces)");
        var tree = setup.parser.link();      
        antlr4.tree.ParseTreeWalker.DEFAULT.walk(setup.htmlChat, tree);            
        
        test.equals(setup.errorListener.symbol, '-');                    
        setup.res.end();        

        test.done();
    },

    testValidLink: function (test) {
        var setup = prepare("[okay-now](awesome link, even with spaces)");
        var tree = setup.parser.link();      
        antlr4.tree.ParseTreeWalker.DEFAULT.walk(setup.htmlChat, tree);            

        test.equals(setup.errorListener.symbol, '');            
                
        setup.res.end();
        
        test.done();
    }   
};
