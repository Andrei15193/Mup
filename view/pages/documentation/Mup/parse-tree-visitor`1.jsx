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
                    <li class={Bootstrap.active}>ParseTreeVisitor&lt;TResult&gt;</li>
                </ol>
                <h2>ParseTreeVisitor&lt;TResult&gt; Class</h2>
                <p>Base class for all parse tree visitors that eventually provide a result that is stored in memory (e.g. a <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx">string</a> or a <a href="https://msdn.microsoft.com/en-us/library/system.io.memorystream.aspx">MemoryStream</a>).</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>abstract</span> <span class={Bootstrap.textPrimary}>class</span> ParseTreeVisitor&lt;TResult&gt; : ParseTreeVisitor</code></pre>
                <h3>Generic Parameters</h3>
                <ul>
                    <li><strong>TResult</strong>: The type which is constructed after visitng a parse tree.</li>
                </ul>
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
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor<TResult>.GetResult()" })}>GetResult()</Link>
                            </td>
                            <td>protected</td>
                            <td>Gets the visitor result. This method is called only after the visit operation completes.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor<TResult>.GetResultAsync(System.Threading.CancellationToken)" })}>GetResultAsync(CancellationToken)</Link>
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
