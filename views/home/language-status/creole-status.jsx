import React from "react";

import { WikiLanguage, Phase, ElementStatus, Element } from "./wiki-language";

export default class CreoleStatus extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <WikiLanguage name="Creole" siteUrl="http://www.wikicreole.org/wiki/Home/" phase={Phase.Released} version={{ major: 1, minor: 0, patch: 0 }}>
                <Element name="headings" status={ElementStatus.Done} />
                <Element name="paragraphs" status={ElementStatus.Done} />
                <Element name="lists" status={ElementStatus.Done} />
                <Element name="tables" status={ElementStatus.Done} />
                <Element name="inline code" status={ElementStatus.Done} />
                <Element name="code blocks" status={ElementStatus.Done} />
                <Element name="horizontal rules" status={ElementStatus.Done} />
                <Element name="emphasis (italics)" status={ElementStatus.Done} />
                <Element name="strong (bold)" status={ElementStatus.Done} />
                <Element name="inline hyperlinks" status={ElementStatus.Done} />
                <Element name="hyperlinks" status={ElementStatus.Done} />
                <Element name="images" status={ElementStatus.Done} />
                <Element name="line breaks" status={ElementStatus.Done} />
                <Element name="plugins" status={ElementStatus.Done} />
            </WikiLanguage>
        );
    }
};