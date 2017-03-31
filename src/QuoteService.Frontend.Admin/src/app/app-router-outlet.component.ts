import { RouterOutlet } from "./router";
import { AuthorizedRouteMiddleware } from "./users";

export class AppRouterOutletComponent extends RouterOutlet {
    constructor(el: any) {
        super(el);
    }

    connectedCallback() {
        this.setRoutes([
            { path: "/", name: "survey-list", authRequired: true },
            { path: "/survey/edit/:surveyId", name: "survey-edit", authRequired: true },
            { path: "/survey/edit/:surveyId/tab/:tabIndex", name: "survey-edit", authRequired: true },            
            { path: "/survey/create", name: "survey-edit", authRequired: true },
            { path: "/survey/list", name: "survey-list", authRequired: true },

            { path: "/survey-result/view/:surveyResultId", name: "survey-result-view", authRequired: true },

            { path: "/questions", name: "question-master-detail", authRequired: true },

            { path: "/login", name: "login" },
            { path: "/error", name: "error" },
            { path: "*", name: "not-found" }

        ] as any);

        this.use(new AuthorizedRouteMiddleware());

        super.connectedCallback();
    }

}

customElements.define(`ce-app-router-oulet`, AppRouterOutletComponent);