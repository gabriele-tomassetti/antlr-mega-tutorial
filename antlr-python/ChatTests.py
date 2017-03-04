from antlr4 import *
from ChatLexer import ChatLexer
from ChatParser import ChatParser
from HtmlChatListener import HtmlChatListener
from ChatErrorListener import ChatErrorListener
import sys
import unittest
import io

class TestChatParser(unittest.TestCase):

    def setup(self, text):        
        lexer = ChatLexer(InputStream(text))        
        stream = CommonTokenStream(lexer)
        parser = ChatParser(stream)
        
        self.output = io.StringIO()
        self.error = io.StringIO()
        
        parser.removeErrorListeners()        
        errorListener = ChatErrorListener(self.error)
        parser.addErrorListener(errorListener)  

        lexer.removeErrorListeners()
        lexer.addErrorListener(errorListener)  

        self.errorListener = errorListener              
        
        return parser
        
    def test_valid_name(self):
        parser = self.setup("John ")
        tree = parser.name()               
    
        htmlChat = HtmlChatListener(self.output)
        walker = ParseTreeWalker()
        walker.walk(htmlChat, tree)              
        
        # let's check that there aren't any symbols in errorListener
        self.assertEqual(len(self.errorListener.symbol), 0)

    def test_invalid_name(self):
        parser = self.setup("John-")
        tree = parser.name()               
    
        htmlChat = HtmlChatListener(self.output)
        walker = ParseTreeWalker()
        walker.walk(htmlChat, tree)              
        
        # let's check the symbol in errorListener
        # note that self.errorListener.symbol could be '<no token>'
        # when ANTLR doesn't recognize the token or there is no error.
        # In such cases check the output of errorListener
        self.assertEqual(self.errorListener.symbol, '-')
        
    def test_valid_link(self):
        parser = self.setup("[okay-now](awesome link, even with spaces)")
        tree = parser.link()        
    
        htmlChat = HtmlChatListener(self.output)
        walker = ParseTreeWalker()
        walker.walk(htmlChat, tree)          
        
        self.assertEqual(len(self.errorListener.symbol), 0)

    def test_invalid_link(self):
        parser = self.setup("[not-okay-now]](awesome link, even with spaces)")
        tree = parser.link()               
    
        htmlChat = HtmlChatListener(self.output)
        walker = ParseTreeWalker()
        walker.walk(htmlChat, tree)              
        
        self.assertEqual(self.errorListener.symbol, ']')

if __name__ == '__main__':
    unittest.main()