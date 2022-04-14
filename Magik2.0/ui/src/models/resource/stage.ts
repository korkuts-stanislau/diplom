import { Attachment } from "./attachment";

export class Stage {
    constructor(public name:string,
                public description:string,
                public deadline:Date,
                public attachments:Attachment[],
                public creationDate?:Date,
                public id?:number,
                public progress?:number,
                public color?:string) {}
}