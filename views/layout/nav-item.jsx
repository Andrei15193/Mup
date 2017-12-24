import React from "react";
import PropTypes from "prop-types";
import { NavLink, withRouter } from "react-router-dom";

import Style from "mup/style";

import Routes, { RoutePaths } from "mup/routes";

export default withRouter(
    class NavItem extends React.PureComponent {
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