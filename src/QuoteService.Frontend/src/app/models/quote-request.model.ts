export class QuoteRequest { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): QuoteRequest {
        let quoteRequest = new QuoteRequest();
        quoteRequest.name = data.name;
        return quoteRequest;
    }
}
