import { ApiService } from "../shared";
import {  } from "../utilities";

const template = require("./splash.component.html");
const styles = require("./splash.component.scss");

export class SplashComponent extends HTMLElement {
    constructor(
        private _apiService: ApiService = ApiService.Instance
    ) {
        super();
        this.onClick = this.onClick.bind(this);
    }

    static get observedAttributes () {
        return [];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.services = await this._apiService.getServices();
        
        for (let i = 0; i < this.services.length; i++) {
            let option = document.createElement("option");
            option.text = this.services[i].name;
            option.value = this.services[i].id;
            this._serviceSelectElement.appendChild(option);
        }
    }

    public async onClick() {
        const result = await this._apiService.calculateQuote({
            address: this._addressElement.value,
            city: this._cityElement.value,
            dateTime: this._dateTimeElement.value,
            durationInHours: this._durationInHoursElement.value,
            serviceId: this._serviceSelectElement.value
        });

        this._resultElement.innerText = result.quote.total;
    }

    private _setEventListeners() {
        this._submitElement.addEventListener("click", this.onClick);
    }

    disconnectedCallback() {
        this._submitElement.removeEventListener("click", this.onClick);
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            default:
                break;
        }
    }

    private services: Array<any> = [];

    private get _addressElement(): HTMLInputElement { return this.querySelector(".address") as HTMLInputElement; }

    private get _cityElement(): HTMLInputElement { return this.querySelector(".city") as HTMLInputElement; }

    private get _durationInHoursElement(): HTMLInputElement { return this.querySelector(".duration-in-hours") as HTMLInputElement; }

    private get _dateTimeElement(): HTMLInputElement { return this.querySelector(".date-time") as HTMLInputElement; }

    private get _serviceSelectElement(): HTMLSelectElement { return this.querySelector("select") as HTMLSelectElement; }

    private get _resultElement(): HTMLSelectElement { return this.querySelector(".results") as HTMLSelectElement; }

    private get _submitElement(): HTMLElement { return this.querySelector(".submit") as HTMLElement; }
}

customElements.define(`ce-splash`,SplashComponent);