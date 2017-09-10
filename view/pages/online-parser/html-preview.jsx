import React from "react";
import join from "classnames";

import ViewMode from "constants/view-mode"
import DependencyContainer from "dependency-container";

import Style from "./editor.css";

export default class HtmlPreview extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <pre class={Style.preview}><code>
                {this.props.html}
            </code></pre>
        );
    }
};