export class Service { 
    public id:any;
    public name: string;
    public rate: any;
    public description: any;
    public imageUrl: any;

    public fromJSON(data: { name:string }): Service {
        let service = new Service();
        service.name = data.name;
        return service;
    }
}
