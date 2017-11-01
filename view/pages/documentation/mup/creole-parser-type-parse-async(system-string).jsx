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
                    <li class={Bootstrap.active}>ParseAsync(string)</li>
                </ol>
                <h2>ParseAsync(string) Method</h2>
                <p>Asynchronously parses the given text.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>virtual</span> Task&lt;IParseTree&gt; ParseAsync(<span class={Bootstrap.textPrimary}>string</span> text)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>text</strong>: The text to parse.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns an <Link to={routePath.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> wrapped in a <a href="https://msdn.microsoft.com/en-us/library/dd321424.aspx" target="_blank">Task&lt;TResult&gt;</a> that can eventually be traversed using a <Link to={routePath.documentation({ member: "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>.</p>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when text is null.</li>
                </ul>
            </div>
        );
    }
};
