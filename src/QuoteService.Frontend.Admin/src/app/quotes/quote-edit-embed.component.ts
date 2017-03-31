import { Quote } from "./quote.model";
import { EditorComponent } from "../shared";
import {  QuoteDelete, QuoteEdit, QuoteAdd } from "./quote.actions";

const template = require("./quote-edit-embed.component.html");
const styles = require("./quote-edit-embed.component.scss");

export class QuoteEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "quote",
            "quote-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.quote ? "Edit Quote": "Create Quote";

        if (this.quote) {                
            this._nameInputElement.value = this.quote.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        const quote = {
            id: this.quote != null ? this.quote.id : null,
            name: this._nameInputElement.value
        } as Quote;
        
        this.dispatchEvent(new QuoteAdd(quote));            
    }

    public onDelete() {        
        const quote = {
            id: this.quote != null ? this.quote.id : null,
            name: this._nameInputElement.value
        } as Quote;

        this.dispatchEvent(new QuoteDelete(quote));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "quote-id":
                this.quoteId = newValue;
                break;
            case "quote":
                this.quote = JSON.parse(newValue);
                if (this.parentNode) {
                    this.quoteId = this.quote.id;
                    this._nameInputElement.value = this.quote.name != undefined ? this.quote.name : "";
                    this._titleElement.textContent = this.quoteId ? "Edit Quote" : "Create Quote";
                }
                break;
        }           
    }

    public quoteId: any;
    public quote: Quote;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".quote-name") as HTMLInputElement;}
}

customElements.define(`ce-quote-edit-embed`,QuoteEditEmbedComponent);
