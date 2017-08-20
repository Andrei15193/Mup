import ActionCategories from "constants/action-categories";
import ViewTypes from "constants/view-types";
import dependencyContainer from "dependency-container";

import EventHandler from "./event-handler";

export default class ParserStore {
    constructor(dispatcher, request) {
        this._request = request;

        this._view = ViewTypes.pretty;
        this._text = "";
        this._html = "";

        this.viewChanged = new EventHandler("viewChanged", this);
        this.htmlChanged = new EventHandler("htmlChanged", this);

        dispatcher.register(this._handle.bind(this));
    }

    get view() {
        return this._view;
    }

    _setView(value) {
        this._view = value;
        this.viewChanged.invoke(this);
    }

    get html() {
        return this._html;
    }

    _setHtml(value) {
        this._html = value;
        this.htmlChanged.invoke(this);
    }

    get text() {
        return this._text;
    }

    _handle(action) {
        switch (action.category) {
            case ActionCategories.parserView:
                this._setView(action.data);
                break;

            case ActionCategories.parserText:
                this._text = action.data;
                break;

            case ActionCategories.parserParse:
                this._setHtml("Fetching some HTML for you, might take a bit...");
                this._request
                    .postAsync("/api/creole", this._text)
                    .then((function (response) {
                        this._setHtml(response.data);
                    }).bind(this))
                    .catch((function (error) {
                        this._setHtml("Something went wrong...");
                    }).bind(this));
                break;
        }
    }
};