import { Location } from "./location.model";

const template = require("./location-list-embed.component.html");
const styles = require("./location-list-embed.component.scss");

export class LocationListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "locations"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.locations.length; i++) {
            let el = this._document.createElement(`ce-location-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.locations[i]));
            this.appendChild(el);
        }    
    }

    locations:Array<Location> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "locations":
                this.locations = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-location-list-embed", LocationListEmbedComponent);
