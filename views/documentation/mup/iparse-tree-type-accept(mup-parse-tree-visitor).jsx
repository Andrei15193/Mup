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
                            <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">Accept(ParseTreeVisitor)</li>
                    </ol>
                </nav>
                <h2>Accept(ParseTreeVisitor) Method</h2>
                <p>Accepts a visitor which can be used to generate output from the parse tree.</p>
                <pre><code><span className={Style.textPrimary}>void</span> Accept(ParseTreeVisitor visitor)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>visitor</strong>: The visitor that will traverse the parse tree.</li>
                </ul>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when visitor is null.</li>
                </ul>
            </div>
        );
    }
};
