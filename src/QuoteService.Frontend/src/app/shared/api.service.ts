import { fetch } from "../utilities";
import { Question, Survey } from "../models";
import { environment } from "../environment";

export class ApiService {
    constructor(private _fetch = fetch) { }

    private static _instance: ApiService;

    public static get Instance() {
        this._instance = this._instance || new this();
        return this._instance;
    }

    public getSurvey(): Promise<Survey> {        
        return this._fetch({ url: `/api/survey/getbyuniqueId?uniqueId=${environment.surveyUniqueId}`, authRequired: false }).then((results: string) => {
            return (JSON.parse(results) as { survey: Survey }).survey;
        });
    }   

    public addSurveyResult(surveyResult): Promise<any> {
        return this._fetch({ url: `/api/surveyresult/add`, authRequired: false, method: "POST", data: { surveyResult, surveyUniqueId:environment.surveyUniqueId } }).then((results: string) => {
            return true;
        });
    } 
}
