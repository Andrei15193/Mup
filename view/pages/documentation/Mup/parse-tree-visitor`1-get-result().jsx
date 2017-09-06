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
                    <li class={Bootstrap.active}>GetResult()</li>
                </ol>
                <h2>GetResult() Method</h2>
                <p>Gets the visitor result. This values is used only after the visit operation completes.</p>
                <pre><code><span class={Bootstrap.textPrimary}>protected</span> <span class={Bootstrap.textPrimary}>abstract</span> TResult GetResult()</code></pre>
                <h3>Returns</h3>
                <p>Returns the result after the entire parse tree has been visited.</p>
            </div>
        );
    }
};
