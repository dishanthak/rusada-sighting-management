import { BaseModel } from './../shared/models';

export interface ISighting extends BaseModel {
  make: string;
  model: string;
  registration: string;
  location: string;
  dateAndTime: string;
  aircraftPhotoName: string;
  aircraftPhoto: any;
}
