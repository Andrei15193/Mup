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
                    <li class={Bootstrap.active}>ParseTreeExtensions</li>
                </ol>
                <h2>ParseTreeExtensions Class</h2>
                <p>A helper class containing extension methods for <Link to={routePath.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link>.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>abstract</span> <span class={Bootstrap.textPrimary}>class</span> ParseTreeExtensions</code></pre>
                <h3>Methods</h3>
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
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeExtensions.With(System.Threading.Tasks.Task<Mup.IParseTree>,Mup.ParseTreeVisitor)" })}>With(Task&lt;IParseTree&gt;, ParseTreeVisitor)</Link>
                            </td>
                            <td>public</td>
                            <td>Uses the given visitor to visit the parse tree as soon as the parse operation completes.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeExtensions.With(System.Threading.Tasks.Task<Mup.IParseTree>,Mup.ParseTreeVisitor,System.Threading.CancellationToken)" })}>With(Task&lt;IParseTree&gt;, ParseTreeVisitor, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Uses the given visitor to visit the parse tree as soon as the parse operation completes.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeExtensions.With`1(System.Threading.Tasks.Task<Mup.IParseTree>,Mup.ParseTreeVisitor<Mup.TResult>)" })}>With(Task&lt;IParseTree&gt;, ParseTreeVisitor&lt;TResult&gt;)</Link>
                            </td>
                            <td>public</td>
                            <td>Uses the given visitor to transform the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeExtensions.With`1(System.Threading.Tasks.Task<Mup.IParseTree>,Mup.ParseTreeVisitor<Mup.TResult>,System.Threading.CancellationToken)" })}>With(Task&lt;IParseTree&gt;, ParseTreeVisitor&lt;TResult&gt;, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Uses the given visitor to transform the parse tree.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
