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
                        <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>
                    </li>
                    <li class={Bootstrap.active}>VisitImageAsync(string, string, CancellationToken)</li>
                </ol>
                <h2>VisitImageAsync(string, string, CancellationToken) Method</h2>
                <p>Asynchronously visits an image.</p>
                <pre><code><span class={Bootstrap.textPrimary}>protected</span> <span class={Bootstrap.textPrimary}>virtual</span> Task VisitImageAsync(<span class={Bootstrap.textPrimary}>string</span> source, <span class={Bootstrap.textPrimary}>string</span> alternativeText, CancellationToken cancellationToken)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>source</strong>: The source of the image.</li>
                    <li><strong>alternativeText</strong>: The alternative text for the image.</li>
                    <li><strong>cancellationToken</strong>: A token that can be used to signal a cancellation request.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns a <a href="https://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx" target="_blank">Task</a> representing the asynchronous operation.</p>
            </div>
        );
    }
};
