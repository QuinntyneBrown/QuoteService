import { Service } from "./service.model";
import { EditorComponent } from "../shared";
import {  ServiceDelete, ServiceEdit, ServiceAdd } from "./service.actions";

const template = require("./service-edit-embed.component.html");
const styles = require("./service-edit-embed.component.scss");

export class ServiceEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "service",
            "service-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.service ? "Edit Service": "Create Service";

        this._descriptionEditor = new EditorComponent(this._descriptionElement);

        if (this.service) {                
            this._nameInputElement.value = this.service.name; 
            this._rateElement.value = this.service.rate; 
            this._descriptionEditor.setHTML(this.service.description);
            this._imageUrl.value = this.service.imageUrl;

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
        const service = {
            id: this.service != null ? this.service.id : null,
            name: this._nameInputElement.value,
            rate: this._rateElement.value,
            imageUrl: this._imageUrl.value,
            description: this._descriptionEditor.text
        } as Service;
        
        this.dispatchEvent(new ServiceAdd(service));            
    }

    public onDelete() {        
        const service = {
            id: this.service != null ? this.service.id : null,
            name: this._nameInputElement.value
        } as Service;

        this.dispatchEvent(new ServiceDelete(service));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "service-id":
                this.serviceId = newValue;
                break;
            case "service":
                this.service = JSON.parse(newValue);
                if (this.parentNode) {
                    this.serviceId = this.service.id;
                    this._nameInputElement.value = this.service.name != undefined ? this.service.name : "";
                    this._rateElement.value = this.service.rate != undefined ? this.service.rate : "";
                    this._imageUrl.value = this.service.imageUrl != undefined ? this.service.imageUrl : "";
                    this._descriptionEditor.setHTML(this.service.description != undefined ? this.service.description : "");
                    this._titleElement.textContent = this.serviceId ? "Edit Service" : "Create Service";
                }
                break;
        }           
    }

    public serviceId: any;
    public service: Service;
    public _descriptionEditor: EditorComponent;

    private get _imageUrl(): HTMLInputElement { return this.querySelector(".service-image-url") as HTMLInputElement; }
    private get _rateElement(): HTMLInputElement { return this.querySelector(".service-rate") as HTMLInputElement; }
    private get _descriptionElement(): HTMLElement { return this.querySelector(".service-description") as HTMLElement; }
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".service-name") as HTMLInputElement;}
}

customElements.define(`ce-service-edit-embed`,ServiceEditEmbedComponent);
