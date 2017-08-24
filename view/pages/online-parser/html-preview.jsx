import React from "react";
import join from "classnames";

import ViewMode from "constants/view-mode"
import DependencyContainer from "dependency-container";

import Style from "./editor.css";

export default class HtmlPreview extends React.Component {
    constructor(props) {
        super(props);
        this._store = DependencyContainer.parserStore;

        this.state = { html: this._store.html };

        this._updateHtml = (() => this.setState({ html: this._store.html })).bind(this);
    }

    componentDidMount() {
        this._store.htmlChanged.add(this._updateHtml);
    }

    componentWillUnmount() {
        this._store.htmlChanged.remove(this._updateHtml);
    }

    render() {
        return (
            <pre class={Style.preview}><code>
                {this.state.html}
            </code></pre>
        );
    }
};