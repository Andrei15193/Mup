import React from "react";
import Routes from "common/routes";
import Page from "view/layout/page";

export default class TestCases extends React.Component {
    render() {
        return (
            <Page route={Routes.testCases}>
                <h1>Test Cases</h1>
            </Page>
        );
    }
};