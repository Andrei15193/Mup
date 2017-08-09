import React from "react";
import join from "classnames";

import Bootstrap from "css/bootstrap";

import CreoleStatus from "./language-status/creole";
import MarkdownStatus from "./language-status/markdown";

export default class LanguageStatus extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <h2>Language Support</h2>
                <table class={join(Bootstrap.table, Bootstrap.tableHover)}>
                    <thead>
                        <tr>
                            <th>Language</th>
                            <th>Phase</th>
                            <th>Release Version</th>
                            <th width="60%">Elements</th>
                        </tr>
                    </thead>
                    <tbody>
                        <CreoleStatus />
                        <MarkdownStatus />
                    </tbody>
                </table>
            </div>
        );
    }
}