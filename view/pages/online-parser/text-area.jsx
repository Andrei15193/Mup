import React from "react";
import join from "classnames";

import Bootstrap from "css/bootstrap";

import Style from "./editor.css";

export default class TextArea extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class={join(Bootstrap.formGroup, Style.textArea)}>
                <textarea
                    class={Bootstrap.formControl}
                    disabled={this.props.disabled}
                    value={this.props.text}
                    onChange={(event) => this.props.onChange(event.target.value)} />
            </div>
        );
    }

    _onChange(event) {
        this.setState({ text: event.target.value });
    }

    _onBlur(event) {
        this._actions.save(event.target.value);
    }
};