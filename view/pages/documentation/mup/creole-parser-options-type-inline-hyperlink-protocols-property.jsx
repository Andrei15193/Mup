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
                        <Link to={routePath.documentation({ "member": "Mup.CreoleParserOptions" })}>CreoleParserOptions</Link>
                    </li>
                    <li class={Bootstrap.active}>InlineHyperlinkProtocols</li>
                </ol>
                <h2>InlineHyperlinkProtocols Property</h2>
                <p>The protocols to consider when parsing inline hyperlinks (e.g.: http, https and so on). The defauts are <code>http</code>, <code>https</code>, <code>ftp</code> and <code>ftps</code>.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> IEnumerable&lt;<span class={Bootstrap.textPrimary}>string</span>&gt; InlineHyperlinkProtocols {"{"} <span class={Bootstrap.textPrimary}>get</span>; <span class={Bootstrap.textPrimary}>set</span>; {"}"}</code></pre>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when the value is set to <code>null</code>.</li>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx" target="_blank">ArgumentException</a></strong>: Thrown when the given collection contains <code>null</code>, empty or white space elements.</li>
                </ul>
            </div>
        );
    }
};
