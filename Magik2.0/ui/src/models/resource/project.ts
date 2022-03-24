export class Project {
    constructor(public id: number,
        public name: string,
        public description: string,
        public color: string|undefined = undefined) {}
}