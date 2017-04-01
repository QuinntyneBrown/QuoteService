import { fetch } from "../utilities";
import { Service } from "./service.model";

export class ServiceService {
    constructor(private _fetch = fetch) { }

    private static _instance: ServiceService;

    public static get Instance() {
        this._instance = this._instance || new ServiceService();
        return this._instance;
    }

    public get(): Promise<Array<Service>> {
        return this._fetch({ url: "/api/service/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { services: Array<Service> }).services;
        });
    }

    public getById(id): Promise<Service> {
        return this._fetch({ url: `/api/service/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { service: Service }).service;
        });
    }

    public add(service) {
        return this._fetch({ url: `/api/service/add`, method: "POST", data: { service }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/service/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
