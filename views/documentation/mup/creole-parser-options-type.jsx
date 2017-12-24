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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">CreoleParserOptions</li>
                    </ol>
                </nav>
                <h2>CreoleParserOptions Class</h2>
                <p>Specifies options for the <Link to={Routes.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link>.</p>
                <p>Extends <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" target="_blank">Object</a>.</p>
                <pre><code><span className={Style.textPrimary}>public</span> <span className={Style.textPrimary}>sealed</span> <span className={Style.textPrimary}>class</span> CreoleParserOptions</code></pre>
                <h3>Constructors</h3>
                <table className={[Style.table, Style.tableHover].join(" ")}>
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
                                <Link to={Routes.documentation({ member: "Mup.CreoleParserOptions.CreoleParserOptions()" })}>CreoleParserOptions()</Link>
                            </td>
                            <td>public</td>
                            <td>Initializes a new instance of the <Link to={Routes.documentation({ member: "Mup.CreoleParserOptions" })}>CreoleParserOptions</Link> class.</td>
                        </tr>
                    </tbody>
                </table>
                <h3>Properties</h3>
                <table className={[Style.table, Style.tableHover].join(" ")}>
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
                                <Link to={Routes.documentation({ member: "Mup.CreoleParserOptions.InlineHyperlinkProtocols" })}>InlineHyperlinkProtocols</Link>
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
