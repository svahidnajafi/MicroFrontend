export declare type NullableUserModel = UserModel | null;

export interface UserModel {
    id: string;
    name: string;
    email: string;
}

export interface UpsertUserRequest {
    id: string | null;
    name: string;
    email: string;
}