import EventEmitter from "events";

import { ActionState } from "mup/action-data";

export default class PreviewStore extends EventEmitter {
    constructor(dispatcher) {
        super();
        this._json = [];
        this._html = "";
        this._isBusy = false;
        dispatcher.register(this.handle.bind(this));
    }

    get json() {
        return this._json;
    }

    get html() {
        return this._html;
    }

    get isBusy() {
        return this._isBusy;
    }

    handle(data) {
        if (data.action === "parse")
            switch (data.state) {
                case ActionState.excuting:
                    this._isBusy = true;
                    this._json = [];
                    this._html = "";
                    this.emit("propertyChanged", "json");
                    this.emit("propertyChanged", "html");
                    this.emit("propertyChanged", "isBusy");
                    break;

                case ActionState.completed:
                case ActionState.faulted:
                case ActionState.canceled:
                    this._json = data.json;
                    this._html = data.html;
                    this._isBusy = false;
                    this.emit("propertyChanged", "json");
                    this.emit("propertyChanged", "html");
                    this.emit("propertyChanged", "isBusy");
                    break;
            }
    }
};