import { Quote } from "./quote.model";

export const quoteActions = {
    ADD: "[Quote] Add",
    EDIT: "[Quote] Edit",
    DELETE: "[Quote] Delete",
    QUOTES_CHANGED: "[Quote] Quotes Changed"
};

export class QuoteEvent extends CustomEvent {
    constructor(eventName:string, quote: Quote) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { quote }
        });
    }
}

export class QuoteAdd extends QuoteEvent {
    constructor(quote: Quote) {
        super(quoteActions.ADD, quote);        
    }
}

export class QuoteEdit extends QuoteEvent {
    constructor(quote: Quote) {
        super(quoteActions.EDIT, quote);
    }
}

export class QuoteDelete extends QuoteEvent {
    constructor(quote: Quote) {
        super(quoteActions.DELETE, quote);
    }
}

export class QuotesChanged extends CustomEvent {
    constructor(quotes: Array<Quote>) {
        super(quoteActions.QUOTES_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { quotes }
        });
    }
}
