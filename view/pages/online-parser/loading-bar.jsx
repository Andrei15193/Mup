import React from "react";
import join from "classnames";

import DependencyContainer from "dependency-container";
import Bootstrap from "css/bootstrap";

import Style from "./editor.css"

export default class LoadingBar extends React.Component {
    constructor(props) {
        super(props);
        this._store = DependencyContainer.parserStore;

        this.state = { isLoading: this._store.isLoading };

        this._updateLoading = (() => this.setState({ isLoading: this._store.isLoading })).bind(this);
    }

    componentDidMount() {
        this._store.isLoadingChanged.add(this._updateLoading);
    }

    componentWillUnmount() {
        this._store.isLoadingChanged.remove(this._updateLoading);
    }

    render() {
        return (
            <div class={join(Bootstrap.progress, Style.loadingBar, { [Style.loadingBarInactive]: !this.state.isLoading })}>
                <div
                    class={join(Bootstrap.progressBar, Bootstrap.progressBarStriped, Bootstrap.active)}
                    role="progressbar"
                    aria-valuenow="100"
                    aria-valuemin="0"
                    aria-valuemax="100" />
            </div >
        );
    }
};