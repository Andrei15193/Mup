import React from "react";
import join from "classnames";

import container from "dependency-container";
import ViewMode from "constants/view-mode";
import Bootstrap from "css/bootstrap";

export default class ViewModeSwitch extends React.PureComponent {
    constructor(props) {
        super(props);
        this._actions = container.parserActions;
        this._store = container.parserStore;
    }

    render() {
        return (
            <div class={Bootstrap.textRight}>
                <div class={join(Bootstrap.btnGroup, Bootstrap.btnGroupSm)} role="group">
                    <button
                        class={join(Bootstrap.btn, this._getClassFor(ViewMode.edit))}
                        onClick={() => this._actions.edit()}
                        disabled={this.props.disabled}>
                        Edit
                    </button>
                    <button
                        class={join(Bootstrap.btn, this._getClassFor(ViewMode.preview))}
                        onClick={() => this._actions.preview(this._store.text)}
                        disabled={this.props.disabled}>
                        Preview
                    </button>
                    <button
                        class={join(Bootstrap.btn, this._getClassFor(ViewMode.html))}
                        onClick={() => this._actions.html(this._store.text)}
                        disabled={this.props.disabled}>
                        HTML
                    </button>
                </div>
            </div>
        );
    }

    _getClassFor(viewMode) {
        return ((viewMode === this.props.view) ? Bootstrap.btnPrimary : Bootstrap.btnDefault);
    }
}