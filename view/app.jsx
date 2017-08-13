import React from "react";
import { HashRouter, Route, Switch } from "react-router-dom";

import routePath from "route-path";
import { Home, OnlineParser, TestCases, Roadmap, License } from "view/pages";

export default class App extends React.Component {
    render() {
        document.title = "Mup - Parsers for Everyone";
        return (
            <HashRouter>
                <Switch>
                    <Route exact path={routePath.home} component={Home} />
                    <Route exact path={routePath.onlineParser} component={OnlineParser} />
                    <Route exact path={routePath.testCases} component={TestCases} />
                    <Route exact path={routePath.roadmap} component={Roadmap} />
                    <Route exact path={routePath.license} component={License} />
                    <Route path="/" component={Home} />
                </Switch>
            </HashRouter>
        );
    }
};