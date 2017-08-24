import React from "react";
import join from "classnames";

import ViewMode from "constants/view-mode";
import DependencyContainer from "dependency-container";
import Bootstrap from "css/bootstrap";

import JsonPreview from "./json-preview";

import Style from "./editor.css";

export default class Preview extends React.Component {
    constructor(props) {
        super(props);
        this._store = DependencyContainer.parserStore;
    }

    render() {
        return (
            <div class={join(Bootstrap.panel, Bootstrap.panelDefault, Style.preview)}>
                <div class={join(Bootstrap.panelBody)}>
                    <JsonPreview elements={this._store.json} />
                </div >
            </div >
        );
    }
};