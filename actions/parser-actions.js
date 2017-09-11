export default class ParserActions {
    constructor(dispatcher, request) {
        this._dispatcher = dispatcher;
        this._request = request;

        this._cache = { text: null, promise: null };
    }

    parse(text) {
        if (this._cache.text !== text) {
            const promise = this._request.postAsync("/api/creole", text);
            this._cache = {
                text,
                promise
            };
        }

        this._dispatcher.dispatch({
            type: "parse",
            promise: this._cache.promise
        });
    }

    save(text) {
        this._dispatcher.dispatch({
            type: "save",
            text: text
        });
    }
};