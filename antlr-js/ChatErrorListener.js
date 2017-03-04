const antlr4 = require('antlr4/index');
const ErrorListener = require('antlr4/error/index');
const ChatLexer = require('./ChatLexer');
const ChatParser = require('./ChatParser');
const ChatListener = require('./ChatListener').ChatListener;

ChatErrorListener = function(res) {
    this.Res = res;
    this.symbol = '';
    ErrorListener.ErrorListener.call(this);
	return this;
};

ChatErrorListener.prototype = Object.create(ErrorListener.ErrorListener.prototype);
ChatErrorListener.prototype.constructor = ChatErrorListener;

ChatErrorListener.prototype.syntaxError = function(recognizer, offendingSymbol, line, column, msg, e) {              
    this.Res.write(msg);
    
    if(offendingSymbol == null)
        this.symbol = recognizer.getTokenErrorDisplay(offendingSymbol);
    else
        this.symbol = offendingSymbol.text;
};

exports.ChatErrorListener = ChatErrorListener;