import React from "react";
import join from "classnames";

import ViewMode from "constants/view-mode";
import DependencyContainer from "dependency-container";
import { Container, Row, MainRow, Cell } from "view/layout/stacking";
import Bootstrap from "css/bootstrap";

import ViewModeSwitch from "./view-mode-switch";
import LoadingBar from "./loading-bar";
import TextArea from "./text-area";
import Preview from "./preview";
import HtmlPreview from "./html-preview";
import Style from "./editor.css";

export default class Editor extends React.Component {
    constructor(props) {
        super(props);
        this._actions = DependencyContainer.parserActions;
        this._store = DependencyContainer.parserStore;

        this.state = {
            view: ViewMode.edit,
            text: this._store.text,
            json: this._store.json,
            html: this._store.html,
            isLoading: this._store.isLoading
        };

        this._propertyChanged = (propertyName) => this.setState({ [propertyName]: this._store[propertyName] });
    }

    componentDidMount() {
        this._store.propertyChanged.add(this._propertyChanged);
    }

    componentWillUnmount() {
        this._store.propertyChanged.remove(this._propertyChanged);
    }

    render() {
        return (
            <Container>
                <Row>
                    <Container>
                        <MainRow>
                            <Cell>
                                <h3>Creole</h3>
                            </Cell>
                            <Cell>
                                <ViewModeSwitch view={this.state.view} onViewChanged={this._onViewChanged.bind(this)} disabled={this.state.isLoading} />
                            </Cell>
                        </MainRow>
                    </Container>
                </Row>
                <Row>
                    <LoadingBar visible={this.state.isLoading} />
                </Row>
                <MainRow>
                    <div class={Style.content}>
                        {this._view}
                    </div>
                </MainRow>
            </Container>
        )
    }

    _onViewChanged(view) {
        if (view != ViewMode.edit)
            this._actions.parse(this.state.text);
        this.setState({ view });
    }

    _onTextChanaged(text) {
        this.setState({ text });
    }

    get _view() {
        switch (this.state.view) {
            case ViewMode.edit:
                return (
                    <div class={join(Bootstrap.panel, Bootstrap.panelDefault, Style.editor)}>
                        <TextArea text={this.state.text} disabled={this.state.isLoading} onChange={this._onTextChanaged.bind(this)} />
                    </div>
                );

            case ViewMode.preview:
                return <Preview json={this.state.json} disabled={this.state.isLoading} />;

            case ViewMode.html:
                return <HtmlPreview html={this.state.html} disabled={this.state.isLoading} />;
        }
    }
};