import React from "react";

import LogoUrl from "mup/images/logo";
import Style from "mup/style";
import { Routes } from "../../routes";
import NavItem from "./nav-item";

export default class Page extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className={[Style.h100, Style.dFlex, Style.flexColumn, Style.container, Style.px2].join(" ")}>
                <h1 className={[Style.mt4, Style.mb3].join(" ")}>
                    <img src={LogoUrl} alt="logo" /> <abbr title="MarkUp Parser">Mup</abbr> <small>Markup for Everyone</small>
                </h1>

                <div>
                    <ul className={[Style.nav, Style.navPills, Style.pAuto].join(" ")}>
                        <NavItem exact path={Routes.home()}>Home</NavItem>
                        <NavItem path={Routes.onlineParser()}>Online Parser</NavItem>
                        <NavItem path={Routes.documentation()}>Documentation</NavItem>
                        <NavItem path={Routes.license()}>License</NavItem>
                    </ul>
                </div>
                <div className={[Style.px3, Style.py4, Style.flexFill].join(" ")}>
                    {this.props.children}
                </div>
                <div>
                    <p className={Style.textCenter}>
                        Mup Copyright &copy; 2017 Andrei Fangli<br />
                        <a href="http://www.mup-project.net/">www.mup-project.net</a>
                    </p>
                </div>
            </div>
        );
    }
};