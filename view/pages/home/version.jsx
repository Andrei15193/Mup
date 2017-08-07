import React from "react";
import PropTypes from 'prop-types';

export default class Version extends React.Component {
    static get propTypes() {
        return {
            major: PropTypes.number.isRequired,
            minor: PropTypes.number,
            patch: PropTypes.number
        };
    }

    static get defaultProps() {
        return {
            minor: 0,
            patch: 0
        };
    }

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <td>{this.props.major}.{this.props.minor}.{this.props.patch}</td>
        );
    }
}