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
                    <li class={Bootstrap.active}>VisitHyperlinkBeginning(string)</li>
                </ol>
                <h2>VisitHyperlinkBeginning(string) Method</h2>
                <p>Visits the beginning of a hyperlink.</p>
                <pre><code><span class={Bootstrap.textPrimary}>protected</span> <span class={Bootstrap.textPrimary}>virtual</span> <span class={Bootstrap.textPrimary}>void</span> VisitHyperlinkBeginning(<span class={Bootstrap.textPrimary}>string</span> destination)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>destination</strong>: The hyperlink destination.</li>
                </ul>
            </div>
        );
    }
};
