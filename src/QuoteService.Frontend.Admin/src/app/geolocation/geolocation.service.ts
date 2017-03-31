import { fetch } from "../utilities";

export class GeolocationService {
    constructor(private _fetch = fetch) { }

    private static _instance: GeolocationService;

    public static get Instance() {
        this._instance = this._instance || new GeolocationService();
        
        return this._instance;
    }

    public get(options: { address1: string, address2: string }): Promise<any> {
        return this._fetch({ url: "/api/geolocation/getdistance", method: "POST", data: options}).then((results:string) => {
            return JSON.parse(results);
        });
    }
    
}
