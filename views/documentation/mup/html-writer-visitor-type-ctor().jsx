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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">HtmlWriterVisitor()</li>
                    </ol>
                </nav>
                <h2>HtmlWriterVisitor() Constructor</h2>
                <p>Initializes a new instance of the <Link to={Routes.documentation({ member: "Mup.HtmlWriterVisitor" })}>HtmlWriterVisitor</Link> class.</p>
                <pre><code><span className={Style.textPrimary}>public</span> HtmlWriterVisitor()</code></pre>
            </div>
        );
    }
};
