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
                    <li class={Bootstrap.active}>CreoleParserOptions</li>
                </ol>
                <h2>CreoleParserOptions Class</h2>
                <p>Specifies options for the <Link to={routePath.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link>.</p>
                <p>Extends <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" target="_blank">Object</a>.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>sealed</span> <span class={Bootstrap.textPrimary}>class</span> CreoleParserOptions</code></pre>
                <h3>Constructors</h3>
                <table class={join(Bootstrap.table, Bootstrap.tableHover)}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Access Modifier</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParserOptions.CreoleParserOptions()" })}>CreoleParserOptions()</Link>
                            </td>
                            <td>public</td>
                            <td>Initializes a new instance of the <Link to={routePath.documentation({ member: "Mup.CreoleParserOptions" })}>CreoleParserOptions</Link> class.</td>
                        </tr>
                    </tbody>
                </table>
                <h3>Properties</h3>
                <table class={join(Bootstrap.table, Bootstrap.tableHover)}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Access Modifier</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParserOptions.InlineHyperlinkProtocols" })}>InlineHyperlinkProtocols</Link>
                            </td>
                            <td>public get; public set</td>
                            <td>The protocols to consider when parsing inline hyperlinks (e.g.: http, https and so on). The defauts are <code>http</code>, <code>https</code>, <code>ftp</code> and <code>ftps</code>.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
