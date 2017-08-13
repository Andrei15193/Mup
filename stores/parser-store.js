import ActionCategories from "constants/action-categories";
import ViewTypes from "constants/view-types";

import EventHandler from "./event-handler";

export default class ParserStore {
    constructor(dispatcher) {
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

    _handle(action) {
        switch (action.category) {
            case ActionCategories.parserView:
                this._setView(action.data);
                break;

            case ActionCategories.parserText:
                this._text = action.data;
                break;

            case ActionCategories.parserParse:
                console.log("Do async call for parse");
                this._setHtml(this._text);
                break;
        }
    }
};