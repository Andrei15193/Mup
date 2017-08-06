import React from "react";
import join from "classnames";
import { Link } from "react-router-dom";

import Bootstrap from "css/bootstrap";
import Style from "css/style";

import Routes from "common/routes";

export default class Navigation extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        var navItemComponents = Object
            .getOwnPropertyNames(Routes)
            .map(function (routeName) {
                const route = Routes[routeName];
                return (
                    <li key={routeName} class={join({ [Bootstrap.active]: (route === this.props.route) })} >
                        <Link to={route.path}>{route.title}</Link>
                    </li >
                );
            }, this);

        return (
            <ul class={join(Style.content, Bootstrap.nav, Bootstrap.navTabs)}>
                {navItemComponents}
            </ul>
        );
    }
};