import React from "react";

export default class Language extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <td><strong><a href={this.props.site} target="_blank">{this.props.name}</a></strong></td>
        );
    }
}