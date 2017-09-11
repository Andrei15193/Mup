import React from "react";
import join from "classnames";

import ViewMode from "constants/view-mode";
import Bootstrap from "css/bootstrap";

export default class ViewModeSwitch extends React.PureComponent {
    constructor(props) {
        super(props);
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
        ].map(item => (
            <button
                key={item.viewMode}
                class={join(Bootstrap.btn, this._getClassFor(item.viewMode))}
                onClick={() => this.props.onViewChanged(item.viewMode)}
                disabled={this.props.disabled} >
                {item.label}
            </button>
        ));
    }

    _getClassFor(viewMode) {
        return ((viewMode === this.props.view) ? Bootstrap.btnPrimary : Bootstrap.btnDefault);
    }
}