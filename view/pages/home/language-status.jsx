import React from "react";
import join from "classnames";

import Bootstrap from "css/bootstrap";

import CreoleStatus from "./language-status/creole-status";
import CommonMarkStatus from "./language-status/common-mark-status";

export default class LanguageStatus extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <h3>Language Support</h3>
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
                        <CommonMarkStatus />
                    </tbody>
                </table>
            </div>
        );
    }
}