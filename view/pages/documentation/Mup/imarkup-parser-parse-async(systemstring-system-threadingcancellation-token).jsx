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
                        <Link to={routePath.documentation({ "member": "Mup.IMarkupParser" })}>IMarkupParser</Link>
                    </li>
                    <li class={Bootstrap.active}>ParseAsync(string, CancellationToken)</li>
                </ol>
                <h2>ParseAsync(string, CancellationToken) Method</h2>
                <p>Asynchronously parses the given text.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>abstract</span> Task&lt;IParseTree&gt; ParseAsync(<span class={Bootstrap.textPrimary}>string</span> text, CancellationToken cancellationToken)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>text</strong>: The text to parse.</li>
                    <li><strong>cancellationToken</strong>: A token that can be used to signal a cancellation request.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns an <Link to={routePath.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> that can be used to generate other formats.</p>
            </div>
        );
    }
};
