import EventEmitter from "events";

import { ActionState } from "mup/action-data";

export default class EditorStore extends EventEmitter {
    constructor(dispatcher) {
        super();
        this._text = "";
        dispatcher.register(this.handle.bind(this));
    }

    get text() {
        return this._text;
    }

    handle(data) {
        if (data.action === "update" && data.state === ActionState.completed) {
            this._text = data.text;
            this.emit("propertyChanged", "text");
        }
    }
};