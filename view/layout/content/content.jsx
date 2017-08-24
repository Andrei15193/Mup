import React from "react";
import join from "classnames";

import Style from "./content.css";

export default class Content extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class={join(
                {
                    [Style.content]: !this.props.merge,
                    [Style.fullHeight]: this.props.fullHeight
                })}>
                {this._children}
            </div>
        );
    }

    get _children() {
        let children;
        if (this.props.merge) {
            let count = 0;
            children = [];

            React.Children.forEach(
                this.props.children,
                function (child) {
                    children.push(
                        React.cloneElement(
                            child,
                            {
                                key: count++,
                                className: join(child.props.className, Style.content)
                            })
                    );
                });
        }
        else
            children = this.props.children;

        return children;
    }
};