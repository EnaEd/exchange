export class BaseModel {
  id: number;

  constructor(model: BaseModel) {
    this.id = model?.id;
  }
}
