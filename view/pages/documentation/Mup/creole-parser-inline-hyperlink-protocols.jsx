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
                        <Link to={routePath.documentation({ "member": "Mup.CreoleParser" })}>CreoleParser</Link>
                    </li>
                    <li class={Bootstrap.active}>InlineHyperlinkProtocols</li>
                </ol>
                <h2>InlineHyperlinkProtocols Property</h2>
                <p>Gets the protocols for which inline hyperlinks are generated.</p>
                <pre><code><span class={Bootstrap.textPrimary}>protected</span> <span class={Bootstrap.textPrimary}>virtual</span> IEnumerable&lt;<span class={Bootstrap.textPrimary}>string</span>&gt; InlineHyperlinkProtocols {"{"} <span class={Bootstrap.textPrimary}>get</span>; {"}"}</code></pre>
            </div>
        );
    }
};
