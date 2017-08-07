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
            <table class={join(Bootstrap.table, Bootstrap.tableHover)}>
                <caption>Supported <a href="https://en.wikipedia.org/wiki/Lightweight_markup_language">Lightweight Markup Languages</a></caption>
                <thead>
                    <tr>
                        <th>Language</th>
                        <th>Phase</th>
                        <th>Since</th>
                        <th width="60%">Elements</th>
                    </tr>
                </thead>
                <tbody>
                    <CreoleStatus />
                    <MarkdownStatus />
                </tbody>
            </table>
        );
    }
}