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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">CreoleParser</li>
                    </ol>
                </nav>
                <h2>CreoleParser Class</h2>
                <p>A markup parser implementation for Creole.</p>
                <p>Extends <a href="https://msdn.microsoft.com/en-us/library/system.object.aspx" target="_blank">Object</a>. Implementes <Link to={Routes.documentation({ member: "Mup.IMarkupParser" })}>IMarkupParser</Link>.</p>
                <pre><code><span className={Style.textPrimary}>public</span> <span className={Style.textPrimary}>class</span> CreoleParser : IMarkupParser</code></pre>
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
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.CreoleParser()" })}>CreoleParser()</Link>
                            </td>
                            <td>public</td>
                            <td>Initializes a new instance of the <Link to={Routes.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link> class.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.CreoleParser(Mup.CreoleParserOptions)" })}>CreoleParser(CreoleParserOptions)</Link>
                            </td>
                            <td>public</td>
                            <td>Initializes a new instance of the <Link to={Routes.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link> class.</td>
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
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.Options" })}>Options</Link>
                            </td>
                            <td>public get</td>
                            <td>The options used by the parser.</td>
                        </tr>
                    </tbody>
                </table>
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
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.Parse(System.String)" })}>Parse(string)</Link>
                            </td>
                            <td>public</td>
                            <td>Parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.ParseAsync(System.String)" })}>ParseAsync(string)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.ParseAsync(System.String,System.Threading.CancellationToken)" })}>ParseAsync(string, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.ParseAsync(System.IO.TextReader)" })}>ParseAsync(TextReader)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.ParseAsync(System.IO.TextReader,System.Threading.CancellationToken)" })}>ParseAsync(TextReader, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.ParseAsync(System.IO.TextReader,System.Int32)" })}>ParseAsync(TextReader, int)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.CreoleParser.ParseAsync(System.IO.TextReader,System.Int32,System.Threading.CancellationToken)" })}>ParseAsync(TextReader, int, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
