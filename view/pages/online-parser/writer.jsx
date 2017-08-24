import React from "react";
import join from "classnames";

import DependencyContainer from "dependency-container";
import Bootstrap from "css/bootstrap";

import Style from "./editor.css";

export default class Writer extends React.Component {
    constructor(props) {
        super(props);
        this._store = DependencyContainer.parserStore;
        this._actions = DependencyContainer.parserActions;

        this.state = { text: this._store.text, isLoading: this._store.isLoading };

        this._updateLoading = (() => this.setState({ isLoading: this._store.isLoading })).bind(this);
    }

    componentDidMount() {
        this._store.isLoadingChanged.add(this._updateLoading);
    }

    componentWillUnmount() {
        this._store.isLoadingChanged.remove(this._updateLoading);
    }

    render() {
        return (
            <div class={join(Bootstrap.formGroup, Style.writer)}>
                <textarea
                    class={Bootstrap.formControl}
                    disabled={this.state.isLoading}
                    value={this.state.text}
                    onChange={this._handleChange.bind(this)}
                    onBlur={this._handleBlur.bind(this)} />
            </div>
        );
    }

    _handleChange(event) {
        this.setState({ text: event.target.value });
    }

    _handleBlur(event) {
        this._actions.updateText(this.state.text);
    }
};