import { Record } from '../record';

export class ReportResponse {
    constructor(
        public balanceForMonth?: number,
        public balanceForPeriod?: number,
        public recordsForPeriod?: Record[]
    ) {}
}