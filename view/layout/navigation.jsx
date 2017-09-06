import React from "react";
import join from "classnames";

import Content from "view/layout/content";

import Bootstrap from "css/bootstrap";

import NavItem from "./nav-item";

export default class Navigation extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Content merge>
                <ul class={join(Bootstrap.nav, Bootstrap.navTabs)}>
                    <NavItem title="Home" page="home" />
                    <NavItem title="Online Parser" page="onlineParser" />
                    <NavItem title="Getting Started" page="gettingStarted" />
                    <NavItem title="Documentation" page="documentation" />
                    <NavItem title="License" page="license" />
                </ul>
            </Content>
        );
    }
};
