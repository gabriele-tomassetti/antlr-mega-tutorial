grammar Spreadsheet;

expression          : '(' expression ')'                        #parethesisExp
                    | expression (ASTERISK|SLASH) expression    #mulDivExp
                    | expression (PLUS|MINUS) expression        #addsubExp
                    | NAME '(' expression ')'                   #functionExp
                    | NUMBER                                    #numericAtomExp
                    | ID                                        #idAtomExp
                    ;

fragment LETTER     : [a-zA-Z] ;
fragment DIGIT      : [0-9] ;

ASTERISK            : '*' ;
SLASH               : '/' ;
PLUS                : '+' ;
MINUS               : '-' ;

ID                  : LETTER DIGIT ;

NAME				: LETTER+ ;

NUMBER              : DIGIT+ ('.' DIGIT+)? ;

WHITESPACE          : ' ' -> skip;
