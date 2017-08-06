import React from "react";

import Style from "css/style";

import Header from "./header";
import Navigation from "./navigation";

export default class Page extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <Header {...this.props} />
                <Navigation {...this.props} />
                <div class={Style.content}>
                    {this.props.children}
                </div>
            </div>
        );
    }
};