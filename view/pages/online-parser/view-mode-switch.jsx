import React from "react";
import join from "classnames";

import container from "dependency-container";
import ViewMode from "constants/view-mode";
import Bootstrap from "css/bootstrap";

export default class ViewModeSwitch extends React.Component {
    constructor(props) {
        super(props);
        this._store = container.parserStore;
        this._actions = container.parserActions;

        this.state = { view: this._store.view, isLoading: this._store.isLoading };

        this._updateView = () => this.setState({ view: this._store.view });
        this._updateIsLoading = () => this.setState({ isLoading: this._store.isLoading });
    }

    componentDidMount() {
        this._store.viewChanged.add(this._updateView);
        this._store.isLoadingChanged.add(this._updateIsLoading);
    }

    componentWillUnmount() {
        this._store.viewChanged.remove(this._updateView);
        this._store.isLoadingChanged.remove(this._updateIsLoading);
    }

    render() {
        return (
            <div class={Bootstrap.textRight}>
                <div class={join(Bootstrap.btnGroup, Bootstrap.btnGroupSm)} role="group">
                    {this._buttons}
                </div>
            </div>
        );
    }

    get _buttons() {
        return this._items.map(
            (item) => (
                <button
                    key={item.viewMode}
                    class={join(Bootstrap.btn, this._getClassFor(item.viewMode))}
                    onClick={() => this._actions.show(item.viewMode)}
                    disabled={this.state.isLoading}>
                    {item.label}
                </button>
            ), this);
    }

    get _items() {
        return [
            {
                viewMode: ViewMode.edit,
                label: "Edit"
            },
            {
                viewMode: ViewMode.preview,
                label: "Preview"
            },
            {
                viewMode: ViewMode.html,
                label: "HTML"
            }
            // ,{
            //     viewMode: ViewMode.json,
            //     label: "JSON"
            // }
        ];
    }

    _getClassFor(viewMode) {
        return ((viewMode === this.state.view) ? Bootstrap.btnPrimary : Bootstrap.btnDefault);
    }
}