export class Attachment {
    constructor(public name:string,
                public data:string,
                public typeId:number,
                public id?:number) {}

    //attachment type id: 1-Link, 2-Table
}