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
                            <Link to={Routes.documentation({ member: "Mup.HtmlWriterVisitor" })}>HtmlWriterVisitor</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">EndVisit()</li>
                    </ol>
                </nav>
                <h2>EndVisit() Method</h2>
                <p>Completes the visit operation. This method is called after all visit methods.</p>
                <pre><code><span className={Style.textPrimary}>protected</span> <span className={Style.textPrimary}>sealed</span> <span className={Style.textPrimary}>void</span> EndVisit()</code></pre>
            </div>
        );
    }
};
