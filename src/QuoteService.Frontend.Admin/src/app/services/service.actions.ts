import { Service } from "./service.model";

export const serviceActions = {
    ADD: "[Service] Add",
    EDIT: "[Service] Edit",
    DELETE: "[Service] Delete",
    SERVICES_CHANGED: "[Service] Services Changed"
};

export class ServiceEvent extends CustomEvent {
    constructor(eventName:string, service: Service) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { service }
        });
    }
}

export class ServiceAdd extends ServiceEvent {
    constructor(service: Service) {
        super(serviceActions.ADD, service);        
    }
}

export class ServiceEdit extends ServiceEvent {
    constructor(service: Service) {
        super(serviceActions.EDIT, service);
    }
}

export class ServiceDelete extends ServiceEvent {
    constructor(service: Service) {
        super(serviceActions.DELETE, service);
    }
}

export class ServicesChanged extends CustomEvent {
    constructor(services: Array<Service>) {
        super(serviceActions.SERVICES_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { services }
        });
    }
}
