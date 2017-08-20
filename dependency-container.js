import { Dispatcher } from "flux";

import apiConfig from "config/api";

import ParserActions from "./actions/parser-actions";
import ParserStore from "./stores/parser-store";
import Request from "./request";

export default {
    get request() {
        return singleton("request", () => new Request(apiConfig));
    },

    get dispatcher() {
        return singleton("dispatcher", () => new Dispatcher());
    },

    get parserActions() {
        return new ParserActions(this.dispatcher);
    },

    get parserStore() {
        return singleton("parser-store", () => new ParserStore(this.dispatcher, this.request), this);
    }
}

var singletons = {};
function singleton(key, value, context) {
    var result;

    if (key in singletons)
        result = singletons[key];
    else {
        if (typeof (value) === "function")
            result = value.apply(context);
        else
            result = value;
        singletons[key] = result;
    }

    return result;
};