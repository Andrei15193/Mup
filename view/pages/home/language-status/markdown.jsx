import React from "react";

import Language from "../language";
import Phase from "../phase";
import Version from "../version";
import Elements from "../elements";

const SupportedElements = [
    "headings",
    "paragraphs",
    "lists",
    "blockquotes",
    "inline code",
    "code blocks",
    "horizontal rules",
    "emphasis (italics)",
    "strong (bold)",
    "inline hyperlinks",
    "hyperlinks",
    "images",
    "line breaks"
];

export default class MarkdownStatus extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <tr>
                <Language name="Markdown" site="https://daringfireball.net/projects/markdown/" />
                <Phase name="planned" />
                <Version major={2} />
                <Elements elements={SupportedElements} />
            </tr>
        );
    }
}