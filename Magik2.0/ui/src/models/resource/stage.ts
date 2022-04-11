export class Stage {
    constructor(public name:string,
                public description:string,
                public deadline:Date,
                public creationDate?:Date,
                public id?:number,
                public progress?:number,
                public color?:string) {}
}