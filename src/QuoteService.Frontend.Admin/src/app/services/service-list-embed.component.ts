import { Service } from "./service.model";

const template = require("./service-list-embed.component.html");
const styles = require("./service-list-embed.component.scss");

export class ServiceListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "services"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.services.length; i++) {
            let el = this._document.createElement(`ce-service-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.services[i]));
            this.appendChild(el);
        }    
    }

    services:Array<Service> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "services":
                this.services = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-service-list-embed", ServiceListEmbedComponent);
