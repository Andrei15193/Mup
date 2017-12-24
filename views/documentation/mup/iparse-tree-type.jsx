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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">IParseTree</li>
                    </ol>
                </nav>
                <h2>IParseTree Interface</h2>
                <p>A common interface for the result of any markup parser.</p>
                <pre><code><span className={Style.textPrimary}>public</span> <span className={Style.textPrimary}>interface</span> IParseTree</code></pre>
                <h3>Methods</h3>
                <table className={[Style.table, Style.tableHover].join(" ")}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IParseTree.Accept(Mup.ParseTreeVisitor)" })}>Accept(ParseTreeVisitor)</Link>
                            </td>
                            <td>Accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IParseTree.Accept<TResult>(Mup.ParseTreeVisitor<TResult>)" })}>Accept&lt;TResult&gt;(ParseTreeVisitor&lt;TResult&gt;)</Link>
                            </td>
                            <td>Accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IParseTree.AcceptAsync(Mup.ParseTreeVisitor)" })}>AcceptAsync(ParseTreeVisitor)</Link>
                            </td>
                            <td>Asynchronously accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IParseTree.AcceptAsync(Mup.ParseTreeVisitor,System.Threading.CancellationToken)" })}>AcceptAsync(ParseTreeVisitor, CancellationToken)</Link>
                            </td>
                            <td>Asynchronously accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IParseTree.AcceptAsync<TResult>(Mup.ParseTreeVisitor<TResult>)" })}>AcceptAsync&lt;TResult&gt;(ParseTreeVisitor&lt;TResult&gt;)</Link>
                            </td>
                            <td>Asynchronously accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IParseTree.AcceptAsync<TResult>(Mup.ParseTreeVisitor<TResult>,System.Threading.CancellationToken)" })}>AcceptAsync&lt;TResult&gt;(ParseTreeVisitor&lt;TResult&gt;, CancellationToken)</Link>
                            </td>
                            <td>Asynchronously accepts a visitor which can be used to generate output from the parse tree.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
