import React from "react";
import Routes from "common/routes";
import Page from "view/layout/page";

export default class About extends React.Component {
    render() {
        return (
            <Page route={Routes.about}>
                <h1>About</h1>
            </Page>
        );
    }
};