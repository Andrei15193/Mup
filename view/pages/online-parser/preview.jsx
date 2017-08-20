import React from "react";
import join from "classnames";

import container from "dependency-container";
import ViewTypes from "constants/view-types"

import Bootstrap from "css/bootstrap";
import Style from "css/style";

import JsonPreview from "./json-preview";

const parserStore = container.parserStore;

export default class Preview extends React.Component {
    constructor(props) {
        super(props);
        this.state = { view: parserStore.view, html: parserStore.html, json: parserStore.json };

        this._updateView = (() => this.setState({ view: parserStore.view })).bind(this);
        this._updateHtml = (() => this.setState({ html: parserStore.html })).bind(this);
        this._updateJson = (() => this.setState({ json: parserStore.json })).bind(this);
    }

    componentDidMount() {
        parserStore.viewChanged.add(this._updateView);
        parserStore.htmlChanged.add(this._updateHtml);
        parserStore.jsonChanged.add(this._updateJson);
    }

    componentWillUnmount() {
        parserStore.viewChanged.remove(this._updateView);
        parserStore.htmlChanged.remove(this._updateHtml);
        parserStore.jsonChanged.remove(this._updateJson);
    }

    render() {
        if (this.state.view == ViewTypes.pretty)
            return (
                <div class={join(Bootstrap.panel, Bootstrap.panelDefault, Style.parseContent, Style.noMargin)}>
                    <div class={Bootstrap.panelBody}>
                        <JsonPreview elements={this.state.json} />
                    </div>
                </div>
            );
        else
            return (
                <pre class={Style.parseContent}><code>
                    {this.state.html}
                </code></pre>
            );
    }
};