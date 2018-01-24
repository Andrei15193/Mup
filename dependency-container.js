import { Dispatcher } from "flux";

import { axios as AxiosConfig } from "mup/config";
import { Api } from "./services/api";
import { EditorActions, ParseActions } from "./actions";
import { EditorStore, PreviewStore } from "./stores";

const dispatcher = new Dispatcher();

export const DependencyContainer = {
    get api() {
        return singleton("api", () => new Api(AxiosConfig));
    },

    get editorActions() {
        return new EditorActions(dispatcher, this.api);
    },

    get parserActions() {
        return new ParseActions(dispatcher, this.api);
    },

    get editorStore() {
        return new EditorStore(dispatcher);
    },

    get previewStore() {
        return new PreviewStore(dispatcher);
    }
}

var singletons = {};
function singleton(key, value, context) {
    var result;

    if (key in singletons)
        result = singletons[key];
    else {
        if (typeof (value) === "function")
            result = value.call(context);
        else
            result = value;
        singletons[key] = result;
    }

    return result;
};