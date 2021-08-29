export interface IBaseModel {
    id?: number;
    rowVersion?: string;
    createdUserId?: number;
    createdDateTime?: string;
    updatedUserId?: number;
    updatedDateTime?: string;    
}

export class BaseModel implements IBaseModel {
    constructor() {}

    id?: number;
    rowVersion?: string;
    createdUserId?: number;
    createdDateTime?: string;
    updatedUserId?: number;
    updatedDateTime?: string;    
}