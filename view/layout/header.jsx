import React from "react";
import join from "classnames";

import Bootstrap from "css/bootstrap";
import Logo from "images/logo";

import { PrimaryLabel } from "view/common/bootstrap";
import Content from "view/layout/content";

export default class Header extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        document.title = "Mup - Markup for Everyone";
        return (
            <Content>
                <h1>
                    <img src={Logo} alt="logo" /> <abbr title="MarkUp Parser">Mup</abbr> <small>Markup for Everyone</small> <PrimaryLabel text="Preview" />
                </h1>
            </Content>
        );
    }
};