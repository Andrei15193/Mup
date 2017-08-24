import React from "react";
import join from "classnames";

import Bootstrap from "css/bootstrap";

import Style from "./page.css";

export default class Footer extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        document.title = "Mup - Markup for Everyone";
        return (
            <p class={join(Bootstrap.textCenter, Style.footer)}>
                Mup Copyright &copy; 2017 Andrei Fangli<br />
                <a href="http://www.mup-project.net/">www.mup-project.net</a>
            </p>
        );
    }
};