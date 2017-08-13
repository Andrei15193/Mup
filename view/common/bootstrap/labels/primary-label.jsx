import React from "react";
import LabelType from "./label-type";
import Label from "./label";

export default class PrimaryLabel extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return <Label {...this.props} type={LabelType.Primary} />;
    }
};