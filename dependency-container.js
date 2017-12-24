import { Dispatcher } from "flux";

import Config from "mup/config";
import Request from "mup/actions/request";
import EditorActions from "mup/actions/editor-actions";
import ParseActions from "mup/actions/parse-actions";
import EditorStore from "mup/stores/editor-store";
import PreviewStore from "mup/stores/preview-store";

export default {
    get request() {
        return singleton("request", () => new Request(Config.axios));
    },

    get dispatcher() {
        return singleton("dispatcher", () => new Dispatcher());
    },

    get editorActions() {
        return new EditorActions(this.dispatcher, this.request);
    },

    get parserActions() {
        return new ParseActions(this.dispatcher, this.request);
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