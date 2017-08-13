import ActionCategories from "constants/action-categories";
import ViewTypes from "constants/view-types";

import Action from "./action";

export default class ParserActions {
    constructor(dispatcher) {
        this._dispatcher = dispatcher;
    }

    showPretty() {
        this._dispatcher.dispatch(new Action(ActionCategories.parserView, ViewTypes.pretty));
    }

    showRaw() {
        this._dispatcher.dispatch(new Action(ActionCategories.parserView, ViewTypes.raw));
    }

    parse(text) {
        this._dispatcher.dispatch(new Action(ActionCategories.parserProcess, text));
    }
};