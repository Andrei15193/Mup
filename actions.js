import { ActionState, ActionData } from "./action-data";

export class EditorActions {
    constructor(dispatcher) {
        this.dispatcher = dispatcher;
    }

    updateText(text) {
        this.dispatcher.dispatch(
            new ActionData("update", ActionState.completed, {
                text: text
            }));
    }
};

export class ParseActions {
    constructor(dispatcher, api) {
        this.dispatcher = dispatcher;
        this.api = api;
        this.cache = {
            text: null,
            json: null,
            html: null
        };
    }

    parseAsync(text) {
        if (this.cache.text !== text) {
            this.dispatcher.dispatch(new ActionData("parse", ActionState.excuting));
            return this.api.postAsync("/api/creole", text)
                .then(response =>
                    new ActionData("parse", ActionState.completed, {
                        json: response.data.json,
                        html: response.data.html
                    }))
                .catch(() =>
                    new ActionData("parse", ActionState.faulted, {
                        json: "Something went wrong...",
                        html: "<p>Something went wrong...</p>"
                    }))
                .then(actionData => {
                    this.cache = {
                        text: text,
                        json: actionData.json,
                        html: actionData.html
                    };
                    this.dispatcher.dispatch(actionData);
                });
        }
        else {
            this.dispatcher.dispatch(
                new ActionData("parse", ActionState.completed, {
                    json: this.cache.json,
                    html: this.cache.html
                }));
            return Promise.resolve();
        }
    }
};