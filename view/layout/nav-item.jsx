import React from "react";
import { withRouter } from "react-router";
import { Link } from "react-router-dom";
import join from "classnames";
import PropTypes from "prop-types";

import routePath from "route-path";

import Bootstrap from "css/bootstrap";


export default withRouter(
    class NavItem extends React.Component {
        static get propTypes() {
            return {
                match: PropTypes.object.isRequired,
                location: PropTypes.object.isRequired,
                history: PropTypes.object.isRequired,
                title: PropTypes.string.isRequired,
                page: PropTypes.oneOf(routePath.names)
            }
        }

        constructor(props) {
            super(props);
        }

        render() {
            return (
                <li class={join({ [Bootstrap.active]: (this.props.match.path === routePath(this.props.page)) })} >
                    <Link to={routePath(this.props.page)}>{this.props.title}</Link>
                </li>
            );
        }
    }
);