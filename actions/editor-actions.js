import { ActionData, ActionState } from "mup/action-data";

export default class EditorActions {
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