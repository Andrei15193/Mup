export default class ParserActions {
    constructor(dispatcher, request) {
        this._dispatcher = dispatcher;
        this._request = request;
    }

    edit() {
        this._dispatcher.dispatch({
            type: "edit"
        });
    }

    preview(text) {
        const promise = this._request.postAsync("/api/creole", text);
        this._dispatcher.dispatch({
            type: "preview",
            promise: promise
        });
    }

    html(text) {
        const promise = this._request.postAsync("/api/creole", text);
        this._dispatcher.dispatch({
            type: "html",
            promise: promise
        });
    }

    save(text) {
        this._dispatcher.dispatch({
            type: "saveText",
            text: text
        });
    }
};