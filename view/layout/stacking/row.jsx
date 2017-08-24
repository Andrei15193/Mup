import React from "react";

import Style from "./stacking.css";

export default class Row extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class={Style.row}>
                {this.props.children}
            </div>
        );
    }
};