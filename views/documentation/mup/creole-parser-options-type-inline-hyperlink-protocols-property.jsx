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
                            <Link to={Routes.documentation({ member: "Mup.CreoleParserOptions" })}>CreoleParserOptions</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">InlineHyperlinkProtocols</li>
                    </ol>
                </nav>
                <h2>InlineHyperlinkProtocols Property</h2>
                <p>The protocols to consider when parsing inline hyperlinks (e.g.: http, https and so on). The defauts are <code>http</code>, <code>https</code>, <code>ftp</code> and <code>ftps</code>.</p>
                <pre><code><span className={Style.textPrimary}>public</span> IEnumerable&lt;<span className={Style.textPrimary}>string</span>&gt; InlineHyperlinkProtocols {"{"} <span className={Style.textPrimary}>get</span>; <span className={Style.textPrimary}>set</span>; {"}"}</code></pre>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when the value is set to <code>null</code>.</li>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentexception.aspx" target="_blank">ArgumentException</a></strong>: Thrown when the given collection contains <code>null</code>, empty or white space elements.</li>
                </ul>
            </div>
        );
    }
};
