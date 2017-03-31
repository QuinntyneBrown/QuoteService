import { GeolocationService } from "./geolocation.service";

const template = require("./geolocation.component.html");
const styles = require("./geolocation.component.scss");

export class GeolocationComponent extends HTMLElement {
    constructor(
        private _geolocationService: GeolocationService = GeolocationService.Instance
    ) {
        super();        
        this.onClick = this.onClick.bind(this);
    }
    
    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    public async onClick() {
        const results = await this._geolocationService.get({
            address1: this.address1.value,
            address2: this.address2.value
        });

        this.distanceElement.innerText = results.distance;
    }

    private async _bind() {
        
    }

    private _setEventListeners() {
        this.submitButton.addEventListener("click", this.onClick);
    }

    disconnectedCallback() {
        this.submitButton.removeEventListener("click", this.onClick);
    }

    public get distanceElement(): HTMLElement { return this.querySelector(".distance") as HTMLElement; }

    public get submitButton(): HTMLElement { return this.querySelector(".submit") as HTMLElement; }

    public get address1(): HTMLInputElement { return this.querySelector(".address-1") as HTMLInputElement; }

    public get address2(): HTMLInputElement { return this.querySelector(".address-2") as HTMLInputElement; }
}

customElements.define(`ce-geolocation`,GeolocationComponent);
