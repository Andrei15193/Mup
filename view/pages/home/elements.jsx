import React from "react";
import { Label } from "common/bootstrap";

export default class Elements extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        const components = [];
        const elements = this.props.elements;
        const elementsCount = elements.length;

        if (elementsCount > 0) {
            var elementIndex = 0;
            components.push(<Label key={elementIndex} text={elements[elementIndex]} />);
            for (elementIndex = 1; elementIndex < elementsCount; elementIndex++) {
                components.push(' ');
                components.push(<Label key={elementIndex} text={elements[elementIndex]} />);
            }
        }

        return (
            <td>{components}</td>
        );
    }
};
