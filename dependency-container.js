import { Dispatcher } from "flux";

import { axios as AxiosConfig } from "mup/config";
import { Api } from "./services/api";
import { EditorActions, ParseActions } from "./actions";
import { EditorStore, PreviewStore } from "./stores";

export const DependencyContainer = {
    get api() {
        return singleton("api", () => new Api(AxiosConfig));
    },

    get dispatcher() {
        return singleton("dispatcher", () => new Dispatcher());
    },

    get editorActions() {
        return new EditorActions(this.dispatcher, this.api);
    },

    get parserActions() {
        return new ParseActions(this.dispatcher, this.api);
    },

    get editorStore() {
        return singleton("editor-store", () => new EditorStore(this.dispatcher), this);
    },

    get previewStore() {
        return singleton("preview-store", () => new PreviewStore(this.dispatcher), this);
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