import React from "react";
import join from "classnames";

import container from "dependency-container";

import Bootstrap from "css/bootstrap";
import Style from "css/style";

const parserStore = container.parserStore;
const parserActions = container.parserActions;

export default class Editor extends React.Component {
    constructor(props) {
        super(props);
        this.state = { text: parserStore.text };
    }

    render() {
        return (
            <textarea
                class={join(Bootstrap.formControl, Style.parseContent, Style.verticalResize)}
                value={this.state.text}
                onChange={this._handleChange.bind(this)}
                onBlur={this._handleBlur.bind(this)} />
        );
    }

    _handleChange(event) {
        this.setState({ text: event.target.value });
    }

    _handleBlur(event) {
        parserActions.updateText(this.state.text);
    }
};