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
                    <li class={Bootstrap.active}>CreoleParser</li>
                </ol>
                <h2>CreoleParser Class</h2>
                <p>A markup parser implementation for Creole.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>class</span> CreoleParser : IMarkupParser</code></pre>
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
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParser.CreoleParser()" })}>CreoleParser()</Link>
                            </td>
                            <td>public</td>
                            <td>Initializes a new instance of the <Link to={routePath.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link> class.</td>
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
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParser.InlineHyperlinkProtocols" })}>InlineHyperlinkProtocols</Link>
                            </td>
                            <td>protected get</td>
                            <td>Gets the protocols for which inline hyperlinks are generated.</td>
                        </tr>
                    </tbody>
                </table>
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
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParser.Parse(System.String)" })}>Parse(string)</Link>
                            </td>
                            <td>public</td>
                            <td>Parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParser.ParseAsync(System.String)" })}>ParseAsync(string)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParser.ParseAsync(System.String,System.Threading.CancellationToken)" })}>ParseAsync(string, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParser.ParseAsync(System.IO.TextReader)" })}>ParseAsync(TextReader)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParser.ParseAsync(System.IO.TextReader,System.Threading.CancellationToken)" })}>ParseAsync(TextReader, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParser.ParseAsync(System.IO.TextReader,System.Int32)" })}>ParseAsync(TextReader, int)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.CreoleParser.ParseAsync(System.IO.TextReader,System.Int32,System.Threading.CancellationToken)" })}>ParseAsync(TextReader, int, CancellationToken)</Link>
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
