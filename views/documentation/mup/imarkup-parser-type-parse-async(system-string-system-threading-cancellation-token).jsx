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
                            <Link to={Routes.documentation({ member: "Mup.IMarkupParser" })}>IMarkupParser</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">ParseAsync(string, CancellationToken)</li>
                    </ol>
                </nav>
                <h2>ParseAsync(string, CancellationToken) Method</h2>
                <p>Asynchronously parses the given text.</p>
                <pre><code>Task&lt;IParseTree&gt; ParseAsync(<span className={Style.textPrimary}>string</span> text, CancellationToken cancellationToken)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>text</strong>: The text to parse.</li>
                    <li><strong>cancellationToken</strong>: A token that can be used to signal a cancellation request.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns an <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> wrapped in a <a href="https://msdn.microsoft.com/en-us/library/dd321424.aspx" target="_blank">Task&lt;TResult&gt;</a> that can eventually be traversed using a <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>.</p>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when text is null.</li>
                </ul>
            </div>
        );
    }
};
