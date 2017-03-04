import sys
from antlr4 import *
from ChatParser import ChatParser
from ChatListener import ChatListener
from antlr4.error.ErrorListener import *
import io

class ChatErrorListener(ErrorListener):

    def __init__(self, output):
        self.output = output        
        self._symbol = ''
    
    def syntaxError(self, recognizer, offendingSymbol, line, column, msg, e):        
        self.output.write(msg)
        if offendingSymbol is not None:
            self._symbol = offendingSymbol.text
        else:            
            self._symbol = recognizer.getTokenErrorDisplay(offendingSymbol);        

    @property        
    def symbol(self):
        return self._symbol