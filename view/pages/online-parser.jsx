import React from "react";
import join from "classnames";

import DependencyContainer from "dependency-container";
import Page from "view/layout/page";

import Editor from "./online-parser/editor";

export default class OnlineParser extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Page title="Online Parser">
                <Editor />
            </Page>
        );
    }
};