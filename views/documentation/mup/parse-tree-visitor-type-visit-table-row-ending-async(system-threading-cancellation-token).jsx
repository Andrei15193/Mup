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
                            <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">VisitTableRowEndingAsync(CancellationToken)</li>
                    </ol>
                </nav>
                <h2>VisitTableRowEndingAsync(CancellationToken) Method</h2>
                <p>Asynchronously visits the ending of a table row.</p>
                <pre><code><span className={Style.textPrimary}>protected</span> <span className={Style.textPrimary}>virtual</span> Task VisitTableRowEndingAsync(CancellationToken cancellationToken)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>cancellationToken</strong>: A token that can be used to signal a cancellation request.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns a <a href="https://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx" target="_blank">Task</a> representing the asynchronous operation.</p>
            </div>
        );
    }
};
