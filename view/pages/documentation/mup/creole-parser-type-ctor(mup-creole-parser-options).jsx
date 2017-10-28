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
                    <li class={Bootstrap.active}>CreoleParser(CreoleParserOptions)</li>
                </ol>
                <h2>CreoleParser(CreoleParserOptions) Constructor</h2>
                <p>Initializes a new instance of the <Link to={routePath.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link> class.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> CreoleParser(CreoleParserOptions options)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>options</strong>: The options to use when parsing a block of text.</li>
                </ul>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx">ArgumentNullException</a></strong>: Thrown when options is null.</li>
                </ul>
            </div>
        );
    }
};
