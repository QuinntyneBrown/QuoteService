export class Location { 
    public id:any;
    public name: string;
    public address: any;
    public city: any;
    public province: any;
    public postalCode: any;
    public longitude: any;
    public latitude: any;
    public isMasterOrigin: any;

    public fromJSON(data: any): Location {
        let location = new Location();
        location.name = data.name;
        location.address = data.address;
        location.city = data.city;
        location.province = data.province;
        location.postalCode = data.postalCode;
        location.longitude = data.longitude;
        location.latitude = data.latitude;
        location.isMasterOrigin = data.isMasterOrigin;
        return location;
    }
}
