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
                    <li class={Bootstrap.active}>HtmlStringBuilder</li>
                </ol>
                <h2>HtmlStringBuilder Property</h2>
                <p>Gets the <a href="https://msdn.microsoft.com/en-us/library/system.text.stringbuilder.aspx" target="_blank">StringBuilder</a> where the HTML is being written.</p>
                <pre><code><span class={Bootstrap.textPrimary}>protected</span> StringBuilder HtmlStringBuilder {"{"} <span class={Bootstrap.textPrimary}>get</span>; {"}"}</code></pre>
            </div>
        );
    }
};
