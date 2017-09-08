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
                        <Link to={routePath.documentation({ "member": "Mup.IParseTree" })}>IParseTree</Link>
                    </li>
                    <li class={Bootstrap.active}>AcceptAsync(ParseTreeVisitor, CancellationToken)</li>
                </ol>
                <h2>AcceptAsync(ParseTreeVisitor, CancellationToken) Method</h2>
                <p>Asynchronously accepts a visitor which can be used to generate output from the parse tree.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>abstract</span> Task AcceptAsync(ParseTreeVisitor visitor, CancellationToken cancellationToken)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>visitor</strong>: The visitor that will traverse the parse tree.</li>
                    <li><strong>cancellationToken</strong>: A token that can be used to signal a cancellation request.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns a <a href="https://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx">Task</a> representing the asynchronous operation.</p>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx">ArgumentNullException</a></strong>: Thrown when visitor is null.</li>
                </ul>
            </div>
        );
    }
};
