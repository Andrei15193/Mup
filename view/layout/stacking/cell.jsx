import React from "react";

import Style from "./stacking.css";

export default class Cell extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class={Style.cell}>
                {this.props.children}
            </div>
        );
    }
};