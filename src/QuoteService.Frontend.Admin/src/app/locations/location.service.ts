import { fetch } from "../utilities";
import { Location } from "./location.model";

export class LocationService {
    constructor(private _fetch = fetch) { }

    private static _instance: LocationService;

    public static get Instance() {
        this._instance = this._instance || new LocationService();
        return this._instance;
    }

    public get(): Promise<Array<Location>> {
        return this._fetch({ url: "/api/location/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { locations: Array<Location> }).locations;
        });
    }

    public getById(id): Promise<Location> {
        return this._fetch({ url: `/api/location/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { location: Location }).location;
        });
    }

    public add(location) {
        return this._fetch({ url: `/api/location/add`, method: "POST", data: { location }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/location/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
