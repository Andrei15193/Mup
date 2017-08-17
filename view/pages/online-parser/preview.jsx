import React from "react";
import join from "classnames";

import container from "dependency-container";
import ViewTypes from "constants/view-types"

import Bootstrap from "css/bootstrap";
import Style from "css/style";

const parserStore = container.parserStore;

export default class Preview extends React.Component {
    constructor(props) {
        super(props);
        this.state = { view: parserStore.view, html: parserStore.html };

        this._updateView = () => this.setState({ view: parserStore.view });
        this._updateHtml = () => this.setState({ html: parserStore.html });
    }

    componentDidMount() {
        parserStore.viewChanged.add(this._updateView);
        parserStore.htmlChanged.add(this._updateHtml);
    }

    componentWillUnmount() {
        parserStore.viewChanged.remove(this._updateView);
        parserStore.htmlChanged.remove(this._updateHtml);
    }

    render() {
        if (this.state.view == ViewTypes.pretty)
            return (
                <div class={join(Bootstrap.panel, Bootstrap.panelDefault, Style.parseContent, Style.noMargin)}>
                    <div class={Bootstrap.panelBody}>
                        <div class={Style.preview} dangerouslySetInnerHTML={{ __html: this.state.html }} />
                    </div>
                </div>
            );
        else {
            var rawHtml = this.state.html.replace(new RegExp("<span class=\"keyword\">(\\w+)</span>", "mg"), "$1");
            return (
                <pre class={Style.parseContent}><code>{rawHtml}</code></pre>
            );
        }
    }
};