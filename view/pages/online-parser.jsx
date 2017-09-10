import React from "react";
import join from "classnames";

import DependencyContainer from "dependency-container";
import Page from "view/layout/page";
import { Container, Row, MainRow, Cell } from "view/layout/stacking";
import Bootstrap from "css/bootstrap";

import Editor from "./online-parser/editor";
import ViewModeSwitch from "./online-parser/view-mode-switch";
import LoadingBar from "./online-parser/loading-bar";

export default class OnlineParser extends React.Component {
    constructor(props) {
        super(props);
        this._store = DependencyContainer.parserStore;

        this.state = {
            view: this._store.view,
            isLoading: this._store.isLoading
        };

        this._propertyChanged = (propertyName) => {
            if (propertyName == "isLoading")
                this.setState({ isLoading: this._store.isLoading })
            if (propertyName == "view")
                this.setState({ view: this._store.view })
        };
    }

    componentDidMount() {
        this._store.propertyChanged.add(this._propertyChanged);
    }

    componentWillUnmount() {
        this._store.propertyChanged.remove(this._propertyChanged);
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
                                    <ViewModeSwitch view={this.state.view} disabled={this.state.isLoading} />
                                </Cell>
                            </MainRow>
                        </Container>
                    </Row>
                    <Row>
                        <LoadingBar visible={this.state.isLoading} />
                    </Row>
                    <MainRow>
                        <Editor />
                    </MainRow>
                </Container>
            </Page>
        );
    }
};