import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SystemSetting } from './interface/system-setting';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private dotNetDebuggerPath:string = 'http://localhost:62746/api/';
  private servicePath:string = 'http://localhost:5000/api/';

  private http = inject(HttpClient);
  private BASE_PATH:string = environment.apiUrl;

  constructor() {}


    getSystemSettings():Observable<any> {
      return this.http.get<SystemSetting[]>(this.BASE_PATH + 'SystemSetting');
  }


  
}
