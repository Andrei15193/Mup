import React from "react";
import PropTypes from "prop-types";

import { Badge, BadgeType } from "mup/views/common";

export const Phase = {
    "Planned": "Planned",
    "Alpha": "Alpha",
    "Beta": "Beta",
    "Released": "Released"
};

export const ElementStatus = {
    "Planned": "planned",
    "InProgress": "in-progress",
    "Done": "done"
};

export class Element extends React.PureComponent {
    static get propTypes() {
        return {
            name: PropTypes.string.isRequired,
            status: PropTypes.oneOf(Object.getOwnPropertyNames(ElementStatus).map(name => ElementStatus[name])).isRequired
        };
    }

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Badge type={getBadgeTypeFrom(this.props.status)}>
                {this.props.name}
            </Badge>
        )
    }
}

function getBadgeTypeFrom(elementStatus) {
    switch (elementStatus) {
        case ElementStatus.InProgress:
            return BadgeType.Info;

        case ElementStatus.Done:
            return BadgeType.Success;

        default:
            return BadgeType.Secondary;
    }
}

export class WikiLanguage extends React.PureComponent {
    static get propTypes() {
        return {
            name: PropTypes.string.isRequired,
            siteUrl: PropTypes.string.isRequired,
            phase: PropTypes.oneOf(Object.getOwnPropertyNames(Phase).map(name => Phase[name])).isRequired,
            version: PropTypes.shape({
                major: PropTypes.number.isRequired,
                minor: PropTypes.number.isRequired,
                patch: PropTypes.number.isRequired
            }).isRequired
        };
    }

    constructor(props) {
        super(props);
    }

    render() {
        const components = [];
        React.Children.forEach(this.props.children, child => {
            components.push(child);
            components.push(' ');
        });
        if (components.length > 0)
            components.pop();

        return (
            <tr>
                <td><strong><a href={this.props.siteUrl} target="_blank">{this.props.name}</a></strong></td>
                <td>{this.props.phase} </td>
                <td>{this.props.version.major}.{this.props.version.minor}.{this.props.version.patch}</td>
                <td>{components}</td>
            </tr>
        )
    }
};