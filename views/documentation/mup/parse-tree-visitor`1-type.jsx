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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">ParseTreeVisitor&lt;TResult&gt;</li>
                    </ol>
                </nav>
                <h2>ParseTreeVisitor&lt;TResult&gt; Class</h2>
                <p>Base class for all parse tree visitors that eventually provide a result that is stored in memory (e.g. a <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx" target="_blank">string</a> or a <a href="https://msdn.microsoft.com/en-us/library/system.io.memorystream.aspx" target="_blank">MemoryStream</a>).</p>
                <p>Extends <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor" })}>ParseTreeVisitor</Link>.</p>
                <pre><code><span className={Style.textPrimary}>public</span> <span className={Style.textPrimary}>abstract</span> <span className={Style.textPrimary}>class</span> ParseTreeVisitor&lt;TResult&gt; : ParseTreeVisitor</code></pre>
                <h3>Generic Parameters</h3>
                <ul>
                    <li><strong>TResult</strong>: The type which is constructed after visitng a parse tree.</li>
                </ul>
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
                                <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor<TResult>.GetResult()" })}>GetResult()</Link>
                            </td>
                            <td>protected</td>
                            <td>Gets the visitor result. This method is called only after the visit operation completes.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor<TResult>.GetResultAsync(System.Threading.CancellationToken)" })}>GetResultAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously gets the visitor result. This method is called only after the visit operation completes.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};