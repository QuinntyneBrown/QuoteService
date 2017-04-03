import { Location } from "./location.model";

export const locationActions = {
    ADD: "[Location] Add",
    EDIT: "[Location] Edit",
    DELETE: "[Location] Delete",
    LOCATIONS_CHANGED: "[Location] Locations Changed"
};

export class LocationEvent extends CustomEvent {
    constructor(eventName:string, location: Location) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { location }
        });
    }
}

export class LocationAdd extends LocationEvent {
    constructor(location: Location) {
        super(locationActions.ADD, location);        
    }
}

export class LocationEdit extends LocationEvent {
    constructor(location: Location) {
        super(locationActions.EDIT, location);
    }
}

export class LocationDelete extends LocationEvent {
    constructor(location: Location) {
        super(locationActions.DELETE, location);
    }
}

export class LocationsChanged extends CustomEvent {
    constructor(locations: Array<Location>) {
        super(locationActions.LOCATIONS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { locations }
        });
    }
}
