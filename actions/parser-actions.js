import ActionCategories from "constants/action-categories";
import ViewTypes from "constants/view-types";

import Action from "./action";

export default class ParserActions {
    constructor(dispatcher) {
        this._dispatcher = dispatcher;
    }

    show(view) {
        this._dispatcher.dispatch(new Action(ActionCategories.parserView, view));
    }

    updateText(text) {
        this._dispatcher.dispatch(new Action(ActionCategories.parserText, text));
    }

    parse() {
        this._dispatcher.dispatch(new Action(ActionCategories.parserParse));
    }
};