import React from "react";

import { WikiLanguage, Phase, ElementStatus, Element } from "./wiki-language";

export default class CommonMarkStatus extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <WikiLanguage name="CommonMark" siteUrl="http://commonmark.org/" phase={Phase.Planned} version={{ major: 2, minor: 0, patch: 0 }}>
                <Element name="headings" status={ElementStatus.Planned} />
                <Element name="paragraphs" status={ElementStatus.Planned} />
                <Element name="lists" status={ElementStatus.Planned} />
                <Element name="block quotes" status={ElementStatus.Planned} />
                <Element name="inline code" status={ElementStatus.Planned} />
                <Element name="code blocks" status={ElementStatus.Planned} />
                <Element name="horizontal rules" status={ElementStatus.Planned} />
                <Element name="emphasis (italics)" status={ElementStatus.Planned} />
                <Element name="strong (bold)" status={ElementStatus.Planned} />
                <Element name="inline hyperlinks" status={ElementStatus.Planned} />
                <Element name="hyperlinks" status={ElementStatus.Planned} />
                <Element name="images" status={ElementStatus.Planned} />
                <Element name="line breaks" status={ElementStatus.Planned} />
            </WikiLanguage>
        );
    }
};