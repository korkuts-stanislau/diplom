export class Project {
    constructor(public id: number|undefined,
        public projectTypeId: number|undefined,
        public originalProjectId: number|undefined = undefined,
        public name: string,
        public description: string,
        public color: string|undefined = undefined) {}

    //project type id: 1-Private, 2-Readonly, 3-Copyable, 4-Public
}