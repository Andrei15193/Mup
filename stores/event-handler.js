import EventEmitter from "events";

export default class EventHandler extends EventEmitter {
    constructor(eventName, owner) {
        super();

        this._eventName = eventName;
        this._owner = owner;
    }

    add(listener) {
        this.on(this._eventName, listener);
    }

    remove(listener) {
        this.removeListener(this._eventName, listener);
    }

    invoke(owner, eventArgs) {
        if (this._owner !== owner)
            throw new Error("Only the owner can emit an event.");

        this.emit(this._eventName, eventArgs);
    }
};