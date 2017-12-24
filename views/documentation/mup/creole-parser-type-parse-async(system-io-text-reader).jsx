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
                            <Link to={Routes.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">ParseAsync(TextReader)</li>
                    </ol>
                </nav>
                <h2>ParseAsync(TextReader) Method</h2>
                <p>Asynchronously parses text from the given reader.</p>
                <pre><code><span className={Style.textPrimary}>public</span> <span className={Style.textPrimary}>virtual</span> Task&lt;IParseTree&gt; ParseAsync(TextReader reader)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>reader</strong>: A text reader from which to parse text.</li>
                </ul>
                <h3>Returns</h3>
                <p>Returns an <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> wrapped in a <a href="https://msdn.microsoft.com/en-us/library/dd321424.aspx" target="_blank">Task&lt;TResult&gt;</a> that can eventually be traversed using a <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>.</p>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when reader is null.</li>
                </ul>
            </div>
        );
    }
};
