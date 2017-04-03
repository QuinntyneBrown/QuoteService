export class Location { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Location {
        let location = new Location();
        location.name = data.name;
        return location;
    }
}
