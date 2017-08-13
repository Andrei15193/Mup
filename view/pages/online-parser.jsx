import React from "react";
import join from "classnames";

import Page from "view/layout/page";

import Bootstrap from "css/bootstrap";
import Style from "css/style";

import Editor from "./online-parser/editor";
import Preview from "./online-parser/preview";
import PreviewModeSwitch from "./online-parser/preview-mode-switch";
import ParseButton from "./online-parser/parse-button";

export default class OnlineParser extends React.Component {
    constructor(props) {
        super(props);
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
                                <Editor />
                            </div>
                        </div>
                        <div class={Bootstrap.colMd6}>
                            <Preview />
                        </div>
                    </div>

                    <div class={Bootstrap.row}>
                        <div class={join(Bootstrap.colMd6)}>
                            <ParseButton />
                        </div>
                        <div class={join(Bootstrap.colMd6)}>
                            <PreviewModeSwitch />
                        </div>
                    </div>
                </div>
            </Page>
        );
    }
};