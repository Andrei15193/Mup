import React from "react";
import join from "classnames";

import Page from "view/layout/page";
import { Container, Row, MainRow, Cell } from "view/layout/stacking";
import Bootstrap from "css/bootstrap";

import Editor from "./online-parser/editor";
import ViewModeSwitch from "./online-parser/view-mode-switch";
import LoadingBar from "./online-parser/loading-bar";

export default class OnlineParser extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Page title="Online Parser">
                <Container>
                    <Row>
                        <Container>
                            <MainRow>
                                <Cell>
                                    <h3>Creole</h3>
                                </Cell>
                                <Cell>
                                    <ViewModeSwitch />
                                </Cell>
                            </MainRow>
                        </Container>
                    </Row>
                    <Row>
                        <LoadingBar />
                    </Row>
                    <MainRow>
                        <Editor />
                    </MainRow>
                </Container>
            </Page>
        );
    }
};