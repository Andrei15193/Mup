import React from "react";

import Style from "mup/style";
import { Page } from "./layout";
import { DependencyContainer } from "../dependency-container";
import { JsonPreview } from "./online-parser/json-preview";

const Views = {
    edit: "edit",
    preview: "preview",
    html: "html"
};

export class OnlineParser extends React.Component {
    constructor(props) {
        super(props);

        this.editorActions = DependencyContainer.editorActions;
        this.parserActions = DependencyContainer.parserActions;
        this.editorStore = DependencyContainer.editorStore;
        this.previewStore = DependencyContainer.previewStore;
        this.storePropertyChanged = storePropertyChanged.bind(this);

        this.state = {
            view: Views.edit,
            text: this.editorStore.text,
            isBusy: this.previewStore.isBusy,
            json: this.previewStore.json,
            html: this.previewStore.html
        };
    }

    componentWillMount() {
        this.editorStore.on("propertyChanged", this.storePropertyChanged);
        this.previewStore.on("propertyChanged", this.storePropertyChanged);
    }

    componentWillUnmount() {
        this.editorStore.removeListener("propertyChanged", this.storePropertyChanged);
        this.previewStore.removeListener("propertyChanged", this.storePropertyChanged);
    }

    render() {
        return (
            <Page>
                <div className={[Style.dFlex, Style.alignItemsCenter].join(" ")}>
                    <h1>Creole Parser</h1>
                    <div className={[Style.btnGroup, Style.mlAuto].join(" ")} role="group" aria-label="toolbar">
                        <button type="button" onClick={onEditClicked.bind(this)} className={[Style.btn, getButtonColorClassNameFor.call(this, Views.edit), (this.state.isBusy ? Style.disabled : undefined)].join(" ")}>Edit</button>
                        <button type="button" onClick={onPreviewClicked.bind(this)} className={[Style.btn, getButtonColorClassNameFor.call(this, Views.preview)].join(" ")}>Preview</button>
                        <button type="button" onClick={onHtmlClicked.bind(this)} className={[Style.btn, getButtonColorClassNameFor.call(this, Views.html)].join(" ")}>HTML</button>
                    </div>
                </div>
                <div className={[Style.mt3, Style.flexFill].join(" ")}>
                    {
                        this.state.isBusy ? (
                            <div className={Style.progress}>
                                <div className={[Style.progressBar, Style.progressBarStriped, Style.progressBarAnimated, Style.w100].join(" ")}></div>
                            </div>
                        ) : getActiveView.call(this)
                    }
                </div>
            </Page>
        );
    }
};

function onEditClicked() {
    if (!this.state.isBusy)
        this.setState({ view: Views.edit });
}

function onPreviewClicked() {
    this.setState({ view: Views.preview });
    if (!this.state.isBusy) {
        this.parserActions.parseAsync(this.state.text);
    }
}

function onHtmlClicked() {
    this.setState({ view: Views.html });
    if (!this.state.isBusy) {
        this.parserActions.parseAsync(this.state.text);
    }
}

function getButtonColorClassNameFor(view) {
    return (this.state.view === view ? Style.btnPrimary : Style.btnLight);
}

function getActiveView() {
    switch (this.state.view) {
        case Views.edit:
            return (
                <form className={Style.flexFill}>
                    <div className={[Style.formGroup, Style.flexFill].join(" ")}>
                        <textarea className={[Style.resizeNone, Style.formControl, Style.rounded, Style.px3, Style.py1, Style.flexFill].join(" ")} onChange={event => this.editorActions.updateText(event.target.value)} value={this.state.text}></textarea>
                    </div>
                </form>
            );

        case Views.preview:
            return (
                <div className={[Style.border, Style.rounded, Style.px3, Style.py1].join(" ")}>
                    <JsonPreview json={this.state.json} />
                </div>
            );

        case Views.html:
            return (
                <pre className={[Style.px3, Style.py1].join(" ")}><code>{this.state.html}</code></pre>
            );
    }
}

function storePropertyChanged(propertyName) {
    switch (propertyName) {
        case "text":
            this.setState({
                text: this.editorStore.text
            });
            break;

        case "isBusy":
            this.setState({
                isBusy: this.previewStore.isBusy
            });
            break;

        case "json":
            this.setState({
                json: this.previewStore.json
            });
            break;

        case "html":
            this.setState({
                html: this.previewStore.html
            });
            break;
    }
}