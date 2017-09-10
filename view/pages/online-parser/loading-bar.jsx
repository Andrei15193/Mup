import React from "react";
import join from "classnames";

import Bootstrap from "css/bootstrap";

import Style from "./editor.css"

export default class LoadingBar extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        if (this.props.visible)
            return (
                <div class={join(Bootstrap.progress, Style.loadingBar)}>
                    <div
                        class={join(Bootstrap.progressBar, Bootstrap.progressBarStriped, Bootstrap.active)}
                        role="progressbar"
                        aria-valuenow="100"
                        aria-valuemin="0"
                        aria-valuemax="100" />
                </div >
            );
        else
            return (
                <div />
            );
    }
};