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
                        <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor" })}>HtmlWriterVisitor</Link>
                    </li>
                    <li class={Bootstrap.active}>AppendHtmlSafe(string)</li>
                </ol>
                <h2>AppendHtmlSafe(string) Method</h2>
                <p>Appends the HTML encoded text. Encoding is done only for special characters.</p>
                <pre><code><span class={Bootstrap.textPrimary}>protected</span> <span class={Bootstrap.textPrimary}>void</span> AppendHtmlSafe(<span class={Bootstrap.textPrimary}>string</span> text)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>text</strong>: The text to append to <Link to={routePath.documentation({ member: "Mup.HtmlWriterVisitor.HtmlStringBuilder" })}>HtmlStringBuilder</Link>.</li>
                </ul>
            </div>
        );
    }
};
