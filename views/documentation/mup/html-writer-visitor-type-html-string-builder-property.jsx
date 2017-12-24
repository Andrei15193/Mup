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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">HtmlStringBuilder</li>
                    </ol>
                </nav>
                <h2>HtmlStringBuilder Property</h2>
                <p>Gets the <a href="https://msdn.microsoft.com/en-us/library/system.text.stringbuilder.aspx" target="_blank">StringBuilder</a> where the HTML is being written.</p>
                <pre><code><span className={Style.textPrimary}>protected</span> StringBuilder HtmlStringBuilder {"{"} <span className={Style.textPrimary}>get</span>; {"}"}</code></pre>
            </div>
        );
    }
};
