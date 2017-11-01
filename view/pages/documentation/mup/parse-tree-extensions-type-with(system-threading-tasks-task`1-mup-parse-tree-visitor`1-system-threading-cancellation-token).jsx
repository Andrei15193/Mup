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
                    <li class={Bootstrap.active}>With&lt;TResult&gt;(Task&lt;IParseTree&gt;, ParseTreeVisitor&lt;TResult&gt;, CancellationToken)</li>
                </ol>
                <h2>With&lt;TResult&gt;(Task&lt;IParseTree&gt;, ParseTreeVisitor&lt;TResult&gt;, CancellationToken) Method</h2>
                <p>Visits the <Link to={routePath.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> after the parse task completes.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>static</span> Task&lt;TResult&gt; With&lt;TResult&gt;(<span class={Bootstrap.textPrimary}>this</span> Task&lt;IParseTree&gt; parseTask, ParseTreeVisitor&lt;TResult&gt; visitor, CancellationToken cancellationToken)</code></pre>
                <h3>Generic Parameters</h3>
                <ul>
                    <li><strong>TResult</strong>: The type which is constructed after visitng a parse tree.</li>
                </ul>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>parseTask</strong>: The task generated from calling any of the ParseAsync methods on an <Link to={routePath.documentation({ member: "Mup.IMarkupParser" })}>IMarkupParser</Link>.</li>
                    <li><strong>visitor</strong>: The visitor to use when transforming the parse tree.</li>
                    <li><strong>cancellationToken</strong>: A token that can be used to signal a cancellation request.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns the result generated by the visitor wrapped in a <a href="https://msdn.microsoft.com/en-us/library/dd321424.aspx" target="_blank">Task&lt;TResult&gt;</a>.</p>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when either parseTask or visitor are null.</li>
                </ul>
            </div>
        );
    }
};
