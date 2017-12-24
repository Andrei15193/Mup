import Axios from "axios";

export default class Request {
    constructor(apiConfig) {
        this._axios = Axios.create(apiConfig);
    }

    postAsync(url, data) {
        var requestData;
        if (typeof (data) === "string")
            requestData = JSON.stringify(data);
        else
            requestData = data;

        return this._axios.post(url, requestData);
    }
};