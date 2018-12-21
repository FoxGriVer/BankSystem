var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { DataService } from './data.service';
import { Record } from './record';
import { Dates } from './filterRecords/dates';
var AppComponent = /** @class */ (function () {
    function AppComponent(dataService) {
        this.dataService = dataService;
        this.record = new Record();
        this.dates = new Dates();
        this.tableMode = true;
        this.reportMode = false;
        this.createMode = false;
        this.formPeriodMode = false;
    }
    AppComponent.prototype.ngOnInit = function () {
        this.loadRecords();
    };
    AppComponent.prototype.loadRecords = function () {
        var _this = this;
        this.dataService.getRecords()
            .subscribe(function (data) { return _this.records = data; });
    };
    AppComponent.prototype.getReport = function () {
        this.tableMode = false;
        this.formPeriodMode = true;
    };
    AppComponent.prototype.getFiltredRecordsAndBalance = function () {
        var _this = this;
        this.dataService.getFiltredRecords(this.dates)
            .subscribe(function (data) {
            _this.records = data.recordsForPeriod;
            _this.balanceForLastMonth = data.balanceForMonth;
            _this.balanceForPeriod = data.balanceForPeriod;
        });
        this.cancel();
        this.reportMode = true;
    };
    AppComponent.prototype.cancelReport = function () {
        this.cancel();
        this.loadRecords();
    };
    AppComponent.prototype.save = function () {
        var _this = this;
        if (this.record.id == null) {
            this.dataService.createRecord(this.record)
                .subscribe(function (data) { return _this.loadRecords(); });
        }
        else {
            this.dataService.updateRecord(this.record)
                .subscribe(function (data) { return _this.loadRecords(); });
        }
        this.cancel();
    };
    AppComponent.prototype.editRecord = function (_record) {
        console.log(_record);
        this.record = _record;
    };
    AppComponent.prototype.cancel = function () {
        this.record = new Record();
        this.tableMode = true;
        this.createMode = false;
        this.formPeriodMode = false;
        this.reportMode = false;
    };
    AppComponent.prototype.delete = function (_record) {
        var _this = this;
        this.dataService.deleteRecord(_record.id)
            .subscribe(function (data) { return _this.loadRecords(); });
    };
    AppComponent.prototype.add = function () {
        this.cancel();
        this.tableMode = false;
        this.createMode = true;
    };
    AppComponent = __decorate([
        Component({
            selector: 'app',
            templateUrl: './single.page.html',
            providers: [DataService]
        }),
        __metadata("design:paramtypes", [DataService])
    ], AppComponent);
    return AppComponent;
}());
export { AppComponent };
//# sourceMappingURL=app.component.js.map