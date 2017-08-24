import React from "react";

import Style from "./stacking.css";

export default class MainRow extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class={Style.mainRow}>
                {this.props.children}
            </div>
        );
    }
};