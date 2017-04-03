import { Location } from "./location.model";
import { EditorComponent } from "../shared";
import {  LocationDelete, LocationEdit, LocationAdd } from "./location.actions";

const template = require("./location-edit-embed.component.html");
const styles = require("./location-edit-embed.component.scss");

export class LocationEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "location",
            "location-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.location ? "Edit Location": "Create Location";

        if (this.location) {                
            this._nameInputElement.value = this.location.name;  
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
        const location = {
            id: this.location != null ? this.location.id : null,
            name: this._nameInputElement.value
        } as Location;
        
        this.dispatchEvent(new LocationAdd(location));            
    }

    public onDelete() {        
        const location = {
            id: this.location != null ? this.location.id : null,
            name: this._nameInputElement.value
        } as Location;

        this.dispatchEvent(new LocationDelete(location));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "location-id":
                this.locationId = newValue;
                break;
            case "location":
                this.location = JSON.parse(newValue);
                if (this.parentNode) {
                    this.locationId = this.location.id;
                    this._nameInputElement.value = this.location.name != undefined ? this.location.name : "";
                    this._titleElement.textContent = this.locationId ? "Edit Location" : "Create Location";
                }
                break;
        }           
    }

    public locationId: any;
    public location: Location;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".location-name") as HTMLInputElement;}
}

customElements.define(`ce-location-edit-embed`,LocationEditEmbedComponent);
