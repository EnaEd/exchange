export class PhotoModel {
  photoSource: string;
  description: string;
  categoryId: number;
  userId?: number;

  constructor(model: PhotoModel) {
    this.photoSource = model.photoSource;
    this.description = model.description;
    this.categoryId = model.categoryId;
    this.userId = model.userId;
  }
}
