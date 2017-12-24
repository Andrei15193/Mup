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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">ParseAsync(TextReader, int, CancellationToken)</li>
                    </ol>
                </nav>
                <h2>ParseAsync(TextReader, int, CancellationToken) Method</h2>
                <p>Asynchronously parses text from the given reader.</p>
                <pre><code>Task&lt;IParseTree&gt; ParseAsync(TextReader reader, <span className={Style.textPrimary}>int</span> bufferSize, CancellationToken cancellationToken)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>reader</strong>: A text reader from which to parse text.</li>
                    <li><strong>bufferSize</strong>: The buffer size to use when reading text from the reader.</li>
                    <li><strong>cancellationToken</strong>: A token that can be used to signal a cancellation request.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns an <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> wrapped in a <a href="https://msdn.microsoft.com/en-us/library/dd321424.aspx" target="_blank">Task&lt;TResult&gt;</a> that can eventually be traversed using a <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>.</p>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when reader is null.</li>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx" target="_blank">ArgumentException</a></strong>: Thrown when bufferSize is negative or 0 (zero).</li>
                </ul>
            </div>
        );
    }
};
