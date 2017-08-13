import React from "react";
import join from "classnames";
import Bootstrap from "css/bootstrap";
import LabelType from "./label-type";

export default class Label extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const hasChildren = (!!this.props.children && (!(this.props.children instanceof Array) || this.props.children.length > 0));

        if (hasChildren && this.props.text)
            throw new Error("You cannot use <Label text> and <Label children> in the same label.");

        const content = (hasChildren ? this.props.children : this.props.text);
        return (
            <span class={join(Bootstrap.label, (this.props.type || LabelType.Default))}>{content}</span>
        );
    }
};