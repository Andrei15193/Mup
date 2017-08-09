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
                <Header />
                <Navigation />
                <div class={Style.content}>
                    <h1>{this.props.title}</h1>
                    {this.props.children}
                </div>
            </div>
        );
    }
};