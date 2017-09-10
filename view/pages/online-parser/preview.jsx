import React from "react";
import join from "classnames";

import Bootstrap from "css/bootstrap";

import JsonPreview from "./json-preview";
import Style from "./editor.css";

export default class Preview extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class={join(Bootstrap.panel, Bootstrap.panelDefault, Style.preview)}>
                <div class={join(Bootstrap.panelBody)}>
                    <JsonPreview elements={this.props.json} />
                </div >
            </div >
        );
    }
};