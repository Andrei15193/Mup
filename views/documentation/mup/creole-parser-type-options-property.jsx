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
                            <Link to={Routes.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">Options</li>
                    </ol>
                </nav>
                <h2>Options Property</h2>
                <p>The options used by the parser.</p>
                <pre><code><span className={Style.textPrimary}>public</span> CreoleParserOptions Options {"{"} <span className={Style.textPrimary}>get</span>; {"}"}</code></pre>
            </div>
        );
    }
};
