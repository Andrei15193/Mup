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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">IMarkupParser</li>
                    </ol>
                </nav>
                <h2>IMarkupParser Interface</h2>
                <p>A common interface for each markup parser implementation.</p>
                <pre><code><span className={Style.textPrimary}>public</span> <span className={Style.textPrimary}>interface</span> IMarkupParser</code></pre>
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
                                <Link to={Routes.documentation({ member: "Mup.IMarkupParser.Parse(System.String)" })}>Parse(string)</Link>
                            </td>
                            <td>Parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IMarkupParser.ParseAsync(System.String)" })}>ParseAsync(string)</Link>
                            </td>
                            <td>Asynchronously parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IMarkupParser.ParseAsync(System.String,System.Threading.CancellationToken)" })}>ParseAsync(string, CancellationToken)</Link>
                            </td>
                            <td>Asynchronously parses the given text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IMarkupParser.ParseAsync(System.IO.TextReader)" })}>ParseAsync(TextReader)</Link>
                            </td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IMarkupParser.ParseAsync(System.IO.TextReader,System.Threading.CancellationToken)" })}>ParseAsync(TextReader, CancellationToken)</Link>
                            </td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IMarkupParser.ParseAsync(System.IO.TextReader,System.Int32)" })}>ParseAsync(TextReader, int)</Link>
                            </td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.IMarkupParser.ParseAsync(System.IO.TextReader,System.Int32,System.Threading.CancellationToken)" })}>ParseAsync(TextReader, int, CancellationToken)</Link>
                            </td>
                            <td>Asynchronously parses text from the given reader.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
