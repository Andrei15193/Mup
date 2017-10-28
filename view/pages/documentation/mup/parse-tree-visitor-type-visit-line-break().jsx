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
                        <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>
                    </li>
                    <li class={Bootstrap.active}>VisitLineBreak()</li>
                </ol>
                <h2>VisitLineBreak() Method</h2>
                <p>Visits a line break.</p>
                <pre><code><span class={Bootstrap.textPrimary}>protected</span> <span class={Bootstrap.textPrimary}>virtual</span> <span class={Bootstrap.textPrimary}>void</span> VisitLineBreak()</code></pre>
            </div>
        );
    }
};
