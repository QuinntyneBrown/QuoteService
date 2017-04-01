import { insertAfter, createElement } from "../utilities";

const template = require("./button.component.html");
const styles = require("./button.component.scss");

export class ButtonComponent extends HTMLElement {
    constructor() {
        super();
    }
    
    connectedCallback() {
        const textContent = this.textContent;
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        insertAfter(createElement(textContent), this.querySelector("style"));        
    }
}

customElements.define(`ce-button`,ButtonComponent);
