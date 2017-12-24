import React from "react";

import Style from "mup/style";
import CreoleStatus from "./language-status/creole-status";
import CommonMarkStatus from "./language-status/common-mark-status";

export default class WikiLanguages extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <h3>Language Support</h3>
                <table className={[Style.table, Style.tableHover].join(" ")}>
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