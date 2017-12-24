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
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">CreoleParser(CreoleParserOptions)</li>
                    </ol>
                </nav>
                <h2>CreoleParser(CreoleParserOptions) Constructor</h2>
                <p>Initializes a new instance of the <Link to={Routes.documentation({ member: "Mup.CreoleParser" })}>CreoleParser</Link> class.</p>
                <pre><code><span className={Style.textPrimary}>public</span> CreoleParser(CreoleParserOptions options)</code></pre>
                <h3>Parameters</h3>
                <ul>
                    <li><strong>options</strong>: The options to use when parsing a block of text.</li>
                </ul>
                <h3>Exceptions</h3>
                <ul>
                    <li><strong><a href="https://msdn.microsoft.com/en-us/library/system.argumentnullexception.aspx" target="_blank">ArgumentNullException</a></strong>: Thrown when options are null.</li>
                </ul>
            </div>
        );
    }
};
