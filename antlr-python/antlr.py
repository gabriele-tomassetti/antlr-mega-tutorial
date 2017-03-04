import sys
from antlr4 import *
from ChatLexer import ChatLexer
from ChatParser import ChatParser
from HtmlChatListener import HtmlChatListener

def main(argv):
    input = FileStream(argv[1])
    lexer = ChatLexer(input)
    stream = CommonTokenStream(lexer)
    parser = ChatParser(stream)
    tree = parser.chat()

    output = open("output.html","w")
    
    htmlChat = HtmlChatListener(output)
    walker = ParseTreeWalker()
    walker.walk(htmlChat, tree)
        
    output.close()      

if __name__ == '__main__':
    main(sys.argv)