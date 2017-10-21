import React from "react";

const Phases = {
    "planned": "Planned",
    "alpha": "Alpha",
    "beta": "Beta",
    "released": "Released"
};

export default class Phase extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const phase = Phases[this.props.name];
        return (
            <td>{phase}</td>
        );
    }
}