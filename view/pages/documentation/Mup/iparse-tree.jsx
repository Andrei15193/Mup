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
                    <li class={Bootstrap.active}>IParseTree</li>
                </ol>
                <h2>IParseTree Interface</h2>
                <p>A common interface for the result of all markup parsers.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>interface</span> IParseTree</code></pre>
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
                                <Link to={routePath.documentation({ "member": "Mup.IParseTree.Accept(Mup.ParseTreeVisitor)" })}>Accept(ParseTreeVisitor)</Link>
                            </td>
                            <td>public</td>
                            <td>Accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IParseTree.Accept`1(Mup.ParseTreeVisitor<Mup.TResult>)" })}>Accept(ParseTreeVisitor&lt;TResult&gt;)</Link>
                            </td>
                            <td>public</td>
                            <td>Accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IParseTree.AcceptAsync(Mup.ParseTreeVisitor)" })}>AcceptAsync(ParseTreeVisitor)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IParseTree.AcceptAsync(Mup.ParseTreeVisitor,System.Threading.CancellationToken)" })}>AcceptAsync(ParseTreeVisitor, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IParseTree.AcceptAsync`1(Mup.ParseTreeVisitor<Mup.TResult>)" })}>AcceptAsync(ParseTreeVisitor&lt;TResult&gt;)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IParseTree.AcceptAsync`1(Mup.ParseTreeVisitor<Mup.TResult>,System.Threading.CancellationToken)" })}>AcceptAsync(ParseTreeVisitor&lt;TResult&gt;, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
