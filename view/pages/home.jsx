import React from "react";
import Page from "view/layout/page";

import Description from "./home/description";
import LanguageStatus from "./home/language-status";

export default class Home extends React.Component {
    render() {
        return (
            <Page>
                <h1>Home</h1>
                <Description />
                <LanguageStatus />
            </Page>
        );
    }
};