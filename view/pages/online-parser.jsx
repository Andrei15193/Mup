import React from "react";
import Page from "view/layout/page";

import join from "classnames";

import Bootstrap from "css/bootstrap";
import Style from "css/style";

export default class Home extends React.Component {
    generateHtml() {
        console.log("generate HTML clicked");
    }

    render() {
        return (
            <Page title="Online Parser">
                <div class={join(Bootstrap.containerFluid, Style.noPadding)}>
                    <div class={Bootstrap.row}>
                        <div class={Bootstrap.colMd6}>
                            <h3>Input (Creole)</h3>
                            <div class={Bootstrap.formGroup}>
                                <textarea class={Bootstrap.formControl} rows="20"></textarea>
                            </div>
                            <div class={Bootstrap.textRight}>
                                <button class={join(Bootstrap.btn, Bootstrap.btnPrimary)} onClick={this.generateHtml}>Generate HTML</button>
                            </div>
                        </div>
                        <div class={Bootstrap.colMd6}>
                            <h3>Generated HTML</h3>
                            <pre><code>
                            </code></pre>
                        </div>
                    </div>
                </div>
            </Page>
        );
    }
};