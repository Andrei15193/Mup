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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">VisitImage(string, string)</li>
                    </ol>
                </nav>
                <h2>VisitImage(string, string) Method</h2>
                <p>Visits an image.</p>
                <pre><code><span className={Style.textPrimary}>protected</span> <span className={Style.textPrimary}>override</span> <span className={Style.textPrimary}>void</span> VisitImage(<span className={Style.textPrimary}>string</span> source, <span className={Style.textPrimary}>string</span> alternativeText)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>source</strong>: The source of the image.</li>
                    <li><strong>alternativeText</strong>: The alternative text for the image.</li>
                </ul>
            </div>
        );
    }
};
