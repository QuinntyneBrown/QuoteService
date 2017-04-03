import { LocationAdd, LocationDelete, LocationEdit, locationActions } from "./location.actions";
import { Location } from "./location.model";

const template = require("./location-master-detail.component.html");
const styles = require("./location-master-detail.component.scss");

export class LocationMasterDetailComponent extends HTMLElement {
    constructor() {
        super();
        this.onLocationAdd = this.onLocationAdd.bind(this);
        this.onLocationEdit = this.onLocationEdit.bind(this);
        this.onLocationDelete = this.onLocationDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "locations"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.locationListElement.setAttribute("locations", JSON.stringify(this.locations));
    }

    private _setEventListeners() {
        this.addEventListener(locationActions.ADD, this.onLocationAdd);
        this.addEventListener(locationActions.EDIT, this.onLocationEdit);
        this.addEventListener(locationActions.DELETE, this.onLocationDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(locationActions.ADD, this.onLocationAdd);
        this.removeEventListener(locationActions.EDIT, this.onLocationEdit);
        this.removeEventListener(locationActions.DELETE, this.onLocationDelete);
    }

    public onLocationAdd(e) {

        const index = this.locations.findIndex(o => o.id == e.detail.location.id);
        const indexBaseOnUniqueIdentifier = this.locations.findIndex(o => o.name == e.detail.location.name);

        if (index > -1 && e.detail.location.id != null) {
            this.locations[index] = e.detail.location;
        } else if (indexBaseOnUniqueIdentifier > -1) {
            for (var i = 0; i < this.locations.length; ++i) {
                if (this.locations[i].name == e.detail.location.name)
                    this.locations[i] = e.detail.location;
            }
        } else {
            this.locations.push(e.detail.location);
        }
        
        this.locationListElement.setAttribute("locations", JSON.stringify(this.locations));
        this.locationEditElement.setAttribute("location", JSON.stringify(new Location()));
    }

    public onLocationEdit(e) {
        this.locationEditElement.setAttribute("location", JSON.stringify(e.detail.location));
    }

    public onLocationDelete(e) {
        if (e.detail.location.Id != null && e.detail.location.Id != undefined) {
            this.locations.splice(this.locations.findIndex(o => o.id == e.detail.optionId), 1);
        } else {
            for (var i = 0; i < this.locations.length; ++i) {
                if (this.locations[i].name == e.detail.location.name)
                    this.locations.splice(i, 1);
            }
        }

        this.locationListElement.setAttribute("locations", JSON.stringify(this.locations));
        this.locationEditElement.setAttribute("location", JSON.stringify(new Location()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "locations":
                this.locations = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Location> { return this.locations; }

    private locations: Array<Location> = [];
    public location: Location = <Location>{};
    public get locationEditElement(): HTMLElement { return this.querySelector("ce-location-edit-embed") as HTMLElement; }
    public get locationListElement(): HTMLElement { return this.querySelector("ce-location-list-embed") as HTMLElement; }
}

customElements.define(`ce-location-master-detail`,LocationMasterDetailComponent);
