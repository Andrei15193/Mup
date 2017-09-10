import React from "react";

import ViewMode from "constants/view-mode";
import DependencyContainer from "dependency-container";

import Writer from "./writer";
import Preview from "./preview";
import HtmlPreview from "./html-preview";
import Style from "./editor.css";

export default class Editor extends React.Component {
    constructor(props) {
        super(props);
        this._actions = DependencyContainer.parserActions;
        this._store = DependencyContainer.parserStore;

        this.state = {
            view: this._store.view,
            text: this._store.text,
            json: this._store.json,
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
            <div class={Style.content}>
                {this._view}
            </div>
        )
    }

    get _view() {
        switch (this.state.view) {
            case ViewMode.edit:
                return <Writer text={this.state.text} disabled={this.state.isLoading} />;

            case ViewMode.preview:
                return <Preview json={this.state.json} disabled={this.state.isLoading} />;

            case ViewMode.html:
                return <HtmlPreview html={this.state.html} disabled={this.state.isLoading} />;
        }
    }
};