import { Component, OnInit } from '@angular/core';

import { DataService } from './data.service';
import { Record } from './record';
import { Dates } from './filterRecords/dates';
import { ReportResponse } from './filterRecords/reportResponse';

@Component({
    selector: 'app',
    templateUrl: './single.page.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {
    
    record: Record = new Record();
    records: Record[];
    dates: Dates = new Dates();
    balanceForLastMonth: number;
    balanceForPeriod: number;

    tableMode: boolean = true;    
    reportMode: boolean = false;
    createMode: boolean = false;
    formPeriodMode: boolean = false;
    
    
    constructor(private dataService: DataService) {

    }

    ngOnInit() {
        this.loadRecords();
    }

    loadRecords() {
        this.dataService.getRecords()
            .subscribe((data: Record[]) => this.records = data);
    }

    public getReport() {        
        this.tableMode = false;
        this.formPeriodMode = true;
    }

    public getFiltredRecordsAndBalance() {
        this.dataService.getFiltredRecords(this.dates)
            .subscribe((data: ReportResponse) => {
                this.records = data.recordsForPeriod;
                this.balanceForLastMonth = data.balanceForMonth;
                this.balanceForPeriod = data.balanceForPeriod;
            });
        this.cancel();
        this.reportMode = true;
    }

    public cancelReport() {
        this.cancel();
        this.loadRecords();
    }

    public save() {
        if (this.record.id == null) {
            this.dataService.createRecord(this.record)
                .subscribe(data => this.loadRecords());
        } else {
            this.dataService.updateRecord(this.record)
                .subscribe(data => this.loadRecords());
        }
        this.cancel();
    }

    public editRecord(_record: Record) {
        console.log(_record);
        this.record = _record;
    }

    public cancel() {
        this.record = new Record();
        this.tableMode = true;
        this.createMode = false;
        this.formPeriodMode = false;
        this.reportMode = false;
    }

    public delete(_record: Record) {
        this.dataService.deleteRecord(_record.id)
            .subscribe(data => this.loadRecords());
    }

    public add() {
        this.cancel();
        this.tableMode = false;
        this.createMode = true;
    }

}