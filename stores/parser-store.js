import ActionCategories from "constants/action-categories";
import ViewMode from "constants/view-mode";
import dependencyContainer from "dependency-container";

import EventHandler from "./event-handler";

export default class ParserStore {
    constructor(dispatcher, request) {
        this._request = request;

        this._view = ViewMode.edit;
        this._text = "";
        this._html = "";
        this._json = [];
        this._isLoading = false;
        this._textChanged = true;

        this.viewChanged = new EventHandler("viewChanged", this);
        this.htmlChanged = new EventHandler("htmlChanged", this);
        this.jsonChanged = new EventHandler("jsonChanged", this);
        this.isLoadingChanged = new EventHandler("isLoadingChanged", this);

        dispatcher.register(this._handle.bind(this));
    }

    get view() {
        return this._view;
    }

    _setView(value) {
        if (value !== ViewMode.edit && this._textChanged && !this.isLoading) {
            this.isLoading = true;

            this._request
                .postAsync("/api/creole", this._text)
                .then((function (response) {
                    this._setHtml(response.data.html);
                    this._setJson(response.data.json);
                }).bind(this))
                .catch((function (error) {
                    this._setHtml("Something went wrong...");
                    this._setTextJson("Something went wrong...");
                }).bind(this))
                .then((function () {
                    this._textChanged = false;
                    this.isLoading = false;
                    this._setView(value);
                }).bind(this));
        }
        else {
            this._view = value;
            this.viewChanged.invoke(this);
        }
    }

    get html() {
        return this._html;
    }

    _setHtml(value) {
        this._html = value;
        this.htmlChanged.invoke(this);
    }

    get json() {
        return this._json;
    }

    _setJson(value) {
        this._json = value;
        this.jsonChanged.invoke(this);
    }

    get text() {
        return this._text;
    }

    _setText(value) {
        if (this._text !== value) {
            this._text = value;
            this._textChanged = true;
        }
    }

    get isLoading() {
        return this._isLoading;
    }

    set isLoading(value) {
        this._isLoading = value;
        this.isLoadingChanged.invoke(this);
    }

    _handle(action) {
        switch (action.category) {
            case ActionCategories.parserView:
                this._setView(action.data);
                break;

            case ActionCategories.parserText:
                this._setText(action.data);
                break;
        }
    }

    _setTextJson(text) {
        this._setJson([{
            "type": "text",
            "content": text
        }]);
    }
};