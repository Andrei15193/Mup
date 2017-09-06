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
                        <Link to={routePath.documentation({ "member": "Mup.ParseTreeExtensions" })}>ParseTreeExtensions</Link>
                    </li>
                    <li class={Bootstrap.active}>With(Task&lt;IParseTree&gt;, ParseTreeVisitor, CancellationToken)</li>
                </ol>
                <h2>With(Task&lt;IParseTree&gt;, ParseTreeVisitor, CancellationToken) Method</h2>
                <p>Uses the given visitor to visit the parse tree as soon as the parse operation completes.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> Task With(Task&lt;IParseTree&gt; parseTask, ParseTreeVisitor visitor, CancellationToken cancellationToken)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>parseTask</strong>: The task generated from calling any of the ParseAsync methods on a <Link to={routePath.documentation({ member: "Mup.IMarkupParser" })}>IMarkupParser</Link>.</li>
                    <li><strong>visitor</strong>: The visitor to use for traversing the resulting parse tree.</li>
                    <li><strong>cancellationToken</strong>: A token that can be used to signal a cancellation request.</li>
                </ul>
            </div>
        );
    }
};
