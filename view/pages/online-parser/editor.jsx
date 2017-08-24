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
        this._store = DependencyContainer.parserStore;

        this.state = { view: this._store.view };

        this._updateView = (() => this.setState({ view: this._store.view })).bind(this);
    }

    componentDidMount() {
        this._store.viewChanged.add(this._updateView);
    }

    componentWillUnmount() {
        this._store.viewChanged.remove(this._updateView);
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
                return <Writer />;

            case ViewMode.preview:
                return <Preview />;

            case ViewMode.html:
                return <HtmlPreview />;

            case ViewMode.loading:
                return <Loading />;
        }
    }
};