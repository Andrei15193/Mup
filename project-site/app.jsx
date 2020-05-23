import React from "react";
import ReactDOM from "react-dom";
import { HashRouter, Route, Switch, withRouter } from "react-router-dom";

import "./images/logo-og.png";
import { RoutePaths } from "./routes";
import { Home } from "./views/home";
import { Documentation } from "./views/documentation";
import { License } from "./views/license";

import "./app.scss";

const RouteNavigation = withRouter(
    class extends React.PureComponent {
        constructor(props) {
            super(props);
        }

        render() {
            return (
                <Switch>
                    <Route exact path={RoutePaths.home} component={Home} />
                    <Route path={RoutePaths.documentation} component={Documentation} />
                    <Route path={RoutePaths.license} component={License} />
                </Switch>
            );
        }
    }
)

class App extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <HashRouter hashType="noslash">
                <RouteNavigation />
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