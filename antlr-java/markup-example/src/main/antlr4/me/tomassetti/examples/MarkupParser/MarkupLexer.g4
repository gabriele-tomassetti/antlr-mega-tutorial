lexer grammar MarkupLexer;

OPEN                : '[' -> pushMode(BBCODE) ;
TEXT                : ~('[')+ ;

// Parsing content inside tags
mode BBCODE;

CLOSE               : ']' -> popMode ;
SLASH               : '/' ;
EQUALS              : '=' ;
STRING              : '"' .*? '"' ;
ID                  : LETTERS+ ;
WS                  : [ \t\r\n] -> skip ;

fragment LETTERS    : [a-zA-Z] ;