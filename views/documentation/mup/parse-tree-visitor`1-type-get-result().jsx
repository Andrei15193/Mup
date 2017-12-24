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
                        <li className={Style.breadcrumbItem}>
                            <Link to={Routes.documentation({ member: "Mup.ParseTreeVisitor<TResult>" })}>ParseTreeVisitor&lt;TResult&gt;</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">GetResult()</li>
                    </ol>
                </nav>
                <h2>GetResult() Method</h2>
                <p>Gets the visitor result. This method is called only after the visit operation completes.</p>
                <pre><code><span className={Style.textPrimary}>protected</span> <span className={Style.textPrimary}>abstract</span> TResult GetResult()</code></pre>
                <h3>Returns</h3>
                <p>Returns the result after the entire parse tree has been visited.</p>
            </div>
        );
    }
};
