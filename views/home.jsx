import React from "react";

import { Page } from "mup/views/layout";
import Description from "./home/description";
import WikiLanguages from "./home/wiki-languages";

export default class Home extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Page>
                <h1>Home</h1>
                <Description />
                <WikiLanguages />
            </Page>
        );
    }
};