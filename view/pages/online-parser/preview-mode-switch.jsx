import React from "react";
import join from "classnames";

import container from "dependency-container";
import ViewTypes from "constants/view-types";

import Bootstrap from "css/bootstrap";

const parserStore = container.parserStore;
const parserActions = container.parserActions;

export default class PreviewModeSwitch extends React.Component {
    constructor(props) {
        super(props);
        this.state = { view: parserStore.view };

        this._updateView = () => this.setState({ view: parserStore.view });
    }

    componentDidMount() {
        parserStore.viewChanged.add(this._updateView);
    }

    componentWillUnmount() {
        parserStore.viewChanged.remove(this._updateView);
    }

    render() {
        return (
            <div class={Bootstrap.btnGroup} role="group">
                {this._buttons}
            </div>
        );
    }

    get _buttons() {
        return this._items.map(
            (item) => (
                <button
                    key={item.viewType}
                    class={join(Bootstrap.btn, this._getClassFor(item.viewType))}
                    onClick={() => parserActions.show(item.viewType)}>
                    {item.label}
                </button>
            ), this);
    }

    get _items() {
        return [
            {
                viewType: ViewTypes.pretty,
                label: "Pretty"
            },
            {
                viewType: ViewTypes.raw,
                label: "Raw"
            }];
    }

    _getClassFor(view) {
        return ((view === this.state.view) ? Bootstrap.btnPrimary : Bootstrap.btnDefault);
    }
}