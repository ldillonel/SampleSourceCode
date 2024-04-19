import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { User } from '../models/user';
import { UserCredentials } from '../models/user-credentials';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    private dataServiceUrl: string;

    constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl: string) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
        this.dataServiceUrl = `${baseUrl}api/auth`;
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(credentials: UserCredentials) {
        return this.http.post<User>(this.dataServiceUrl + '/login', credentials )
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

    register(credentials: UserCredentials) {
        return this.http.post<User>(this.dataServiceUrl + '/register', credentials )
            .pipe(map(user => {
                return user;
            }));
    }

    changePassword(oldCredentials: UserCredentials, newCredentials:UserCredentials) {
        let oldName = oldCredentials.username;
        let oldPass = oldCredentials.password;
        let newName = newCredentials.username;
        let newPass = newCredentials.password;

        let credentials = [
            { username: oldName, password: oldPass },
            { username: newName, password: newPass },
          ];
        let jsonCredentials = JSON.stringify(credentials);
        let headers = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        }
        return this.http.post<User>(this.dataServiceUrl + '/password', jsonCredentials , headers)
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }
}