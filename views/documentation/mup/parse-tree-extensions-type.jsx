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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">ParseTreeExtensions</li>
                    </ol>
                </nav>
                <h2>ParseTreeExtensions Class</h2>
                <p>A helper class containing extension methods for parse tasks.</p>
                <p>Extends <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" target="_blank">Object</a>.</p>
                <pre><code><span className={Style.textPrimary}>public</span> <span className={Style.textPrimary}>static</span> <span className={Style.textPrimary}>class</span> ParseTreeExtensions</code></pre>
                <h3>Methods</h3>
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
                                <Link to={Routes.documentation({ member: "Mup.ParseTreeExtensions.With(System.Threading.Tasks.Task<Mup.IParseTree>,Mup.ParseTreeVisitor)" })}>With(Task&lt;IParseTree&gt;, ParseTreeVisitor)</Link>
                            </td>
                            <td>public</td>
                            <td>Visits the <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> after the parse task completes.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.ParseTreeExtensions.With(System.Threading.Tasks.Task<Mup.IParseTree>,Mup.ParseTreeVisitor,System.Threading.CancellationToken)" })}>With(Task&lt;IParseTree&gt;, ParseTreeVisitor, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Visits the <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> after the parse task completes.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.ParseTreeExtensions.With<TResult>(System.Threading.Tasks.Task<Mup.IParseTree>,Mup.ParseTreeVisitor<TResult>)" })}>With&lt;TResult&gt;(Task&lt;IParseTree&gt;, ParseTreeVisitor&lt;TResult&gt;)</Link>
                            </td>
                            <td>public</td>
                            <td>Visits the <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> after the parse task completes.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.ParseTreeExtensions.With<TResult>(System.Threading.Tasks.Task<Mup.IParseTree>,Mup.ParseTreeVisitor<TResult>,System.Threading.CancellationToken)" })}>With&lt;TResult&gt;(Task&lt;IParseTree&gt;, ParseTreeVisitor&lt;TResult&gt;, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Visits the <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link> after the parse task completes.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
