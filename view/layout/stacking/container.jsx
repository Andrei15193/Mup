import React from "react";

import Style from "./stacking.css";

export default class Container extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class={Style.container}>
                {this.props.children}
            </div>
        );
    }
};