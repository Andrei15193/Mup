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
                    <li class={Bootstrap.active}>IMarkupParser</li>
                </ol>
                <h2>IMarkupParser Interface</h2>
                <p>A common interface for each markup parser implementation.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>interface</span> IMarkupParser</code></pre>
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
                                <Link to={routePath.documentation({ "member": "Mup.IMarkupParser.Parse(System.String)" })}>Parse(string)</Link>
                            </td>
                            <td>public</td>
                            <td>Parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IMarkupParser.ParseAsync(System.String)" })}>ParseAsync(string)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IMarkupParser.ParseAsync(System.String,System.Threading.CancellationToken)" })}>ParseAsync(string, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IMarkupParser.ParseAsync(System.IO.TextReader)" })}>ParseAsync(TextReader)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IMarkupParser.ParseAsync(System.IO.TextReader,System.Threading.CancellationToken)" })}>ParseAsync(TextReader, CancellationToken)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IMarkupParser.ParseAsync(System.IO.TextReader,System.Int32)" })}>ParseAsync(TextReader, int)</Link>
                            </td>
                            <td>public</td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.IMarkupParser.ParseAsync(System.IO.TextReader,System.Int32,System.Threading.CancellationToken)" })}>ParseAsync(TextReader, int, CancellationToken)</Link>
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
