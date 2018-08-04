import EventEmitter from "events";

import { ActionState } from "./action-data";

export class EditorStore {
    constructor(dispatcher) {
        const sessionStorageId = "EditorStore";
        const propertyChangedEventHandler = new EventHandler("propertyChanged");
        let handleId = null;

        this.text = "";
        this.onPropertyChanged = propertyChangedEventHandler.subscriber;

        const handle = function (data) {
            if (data.action === "update" && data.state === ActionState.completed) {
                this.text = data.text;
                if (sessionStorage)
                    sessionStorage.setItem(sessionStorageId, this.text);
                propertyChangedEventHandler.invoke("text");
            }
        }.bind(this);

        propertyChangedEventHandler
            .subscriptions
            .onSubscribed
            .add(args => {
                if (args.listenerCount === 1 && handleId === null) {
                    if (sessionStorage)
                        this.text = (sessionStorage.getItem(sessionStorageId) || "");
                    else
                        this.text = "";
                    propertyChangedEventHandler.invoke("text");

                    handleId = dispatcher.register(handle);
                }
            });
        propertyChangedEventHandler
            .subscriptions
            .onUnsubscribed
            .add(args => {
                if (args.listenerCount === 0) {
                    dispatcher.unregister(handleId);
                    handleId = null;
                }
            });
    }
};

export class PreviewStore {
    constructor(dispatcher) {
        let handleId = null;
        const propertyChangedEventHandler = new EventHandler("propertyChanged");

        this.json = [];
        this.html = "";
        this.isBusy = false;
        this.onPropertyChanged = propertyChangedEventHandler.subscriber;

        const handle = function (data) {
            if (data.action === "parse")
                switch (data.state) {
                    case ActionState.excuting:
                        this.isBusy = true;
                        this.json = [];
                        this.html = "";
                        propertyChangedEventHandler.invoke("json");
                        propertyChangedEventHandler.invoke("html");
                        propertyChangedEventHandler.invoke("isBusy");
                        break;

                    case ActionState.completed:
                    case ActionState.faulted:
                    case ActionState.canceled:
                        this.json = data.json;
                        this.html = data.html;
                        this.isBusy = false;
                        propertyChangedEventHandler.invoke("json");
                        propertyChangedEventHandler.invoke("html");
                        propertyChangedEventHandler.invoke("isBusy");
                        break;
                }
        }.bind(this);

        propertyChangedEventHandler
            .subscriptions
            .onSubscribed
            .add(args => {
                if (args.listenerCount === 1) {
                    if (handleId === null)
                        handleId = dispatcher.register(handle);
                }
            });
        propertyChangedEventHandler
            .subscriptions
            .onUnsubscribed
            .add(args => {
                if (args.listenerCount === 0) {
                    dispatcher.unregister(handleId);
                    handleId = null;
                }
            });
    }
};

class EventHandler {
    constructor(eventName) {
        const onSubscribedEventSymbol = Symbol("subscribed");
        const onUnsubscribedEventSymbol = Symbol("unsubscribed");

        const eventSymbol = Symbol(eventName || "event");
        const eventEmitter = new EventEmitter();

        this.subscriber = {
            add: function (handler) {
                eventEmitter.on(eventSymbol, handler);
                eventEmitter.emit(onSubscribedEventSymbol, {
                    listenerCount: eventEmitter.listenerCount(eventSymbol)
                });
            },
            remove: function (handler) {
                eventEmitter.removeListener(eventSymbol, handler);
                eventEmitter.emit(onUnsubscribedEventSymbol, {
                    listenerCount: eventEmitter.listenerCount(eventSymbol)
                });
            }
        };
        this.subscriptions = {
            get count() {
                return eventEmitter.listenerCount(eventSymbol);
            },
            onSubscribed: {
                add: function (handler) {
                    eventEmitter.on(onSubscribedEventSymbol, handler);
                },
                remove: function (handler) {
                    eventEmitter.removeListener(onSubscribedEventSymbol, handler);
                }
            },
            onUnsubscribed: {
                add: function (handler) {
                    eventEmitter.on(onUnsubscribedEventSymbol, handler);
                },
                remove: function (handler) {
                    eventEmitter.removeListener(onUnsubscribedEventSymbol, handler);
                }
            }
        };

        this.invoke = function (...args) {
            eventEmitter.emit(eventSymbol, ...args);
        };
    }
}