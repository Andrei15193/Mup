import React from "react";
import { Link } from "react-router-dom";
import join from "classnames";

import routePath from "route-path";

import Bootstrap from "css/bootstrap";

export default class GetInvolved extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class={join(Bootstrap.panel, Bootstrap.panelInfo)}>
                <div class={Bootstrap.panelBody}>
                    Do you want to help improve this library? You can start with adding a few test cases, they are the core part that describe how the language elements
                    work. <Link to={routePath.testCases} class={join(Bootstrap.btn, Bootstrap.btnPrimary)} role="button">Get Involved!</Link>
                </div>
            </div>
        );
    }
}