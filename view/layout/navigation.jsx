import React from "react";
import join from "classnames";

import Bootstrap from "css/bootstrap";
import Style from "css/style";

import NavItem from "./nav-item";

export default class Navigation extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <ul class={join(Style.content, Bootstrap.nav, Bootstrap.navTabs)}>
                <NavItem title="Home" page="home" />
                <NavItem title="Test Cases" page="testCases" />
                <NavItem title="Roadmap" page="roadmap" />
            </ul>
        );
    }
};
