import React from "react";
import { HashRouter, Route, Switch } from "react-router-dom";

import routePath from "route-path";
import { Home, OnlineParser, GettingStarted, Documentation, License } from "view/pages";

export default class App extends React.Component {
    render() {
        document.title = "Mup - Parsers for Everyone";
        return (
            <HashRouter>
                <Switch>
                    <Route exact path={routePath.home.path} component={Home} />
                    <Route exact path={routePath.onlineParser.path} component={OnlineParser} />
                    <Route exact path={routePath.gettingStarted.path} component={GettingStarted} />
                    <Route path={routePath.documentation.path} component={Documentation} />
                    <Route exact path={routePath.license.path} component={License} />
                    <Route path="/" component={Home} />
                </Switch>
            </HashRouter>
        );
    }
};