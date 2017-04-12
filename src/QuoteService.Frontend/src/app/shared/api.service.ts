import { fetch } from "../utilities";
import { environment } from "../environment";

export class ApiService {
    constructor(private _fetch = fetch) { }

    private static _instance: ApiService;

    public static get Instance() {
        this._instance = this._instance || new this();
        return this._instance;
    }

    public getServices():Promise<any> {
        return this._fetch({ url: "/api/service/get" }).then((results: string) => {
            return JSON.parse(results).services;
        });
    }

    public calculateQuote(serviceQuoteRequest: {
        address: any,
        city: any,
        dateTime: any,
        durationInHours: any,
        serviceId: any,
        longitude: number,
        latitude:number
    }): Promise<any> {
        alert(JSON.stringify(serviceQuoteRequest));
        return this._fetch({ url: "/api/quote/calculate", method: "POST", data: { quoteRequest: { serviceQuoteRequest } } }).then((results: string) => {
            return JSON.parse(results);
        });
    }
}
