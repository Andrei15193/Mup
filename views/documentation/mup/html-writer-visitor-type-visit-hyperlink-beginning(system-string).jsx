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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">VisitHyperlinkBeginning(string)</li>
                    </ol>
                </nav>
                <h2>VisitHyperlinkBeginning(string) Method</h2>
                <p>Visits the beginning of a hyperlink.</p>
                <pre><code><span className={Style.textPrimary}>protected</span> <span className={Style.textPrimary}>override</span> <span className={Style.textPrimary}>void</span> VisitHyperlinkBeginning(<span className={Style.textPrimary}>string</span> destination)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>destination</strong>: The hyperlink destination.</li>
                </ul>
            </div>
        );
    }
};
