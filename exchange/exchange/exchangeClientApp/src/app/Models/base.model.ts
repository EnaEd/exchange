export class BaseModel {
  id: number;
  errors: string[];
  constructor(model: BaseModel) {
    this.id = model.id;
    this.errors = model.errors;
  }
}
