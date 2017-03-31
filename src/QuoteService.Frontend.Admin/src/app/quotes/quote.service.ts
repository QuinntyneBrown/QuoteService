import { fetch } from "../utilities";
import { Quote } from "./quote.model";

export class QuoteService {
    constructor(private _fetch = fetch) { }

    private static _instance: QuoteService;

    public static get Instance() {
        this._instance = this._instance || new QuoteService();
        return this._instance;
    }

    public get(): Promise<Array<Quote>> {
        return this._fetch({ url: "/api/quote/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { quotes: Array<Quote> }).quotes;
        });
    }

    public getById(id): Promise<Quote> {
        return this._fetch({ url: `/api/quote/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { quote: Quote }).quote;
        });
    }

    public add(quote) {
        return this._fetch({ url: `/api/quote/add`, method: "POST", data: { quote }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/quote/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
