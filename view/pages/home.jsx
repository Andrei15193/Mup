import React from "react";
import Routes from "common/routes";
import Page from "view/layout/page";

export default class Home extends React.Component {
    render() {
        return (
            <Page route={Routes.home}>
                <h1>Home</h1>
            </Page>
        );
    }
};