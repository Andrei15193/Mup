import React from "react";
import { HashRouter, Route, Switch } from "react-router-dom";

import routePath from "route-path";
import { Home, OnlineParser, License } from "view/pages";

export default class App extends React.Component {
    render() {
        document.title = "Mup - Parsers for Everyone";
        return (
            <HashRouter>
                <Switch>
                    <Route exact path={routePath.home.path} component={Home} />
                    <Route exact path={routePath.onlineParser.path} component={OnlineParser} />
                    <Route exact path={routePath.license.path} component={License} />
                    <Route path="/" component={Home} />
                </Switch>
            </HashRouter>
        );
    }
};