import React from "react";
import join from "classnames";

import Page from "view/layout/page";
import ViewTypes from "constants/view-types"
import { parserActions, parserStore } from "dependency-container";

import Bootstrap from "css/bootstrap";
import Style from "css/style";

export default class OnlineParser extends React.Component {
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

    get previewContent() {
        if (this.state.view == ViewTypes.pretty)
            return (
                <div class={join(Bootstrap.panel, Bootstrap.panelDefault, Style.parseContent, Style.noMargin)}>
                    <div class={Bootstrap.panelBody}>
                        <div dangerouslySetInnerHTML={{ __html: this.state.html }} />
                    </div>
                </div>
            );
        else
            return (
                <pre class={Style.parseContent}><code>{this.state.html}</code></pre>
            );
    }

    get previewButtonGroup() {
        if (this.state.view == ViewTypes.pretty)
            return (
                <div class={Bootstrap.btnGroup} role="group">
                    <button class={join(Bootstrap.btn, Bootstrap.btnPrimary)} onClick={() => parserActions.showPretty()}>Pretty</button>
                    <button class={join(Bootstrap.btn, Bootstrap.btnDefault)} onClick={() => parserActions.showRaw()}>Raw</button>
                </div>
            );
        else
            return (
                <div class={Bootstrap.btnGroup} role="group">
                    <button class={join(Bootstrap.btn, Bootstrap.btnDefault)} onClick={() => parserActions.showPretty()}>Pretty</button>
                    <button class={join(Bootstrap.btn, Bootstrap.btnPrimary)} onClick={() => parserActions.showRaw()}>Raw</button>
                </div>
            );
    }

    render() {
        return (
            <Page title="Online Parser">
                <div class={join(Bootstrap.containerFluid, Style.noPadding)}>
                    <div class={Bootstrap.row}>
                        <div class={Bootstrap.colMd6}>
                            <h3>Creole</h3>
                        </div>
                        <div class={Bootstrap.colMd6}>
                            <h3>Preview</h3>
                        </div>
                    </div>

                    <div class={Bootstrap.row}>
                        <div class={Bootstrap.colMd6}>
                            <div class={Bootstrap.formGroup}>
                                <textarea class={join(Bootstrap.formControl, Style.parseContent, Style.verticalResize)}></textarea>
                            </div>
                        </div>
                        <div class={Bootstrap.colMd6}>
                            {this.previewContent}
                        </div>
                    </div>
                    <div class={Bootstrap.row}>
                        <div class={join(Bootstrap.colMd6)}>
                            <button class={join(Bootstrap.btn, Bootstrap.btnPrimary)} onClick={() => parserActions.parse("<p> this is a test </p>")}>Generate</button>
                        </div>
                        <div class={join(Bootstrap.colMd6)}>
                            {this.previewButtonGroup}
                        </div>
                    </div>
                </div>
            </Page>
        );
    }
};