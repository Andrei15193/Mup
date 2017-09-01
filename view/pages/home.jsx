import React from "react";
import Page from "view/layout/page";

import Description from "./home/description";
import GetInvolved from "./home/get-involved";
import LanguageStatus from "./home/language-status";

export default class Home extends React.Component {
    render() {
        return (
            <Page title="Home">
                {/* <GetInvolved /> */}
                <Description />
                <LanguageStatus />
            </Page>
        );
    }
};