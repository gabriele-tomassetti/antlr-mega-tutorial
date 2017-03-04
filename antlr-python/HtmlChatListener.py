import sys
from antlr4 import *
from ChatParser import ChatParser
from ChatListener import ChatListener

class HtmlChatListener(ChatListener) :
    def __init__(self, output):
        self.output = output
        self.output.write('<html><head><meta charset="UTF-8"/></head><body>')

    def enterName(self, ctx:ChatParser.NameContext) :
        self.output.write("<strong>") 

    def exitName(self, ctx:ChatParser.NameContext) :
        self.output.write(ctx.WORD().getText()) 
        self.output.write("</strong> ") 

    def enterColor(self, ctx:ChatParser.ColorContext) :
        color = ctx.WORD().getText()
        ctx.text = '<span style="color: ' + color + '">'        

    def exitColor(self, ctx:ChatParser.ColorContext):         
        ctx.text += ctx.message().text
        ctx.text += '</span>'

    def exitEmoticon(self, ctx:ChatParser.EmoticonContext) : 
        emoticon = ctx.getText()

        if emoticon == ':-)' or emoticon == ':)' :
            ctx.text = "🙂"
    
        if emoticon == ':-(' or emoticon == ':(' :
            ctx.text = "🙁"

    def enterLink(self, ctx:ChatParser.LinkContext):
        ctx.text = '<a href="%s">%s</a>' % (ctx.TEXT()[1], (ctx.TEXT()[0]))

    def exitMessage(self, ctx:ChatParser.MessageContext):
        text = ''

        for child in ctx.children:
            if hasattr(child, 'text'):
                text += child.text
            else:
                text += child.getText()
        
        if isinstance(ctx.parentCtx, ChatParser.LineContext) is False:
            ctx.text = text
        else:    
            self.output.write(text)
            self.output.write("</p>") 

    def enterCommand(self, ctx:ChatParser.CommandContext):
        if ctx.SAYS() is not None :
            self.output.write(ctx.SAYS().getText() + ':' + '<p>')

        if ctx.SHOUTS() is not None :
            self.output.write(ctx.SHOUTS().getText() + ':' + '<p style="text-transform: uppercase">')    

    def exitChat(self, ctx:ChatParser.ChatContext):
        self.output.write("</body></html>")