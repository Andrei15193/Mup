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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">Mup</li>
                    </ol>
                </nav>
                <h2>Mup Namespace</h2>
                <table className={[Style.table, Style.tableHover].join(" ")}>
                    <thead>
                        <tr>
                            <th>Type</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IMarkupParser" })}>IMarkupParser</Link>
                            </td>
                            <td>A common interface for each markup parser implementation.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link>
                            </td>
                            <td>A common interface for the result of any markup parser.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link>
                            </td>
                            <td>A markup parser implementation for Creole.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.CreoleParserOptions" })}>CreoleParserOptions</Link>
                            </td>
                            <td>Specifies options for the <Link to={Routes.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link>.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.HtmlWriterVisitor" })}>HtmlWriterVisitor</Link>
                            </td>
                            <td>A <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor<TResult>" })}>ParseTreeVisitor&lt;TResult&gt;</Link> implementation for generating HTML from an <Link to={Routes.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link>.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.ParseTreeExtensions" })}>ParseTreeExtensions</Link>
                            </td>
                            <td>A helper class containing extension methods for parse tasks.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>
                            </td>
                            <td>Base class of all parse tree visitors.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor<TResult>" })}>ParseTreeVisitor&lt;TResult&gt;</Link>
                            </td>
                            <td>Base class for all parse tree visitors that eventually provide a result that is stored in memory (e.g. a <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx" target="_blank">string</a> or a <a href="https://msdn.microsoft.com/en-us/library/system.io.memorystream.aspx" target="_blank">MemoryStream</a>).</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
