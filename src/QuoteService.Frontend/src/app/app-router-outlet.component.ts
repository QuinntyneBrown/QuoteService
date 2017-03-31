import { RouterOutlet } from "./router";

export class AppRouterOutletComponent extends RouterOutlet {
    constructor(el: any) {
        super(el);
    }

    connectedCallback() {
        this.setRoutes([
            { path: "/", name: "splash" },
            { path: "/landing", name: "landing" },
            { path: "/thank-you", name: "thank-you" },
            { path: "/error", name: "error" }            
        ] as any);
        
        super.connectedCallback();
    }

}

customElements.define(`ce-app-router-oulet`, AppRouterOutletComponent);