import ViewMode from "constants/view-mode";

import EventHandler from "./event-handler";

export default class ParserStore {
    constructor(dispatcher) {
        this._view = ViewMode.edit;
        this._text = "";
        this._html = "";
        this._json = [];
        this._isLoading = false;

        this._propertyChanged = new EventHandler("propertyChanged", this);

        dispatcher.register(this._handle.bind(this));
    }

    get propertyChanged() {
        return this._propertyChanged;
    }

    get isLoading() {
        return this._isLoading;
    }

    _setIsLoading(value) {
        if (this._isLoading != value) {
            this._isLoading = value;
            this.propertyChanged.invoke(this, "isLoading");
        }
    }

    get text() {
        return this._text;
    }

    _setText(value) {
        if (this._text != value) {
            this._text = value;
            this.propertyChanged.invoke(this, "text");
        }
    }

    get view() {
        return this._view;
    }

    _setView(value) {
        if (this._view != value) {
            this._view = value;
            this.propertyChanged.invoke(this, "view");
        }
    }

    get html() {
        return this._html;
    }

    _setHtml(value) {
        if (this._html != value) {
            this._html = value;
            this.propertyChanged.invoke(this, "html");
        }
    }

    get json() {
        return this._json;
    }

    _setJson(value) {
        if (this._json != value) {
            this._json = value;
            this.propertyChanged.invoke(this, "json");
        }
    }

    _handle(action) {
        switch (action.type) {
            case "edit":
                this._setView(ViewMode.edit);
                break;

            case "saveText":
                this._setText(action.text);
                break;

            case "preview":
            case "html":
                this._parseText(action)
                break;
        }
    }

    _parseText(action) {
        this._setIsLoading(true);
        action
            .promise
            .then((response) => {
                const responseData = response.data;
                this._setHtml(responseData.html);
                this._setJson(responseData.json);
            })
            .catch((error) => {
                this._setHtml("<p>Something went wrong...</p>");
                this._setTextJson("Something went wrong...");
            })
            .then(() => {
                this._setIsLoading(false);
                this._setView(action.type == "preview" ? ViewMode.preview : ViewMode.html);
            });
    }

    _setTextJson(text) {
        this._setJson([{
            "type": "text",
            "content": text
        }]);
    }
};