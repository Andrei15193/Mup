import Axios from "axios";

const config = {
    headers: {
        "Content-Type": "application/json"
    }
};

export default class Request {
    static postAsync(url, data) {
        var requestData;
        if (typeof(data) === "string")
            requestData = JSON.stringify(data);
        else
            requestData = data;

        return Axios.post(url, requestData, config);
    }
};