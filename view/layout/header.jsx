import React from "react";

import Bootstrap from "css/bootstrap";
import Style from "css/style";
import Logo from "images/logo";

import { PrimaryLabel } from "view/common/bootstrap";

export default class Header extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        document.title = "Mup - Markup for Everyone";
        return (
            <h1 class={Style.content}>
                <img src={Logo} alt="logo" /> <abbr title="MarkUp Parser">Mup</abbr> <small>Markup for Everyone</small> <PrimaryLabel text="Preview" />
            </h1>
        );
    }
};