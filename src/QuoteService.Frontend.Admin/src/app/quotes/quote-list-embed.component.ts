import { Quote } from "./quote.model";

const template = require("./quote-list-embed.component.html");
const styles = require("./quote-list-embed.component.scss");

export class QuoteListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "quotes"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.quotes.length; i++) {
            let el = this._document.createElement(`ce-quote-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.quotes[i]));
            this.appendChild(el);
        }    
    }

    quotes:Array<Quote> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "quotes":
                this.quotes = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-quote-list-embed", QuoteListEmbedComponent);
