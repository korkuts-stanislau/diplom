export class Profile {
  constructor(public id: number,
              public username: string,
              public description: string,
              public icon?: string,
              public picture?: string) {
  }
}
