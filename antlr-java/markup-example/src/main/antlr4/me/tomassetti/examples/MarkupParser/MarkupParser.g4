parser grammar MarkupParser;

options { tokenVocab=MarkupLexer; }

file        : element* ;

attribute   : ID '=' STRING ; 

content     : TEXT ;

element     : (content | tag) ;

tag         : '[' ID attribute? ']' element* '[' '/' ID ']' ;