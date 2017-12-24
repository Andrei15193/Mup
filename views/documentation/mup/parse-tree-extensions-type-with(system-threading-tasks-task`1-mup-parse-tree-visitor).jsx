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
                            <Link to={Routes.documentation({ member: "Mup.ParseTreeExtensions" })}>ParseTreeExtensions</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">With(Task&lt;IParseTree&gt;, ParseTreeVisitor)</li>
                    </ol>
                </nav>
                <h2>With(Task&lt;IParseTree&gt;, ParseTreeVisitor) Method</h2>
                <p>Visits the <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> after the parse task completes.</p>
                <pre><code><span className={Style.textPrimary}>public</span> <span className={Style.textPrimary}>static</span> Task With(<span className={Style.textPrimary}>this</span> Task&lt;IParseTree&gt; parseTask, ParseTreeVisitor visitor)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>parseTask</strong>: The task generated from calling any of the ParseAsync methods on an <Link to={Routes.documentation({ member: "Mup.IMarkupParser" })}>IMarkupParser</Link>.</li>
                    <li><strong>visitor</strong>: The visitor to use for traversing the resulting parse tree.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns a <a href="https://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx" target="_blank">Task</a> representing the asynchronous operation.</p>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when either parseTask or visitor are null.</li>
                </ul>
            </div>
        );
    }
};
