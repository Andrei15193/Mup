import React from "react";

import Language from "../language";
import Phase from "../phase";
import Version from "../version";
import Elements from "../elements";
import ElementStatus from "../element-status";

const SupportedElements = [
    { name: "headings", status: ElementStatus.Done },
    { name: "paragraphs", status: ElementStatus.Done },
    { name: "lists", status: ElementStatus.Done },
    { name: "tables", status: ElementStatus.Done },
    { name: "inline code", status: ElementStatus.Done },
    { name: "code blocks", status: ElementStatus.Done },
    { name: "horizontal rules", status: ElementStatus.Done },
    { name: "emphasis (italics)", status: ElementStatus.Done },
    { name: "strong (bold)", status: ElementStatus.Done },
    { name: "inline hyperlinks", status: ElementStatus.Done },
    { name: "hyperlinks", status: ElementStatus.Done },
    { name: "images", status: ElementStatus.Done },
    { name: "line breaks", status: ElementStatus.Done },
    { name: "plugins", status: ElementStatus.Done }
];

export default class CreoleStatus extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <tr>
                <Language name="Creole" site="http://www.wikicreole.org/wiki/Home/" />
                <Phase name="beta" />
                <Version major={1} />
                <Elements elements={SupportedElements} />
            </tr>
        );
    }
}