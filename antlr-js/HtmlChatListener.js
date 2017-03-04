const antlr4 = require('antlr4/index');
const ChatLexer = require('./ChatLexer');
const ChatParser = require('./ChatParser');
var ChatListener = require('./ChatListener').ChatListener;

HtmlChatListener = function(res) {
    this.Res = res;    
    ChatListener.call(this);
	return this;
};

HtmlChatListener.prototype = Object.create(ChatListener.prototype);
HtmlChatListener.prototype.constructor = HtmlChatListener;

HtmlChatListener.prototype.enterName = function(ctx) {          
    this.Res.write("<strong>");    
};
HtmlChatListener.prototype.exitName = function(ctx) {      
    this.Res.write(ctx.WORD().getText());
    this.Res.write("</strong> ");
}; 

HtmlChatListener.prototype.enterColor = function(ctx) {     
    var color = ctx.WORD().getText();             
    ctx.text = '<span style="color: ' + color + '">';
};

HtmlChatListener.prototype.exitColor = function(ctx) {         
    ctx.text += ctx.message().text;    
    ctx.text += '</span>';
};

HtmlChatListener.prototype.exitEmoticon = function(ctx) {      
    var emoticon = ctx.getText();        
    
    if(emoticon == ':-)' || emoticon == ':)')
    {        
        ctx.text = "🙂";
    }
    
    if(emoticon == ':-(' || emoticon == ':(')
    {          
        ctx.text = "🙁";
    }
}; 

HtmlChatListener.prototype.exitMessage = function(ctx) {                
    var text = '';

    for (var index = 0; index <  ctx.children.length; index++ ) {
        if(ctx.children[index].text != null)
            text += ctx.children[index].text;
        else
            text += ctx.children[index].getText();
    }

    if(ctx.parentCtx instanceof ChatParser.ChatParser.LineContext == false)
    {
        ctx.text = text;        
    }    
    else
    {
        this.Res.write(text);
        this.Res.write("</p>");
    }
};

HtmlChatListener.prototype.enterCommand = function(ctx) {          
    if(ctx.SAYS() != null)
        this.Res.write(ctx.SAYS().getText() + ':' + '<p>');

    if(ctx.SHOUTS() != null)
        this.Res.write(ctx.SHOUTS().getText() + ':' + '<p style="text-transform: uppercase">');
};


exports.HtmlChatListener = HtmlChatListener;