import React from "react";
import join from "classnames";

import DependencyContainer from "dependency-container";
import Bootstrap from "css/bootstrap";

import Style from "./editor.css";

export default class Writer extends React.Component {
    constructor(props) {
        super(props);
        this._actions = DependencyContainer.parserActions;
        const store = DependencyContainer.parserStore;

        this.state = { text: store.text };
    }

    render() {
        return (
            <div class={join(Bootstrap.formGroup, Style.writer)}>
                <textarea
                    class={Bootstrap.formControl}
                    disabled={this.props.disabled}
                    value={this.state.text}
                    onChange={this._onChange.bind(this)}
                    onBlur={this._onBlur.bind(this)} />
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