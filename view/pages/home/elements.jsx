import React from "react";
import { Label, LabelType } from "view/common/bootstrap";

import ElementStatus from "./element-status";

export default class Elements extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const components = [];
        const elements = this.props.elements;
        const elementsCount = elements.length;

        if (elementsCount > 0) {
            var elementIndex = 0;
            components.push(this._getLabelFor(elementIndex, elements[elementIndex]));
            for (elementIndex = 1; elementIndex < elementsCount; elementIndex++) {
                components.push(' ');
                components.push(this._getLabelFor(elementIndex, elements[elementIndex]));
            }
        }

        return (
            <td>{components}</td>
        );
    }

    _getLabelFor(key, element) {
        return (
            <Label key={key} text={element.name} type={this._getLabelTypeFrom(element.status)} />
        );
    }

    _getLabelTypeFrom(elementStatus) {
        switch (elementStatus) {
            case ElementStatus.InProgress:
                return LabelType.Info;

            case ElementStatus.Done:
                return LabelType.Success;

            default:
                return LabelType.Default;
        }
    }
};
