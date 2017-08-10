import React from "react";
import join from "classnames";

import Page from "view/layout/page";

import Bootstrap from "css/bootstrap";
import Style from "css/style";

export default class Home extends React.Component {
    constructor(props) {
        super(props);
        this.state = { view: "pretty", html: "<p>Test</p>" };
    }

    showPretty() {
        this.setState({ view: "pretty" });
    }

    showRaw() {
        this.setState({ view: "raw" });
    }

    generateHtml() {
        this.setState({ html: "<p>some new HTML</p>" });
    }

    get previewContent() {
        if (this.state.view == "pretty")
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
        if (this.state.view == "pretty")
            return (
                <div class={Bootstrap.btnGroup} role="group">
                    <button class={join(Bootstrap.btn, Bootstrap.btnPrimary)} onClick={this.showPretty.bind(this)}>Pretty</button>
                    <button class={join(Bootstrap.btn, Bootstrap.btnDefault)} onClick={this.showRaw.bind(this)}>Raw</button>
                </div>
            );
        else
            return (
                <div class={Bootstrap.btnGroup} role="group">
                    <button class={join(Bootstrap.btn, Bootstrap.btnDefault)} onClick={this.showPretty.bind(this)}>Pretty</button>
                    <button class={join(Bootstrap.btn, Bootstrap.btnPrimary)} onClick={this.showRaw.bind(this)}>Raw</button>
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
                                <textarea class={join(Bootstrap.formControl, Style.parseContent)}></textarea>
                            </div>
                        </div>
                        <div class={Bootstrap.colMd6}>
                            {this.previewContent}
                        </div>
                    </div>
                    <div class={Bootstrap.row}>
                        <div class={join(Bootstrap.colMd6)}>
                            <button class={join(Bootstrap.btn, Bootstrap.btnPrimary)} onClick={this.generateHtml.bind(this)}>Generate</button>
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