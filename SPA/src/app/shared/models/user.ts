import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    gender: string;
    age: string;
    knownAs: string;
    created: Date;
    lastActive: Date;
    introduction?: string;
    lookingFor?: string;
    interests?: string;
    city: string;
    country: string;
    photos: Photo[];
    photoUrl: Photo['url'];
}
