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
                    <li class={Bootstrap.active}>Mup</li>
                </ol>
                <h2>Mup Namespace</h2>
                <table class={join(Bootstrap.table, Bootstrap.tableHover)}>
                    <thead>
                        <tr>
                            <th>Type</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ member: "Mup.IMarkupParser" })}>IMarkupParser</Link>
                            </td>
                            <td>A common interface for each markup parser implementation.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link>
                            </td>
                            <td>A common interface for the result of all markup parsers.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link>
                            </td>
                            <td>A markup parser implementation for Creole.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ member: "Mup.HtmlWriterVisitor" })}>HtmlWriterVisitor</Link>
                            </td>
                            <td>A <Link to={routePath.documentation({ member: "Mup.ParseTreeVisitor<TResult>" })}>ParseTreeVisitor&lt;TResult&gt;</Link> implementation for generating HTML from an <Link to={routePath.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link>.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ member: "Mup.ParseTreeExtensions" })}>ParseTreeExtensions</Link>
                            </td>
                            <td>A helper class containing extension methods for parse tasks.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ member: "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>
                            </td>
                            <td>Base class of all parse tree visitors.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ member: "Mup.ParseTreeVisitor<TResult>" })}>ParseTreeVisitor&lt;TResult&gt;</Link>
                            </td>
                            <td>Base class for all parse tree visitors that eventually provide a result that is stored in memory (e.g. a <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx">string</a> or a <a href="https://msdn.microsoft.com/en-us/library/system.io.memorystream.aspx">MemoryStream</a>).</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
