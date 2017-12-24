// This is a generated file.
import React from "react";
import { Link } from "react-router-dom";

import Routes from "mup/routes";
import Style from "mup/style";

export default class extends React.PureComponent {
    constructor(props) {
       super(props);
    }

    render () {
        return (
            <div>
                <nav aria-label="breadcrumb" role="navigation">
                    <ol className={Style.breadcrumb}>
                        <li className={Style.breadcrumbItem}>
                            <Link to={Routes.documentation({ member: "Mup" })}>Mup</Link>
                        </li>
                        <li className={Style.breadcrumbItem}>
                            <Link to={Routes.documentation({ member: "Mup.HtmlWriterVisitor" })}>HtmlWriterVisitor</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">AppendHtmlSafe(char)</li>
                    </ol>
                </nav>
                <h2>AppendHtmlSafe(char) Method</h2>
                <p>Appends the HTML encoded character. Encoding is done only for special characters.</p>
                <pre><code><span className={Style.textPrimary}>protected</span> <span className={Style.textPrimary}>void</span> AppendHtmlSafe(<span className={Style.textPrimary}>char</span> character)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>character</strong>: The character to append to <Link to={Routes.documentation({ member: "Mup.HtmlWriterVisitor.HtmlStringBuilder" })}>HtmlStringBuilder</Link>.</li>
                </ul>
            </div>
        );
    }
};
