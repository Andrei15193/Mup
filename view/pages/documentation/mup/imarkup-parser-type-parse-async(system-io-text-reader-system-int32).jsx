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
                    <li class={Bootstrap.active}>ParseAsync(TextReader, int)</li>
                </ol>
                <h2>ParseAsync(TextReader, int) Method</h2>
                <p>Asynchronously parses text from the given reader.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>abstract</span> Task&lt;IParseTree&gt; ParseAsync(TextReader reader, <span class={Bootstrap.textPrimary}>int</span> bufferSize)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>reader</strong>: A text reader from which to parse text.</li>
                    <li><strong>bufferSize</strong>: The buffer size to use when reading text from the reader.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns an <Link to={routePath.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> wrapped in a <a href="https://msdn.microsoft.com/en-us/library/dd321424.aspx">Task&lt;TResult&gt;</a> that can eventually be traversed using a <Link to={routePath.documentation({ member: "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>.</p>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx">ArgumentNullException</a></strong>: Thrown when reader is null.</li>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx">ArgumentException</a></strong>: Thrown when bufferSize is negative or 0 (zero).</li>
                </ul>
            </div>
        );
    }
};
