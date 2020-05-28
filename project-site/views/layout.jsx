import React from "react";
import { NavLink, withRouter } from "react-router-dom";
import PropTypes from "prop-types";

import { Routes } from "../routes";
import LogoUrl from "../images/logo.png";

export class Page extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="min-height-100 d-flex flex-column container px-2">
                <h1 className="mt-4 mb-3">
                    <img src={LogoUrl} alt="logo" /> <abbr title="MarkUp Parser">Mup</abbr> <small>Markup for Everyone</small>
                </h1>

                <div>
                    <ul className="nav nav-pills p-auto">
                        <NavItem exact path={Routes.home()}>Home</NavItem>
                        <NavItem path={Routes.documentation()}>Documentation</NavItem>
                        <NavItem path={Routes.license()}>License</NavItem>
                    </ul>
                </div>
                <div className="px-3 py-4 flex-fill">
                    {this.props.children}
                </div>
                <div>
                    <p className="text-center">
                        Mup Copyright &copy; 2020 Andrei Fangli<br />
                        <a href="http://www.mup-project.net/">www.mup-project.net</a>
                    </p>
                </div>
            </div>
        );
    }
};

const NavItem =  withRouter(
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
                <li className="nav-item">
                    <NavLink
                        className="nav-link"
                        activeClassName="active"
                        to={this.props.path}
                        exact={this.props.exact}
                        onClick={(event => (event.target.className.includes("active") && event.preventDefault()))}>
                        {this.props.children}
                    </NavLink>
                </li>
            );
        }
    }
);
