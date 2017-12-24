import React from "react";

import { Page } from "mup/views/layout";
import Factories from "./documentation/factories";

export default class Documentation extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const documentationComponent = getDocumentationComponentFor(this.props.match.params.member);
        return (
            <Page >
                <h1>Documentation</h1>
                {documentationComponent}
            </Page>
        );
    }
};

function getDocumentationComponentFor(member) {
    const factoryName = (member && member.toLocaleLowerCase());
    if (factoryName in Factories)
        return Factories[factoryName]();
    else
        return Factories["mup"]();
}