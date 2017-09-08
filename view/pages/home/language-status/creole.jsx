import React from "react";

import Language from "../language";
import Phase from "../phase";
import Version from "../version";
import Elements from "../elements";

const SupportedElements = [
    "headings",
    "paragraphs",
    "lists",
    "tables",
    "inline code",
    "code blocks",
    "horizontal rules",
    "emphasis (italics)",
    "strong (bold)",
    "inline hyperlinks",
    "hyperlinks",
    "images",
    "line breaks",
    "add ins"
];

export default class CreoleStatus extends React.Component {
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