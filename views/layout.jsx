import React from "react";
import { NavLink, withRouter } from "react-router-dom";
import PropTypes from "prop-types";

import Style from "mup/style";
import { Routes } from "../routes";
import LogoUrl from "../images/logo.png";

export class Page extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className={[Style.noHeight, Style.minHeight100, Style.dFlex, Style.flexColumn, Style.container, Style.px2].join(" ")}>
                <h1 className={[Style.mt4, Style.mb3, Style.flexNoShrink].join(" ")}>
                    <img src={LogoUrl} alt="logo" /> <abbr title="MarkUp Parser">Mup</abbr> <small>Markup for Everyone</small>
                </h1>
                <div className={Style.flexNoShrink}>
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
                <div className={Style.flexNoShrink}>
                    <p className={Style.textCenter}>
                        Mup Copyright &copy; 2018 Andrei Fangli<br />
                        <a href="http://www.mup-project.net/">www.mup-project.net</a>
                    </p>
                </div>
            </div>
        );
    }
};

const NavItem = withRouter(
    class extends React.PureComponent {
        static get propTypes() {
            return {
                path: PropTypes.string.isRequired,
                exact: PropTypes.bool.isRequired
            };
        }

        static get defaultProps() {
            return {
                exact: false
            };
        }

        constructor(props) {
            super(props);
        }

        render() {
            return (
                <li className={Style.navItem}>
                    <NavLink
                        className={Style.navLink}
                        activeClassName={Style.active}
                        to={this.props.path}
                        exact={this.props.exact}
                        onClick={(event => (event.target.className.includes(Style.active) && event.preventDefault()))}>
                        {this.props.children}
                    </NavLink>
                </li>
            );
        }
    }
);