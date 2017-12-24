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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">VisitTableEnding()</li>
                    </ol>
                </nav>
                <h2>VisitTableEnding() Method</h2>
                <p>Visits the ending of a table.</p>
                <pre><code><span className={Style.textPrimary}>protected</span> <span className={Style.textPrimary}>virtual</span> <span className={Style.textPrimary}>void</span> VisitTableEnding()</code></pre>
            </div>
        );
    }
};
