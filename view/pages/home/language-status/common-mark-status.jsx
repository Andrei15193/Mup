import React from "react";

import Language from "../language";
import Phase from "../phase";
import Version from "../version";
import Elements from "../elements";
import ElementStatus from "../element-status";

const SupportedElements = [
    { name: "headings", status: ElementStatus.Planned },
    { name: "paragraphs", status: ElementStatus.Planned },
    { name: "lists", status: ElementStatus.Planned },
    { name: "block quotes", status: ElementStatus.Planned },
    { name: "inline code", status: ElementStatus.Planned },
    { name: "code blocks", status: ElementStatus.Planned },
    { name: "horizontal rules", status: ElementStatus.Planned },
    { name: "emphasis (italics)", status: ElementStatus.Planned },
    { name: "strong (bold)", status: ElementStatus.Planned },
    { name: "inline hyperlinks", status: ElementStatus.Planned },
    { name: "hyperlinks", status: ElementStatus.Planned },
    { name: "images", status: ElementStatus.Planned },
    { name: "line breaks", status: ElementStatus.Planned }
];

export default class CommonMarkStatus extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <tr>
                <Language name="CommonMark" site="http://commonmark.org/" />
                <Phase name="planned" />
                <Version major={2} />
                <Elements elements={SupportedElements} />
            </tr>
        );
    }
}