export class ProjectPart {
  constructor(public id: number,
              public name: string,
              public projectId: number,
              public description: string,
              public creationDate?: Date,
              public deadLine?: Date,
              public progress?: number) {
  }
}
