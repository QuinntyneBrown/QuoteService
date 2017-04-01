import { ServiceAdd, ServiceDelete, ServiceEdit, serviceActions } from "./service.actions";
import { Service } from "./service.model";
import { ServiceService } from "./service.service";

const template = require("./service-master-detail.component.html");
const styles = require("./service-master-detail.component.scss");

export class ServiceMasterDetailComponent extends HTMLElement {
    constructor(
        private _serviceService: ServiceService = ServiceService.Instance
    ) {
        super();
        this.onServiceAdd = this.onServiceAdd.bind(this);
        this.onServiceEdit = this.onServiceEdit.bind(this);
        this.onServiceDelete = this.onServiceDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "services"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.services = await this._serviceService.get();
        this.serviceListElement.setAttribute("services", JSON.stringify(this.services));
    }

    private _setEventListeners() {
        this.addEventListener(serviceActions.ADD, this.onServiceAdd);
        this.addEventListener(serviceActions.EDIT, this.onServiceEdit);
        this.addEventListener(serviceActions.DELETE, this.onServiceDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(serviceActions.ADD, this.onServiceAdd);
        this.removeEventListener(serviceActions.EDIT, this.onServiceEdit);
        this.removeEventListener(serviceActions.DELETE, this.onServiceDelete);
    }

    public async onServiceAdd(e) {
        await this._serviceService.add(e.detail.service);

        this.services = await this._serviceService.get();

        
        this.serviceListElement.setAttribute("services", JSON.stringify(this.services));
        this.serviceEditElement.setAttribute("service", JSON.stringify(new Service()));
    }

    public onServiceEdit(e) {
        this.serviceEditElement.setAttribute("service", JSON.stringify(e.detail.service));
    }

    public async onServiceDelete(e) {
        await this._serviceService.remove(e.detail.service.id);

        this.services = await this._serviceService.get();

        this.serviceListElement.setAttribute("services", JSON.stringify(this.services));
        this.serviceEditElement.setAttribute("service", JSON.stringify(new Service()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "services":
                this.services = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Service> { return this.services; }

    private services: Array<Service> = [];
    public service: Service = <Service>{};
    public get serviceEditElement(): HTMLElement { return this.querySelector("ce-service-edit-embed") as HTMLElement; }
    public get serviceListElement(): HTMLElement { return this.querySelector("ce-service-list-embed") as HTMLElement; }
}

customElements.define(`ce-service-master-detail`,ServiceMasterDetailComponent);
