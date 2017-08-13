import React from "react";
import join from "classnames";

import container from "dependency-container";

import Bootstrap from "css/bootstrap";

const parserActions = container.parserActions;

export default class ParseButton extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <button class={join(Bootstrap.btn, Bootstrap.btnPrimary)} onClick={() => parserActions.parse()}>Generate</button>
        );
    }
}