export class Quote { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Quote {
        let quote = new Quote();
        quote.name = data.name;
        return quote;
    }
}
