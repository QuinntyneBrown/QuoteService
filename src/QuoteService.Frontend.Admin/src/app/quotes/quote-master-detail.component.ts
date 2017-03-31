import { QuoteAdd, QuoteDelete, QuoteEdit, quoteActions } from "./quote.actions";
import { Quote } from "./quote.model";

const template = require("./quote-master-detail.component.html");
const styles = require("./quote-master-detail.component.scss");

export class QuoteMasterDetailComponent extends HTMLElement {
    constructor() {
        super();
        this.onQuoteAdd = this.onQuoteAdd.bind(this);
        this.onQuoteEdit = this.onQuoteEdit.bind(this);
        this.onQuoteDelete = this.onQuoteDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "quotes"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.quoteListElement.setAttribute("quotes", JSON.stringify(this.quotes));
    }

    private _setEventListeners() {
        this.addEventListener(quoteActions.ADD, this.onQuoteAdd);
        this.addEventListener(quoteActions.EDIT, this.onQuoteEdit);
        this.addEventListener(quoteActions.DELETE, this.onQuoteDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(quoteActions.ADD, this.onQuoteAdd);
        this.removeEventListener(quoteActions.EDIT, this.onQuoteEdit);
        this.removeEventListener(quoteActions.DELETE, this.onQuoteDelete);
    }

    public onQuoteAdd(e) {

        const index = this.quotes.findIndex(o => o.id == e.detail.quote.id);
        const indexBaseOnUniqueIdentifier = this.quotes.findIndex(o => o.name == e.detail.quote.name);

        if (index > -1 && e.detail.quote.id != null) {
            this.quotes[index] = e.detail.quote;
        } else if (indexBaseOnUniqueIdentifier > -1) {
            for (var i = 0; i < this.quotes.length; ++i) {
                if (this.quotes[i].name == e.detail.quote.name)
                    this.quotes[i] = e.detail.quote;
            }
        } else {
            this.quotes.push(e.detail.quote);
        }
        
        this.quoteListElement.setAttribute("quotes", JSON.stringify(this.quotes));
        this.quoteEditElement.setAttribute("quote", JSON.stringify(new Quote()));
    }

    public onQuoteEdit(e) {
        this.quoteEditElement.setAttribute("quote", JSON.stringify(e.detail.quote));
    }

    public onQuoteDelete(e) {
        if (e.detail.quote.Id != null && e.detail.quote.Id != undefined) {
            this.quotes.splice(this.quotes.findIndex(o => o.id == e.detail.optionId), 1);
        } else {
            for (var i = 0; i < this.quotes.length; ++i) {
                if (this.quotes[i].name == e.detail.quote.name)
                    this.quotes.splice(i, 1);
            }
        }

        this.quoteListElement.setAttribute("quotes", JSON.stringify(this.quotes));
        this.quoteEditElement.setAttribute("quote", JSON.stringify(new Quote()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "quotes":
                this.quotes = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Quote> { return this.quotes; }

    private quotes: Array<Quote> = [];
    public quote: Quote = <Quote>{};
    public get quoteEditElement(): HTMLElement { return this.querySelector("ce-quote-edit-embed") as HTMLElement; }
    public get quoteListElement(): HTMLElement { return this.querySelector("ce-quote-list-embed") as HTMLElement; }
}

customElements.define(`ce-quote-master-detail`,QuoteMasterDetailComponent);
