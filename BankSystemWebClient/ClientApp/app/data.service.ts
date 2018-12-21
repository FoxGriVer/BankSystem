import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Record } from "./record";
import { Dates } from './filterRecords/dates';

@Injectable()
export class DataService {

    private url = "api/RecordSPA";

    constructor(private http: HttpClient) {

    }

    public getFiltredRecords(dates: Dates) {    
        return this.http.post(this.url + '/GetFiltredRecords', dates);       
    }

    public getRecords() {                
        return this.http.get(this.url);
    }

    public createRecord(newRecord: Record) {
        return this.http.post(this.url, newRecord);
    }

    public updateRecord(updatedRecord: Record) {
        return this.http.put(this.url + '/' + updatedRecord.id, updatedRecord);
    }

    public deleteRecord(id: number) {
        return this.http.delete(this.url + '/' + id);
    }

}