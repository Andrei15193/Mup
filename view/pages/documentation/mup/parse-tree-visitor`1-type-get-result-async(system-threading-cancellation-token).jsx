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
                        <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor<TResult>" })}>ParseTreeVisitor&lt;TResult&gt;</Link>
                    </li>
                    <li class={Bootstrap.active}>GetResultAsync(CancellationToken)</li>
                </ol>
                <h2>GetResultAsync(CancellationToken) Method</h2>
                <p>Asynchronously gets the visitor result. This method is called only after the visit operation completes.</p>
                <pre><code><span class={Bootstrap.textPrimary}>protected</span> <span class={Bootstrap.textPrimary}>virtual</span> Task&lt;TResult&gt; GetResultAsync(CancellationToken cancellationToken)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>cancellationToken</strong>: A token that can be used to signal a cancellation request.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns the result after the entire parse tree has been visited wrapped in a <a href="https://msdn.microsoft.com/en-us/library/dd321424.aspx">Task&lt;TResult&gt;</a>.</p>
            </div>
        );
    }
};
