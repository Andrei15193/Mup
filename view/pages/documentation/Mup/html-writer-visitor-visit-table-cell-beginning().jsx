// This is a generated file.
import React from "react";
import { Link } from "react-router-dom";
import join from "classnames";

import routePath from "route-path";
import Bootstrap from "css/bootstrap";

export default class extends React.PureComponent {
    constructor(props) {
       super(props);
    }

    render () {
        return (
            <div>
                <ol class={Bootstrap.breadcrumb}>
                    <li>
                        <Link to={routePath.documentation({ "member": "Mup" })}>Mup</Link>
                    </li>
                    <li>
                        <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor" })}>HtmlWriterVisitor</Link>
                    </li>
                    <li class={Bootstrap.active}>VisitTableCellBeginning()</li>
                </ol>
                <h2>VisitTableCellBeginning() Method</h2>
                <p>Visits the beginning of a table cell.</p>
                <pre><code><span class={Bootstrap.textPrimary}>protected</span> <span class={Bootstrap.textPrimary}>override</span> <span class={Bootstrap.textPrimary}>void</span> VisitTableCellBeginning()</code></pre>
            </div>
        );
    }
};
