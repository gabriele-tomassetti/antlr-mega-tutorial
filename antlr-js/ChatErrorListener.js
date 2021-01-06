import antlr4 from 'antlr4';
import ChatLexer from './ChatLexer.js';
import ChatParser from './ChatParser.js';
import ChatListener  from './ChatListener.js';

export default class ChatErrorErrorListener extends antlr4.error.ErrorListener {
    constructor(res) {
        super();
        this.Res = res;
        this.symbol = '';
    }

    syntaxError(recognizer, offendingSymbol, line, column, msg, e) {            
        this.Res.write(msg);
        
        if(offendingSymbol == null)
            this.symbol = recognizer.getTokenErrorDisplay(offendingSymbol);
        else
            this.symbol = offendingSymbol.text;
    }
}