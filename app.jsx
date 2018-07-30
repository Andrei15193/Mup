import React from "react";
import ReactDOM from "react-dom";
import { HashRouter, Route, Switch } from "react-router-dom";
import "bootstrap";

import "./images/logo-og.png";
import { RoutePaths } from "./routes";
import { Home } from "./views/home";
import { OnlineParser } from "./views/online-parser";
import { Documentation } from "./views/documentation";
import { License } from "./views/license";

class App extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <HashRouter hashType="noslash">
                <Switch>
                    <Route exact path={RoutePaths.home} component={Home} />
                    <Route path={RoutePaths.onlineParser} component={OnlineParser} />
                    <Route path={RoutePaths.documentation} component={Documentation} />
                    <Route path={RoutePaths.license} component={License} />
                </Switch>
            </HashRouter>
        );
    }
}

const appElement = document.createElement("div");
if (document.body.children.length > 0)
    document.body.insertBefore(appElement, document.body.children[0]);
else
    document.body.appendChild(appElement);
ReactDOM.render(<App />, appElement);