import Axios from "axios";

const config = {
    headers: {
        "Content-Type": "application/json"
    }
};

export default class Request {
    static postAsync(url, data) {
        return Axios.post(url, data, config);
    }
};