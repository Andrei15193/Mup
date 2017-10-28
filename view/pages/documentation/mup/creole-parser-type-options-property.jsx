// This is a generated file.
import React from "react";
import { Link } from "react-router-dom";
import join from "classnames";

import routePath from "route-path";
import Bootstrap from "css/bootstrap";

export default class extends React.PureComponent {
    constructor(props) {
       super(props);
    }

    render () {
        return (
            <div>
                <ol class={Bootstrap.breadcrumb}>
                    <li>
                        <Link to={routePath.documentation({ "member": "Mup" })}>Mup</Link>
                    </li>
                    <li>
                        <Link to={routePath.documentation({ "member": "Mup.CreoleParser" })}>CreoleParser</Link>
                    </li>
                    <li class={Bootstrap.active}>Options</li>
                </ol>
                <h2>Options Property</h2>
                <p>The options used by the parser.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> CreoleParserOptions Options {"{"} {"}"}</code></pre>
            </div>
        );
    }
};
