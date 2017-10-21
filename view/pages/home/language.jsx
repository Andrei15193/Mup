import React from "react";
import { Link } from "react-router-dom";

export default class Language extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <td><strong><a href={this.props.site} target="_blank">{this.props.name}</a></strong></td>
        );
    }
}