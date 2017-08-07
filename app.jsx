import React from "react";
import { HashRouter, Route, Switch } from "react-router-dom";

import routePath from "route-path";
import { Home, TestCases, Roadmap } from "view/pages";

export default class App extends React.Component {
    render() {
        document.title = "Mup - Parsers for Everyone";
        return (
            <HashRouter>
                <Switch>
                    <Route exact path={routePath.home} component={Home} />
                    <Route exact path={routePath.testCases} component={TestCases} />
                    <Route exact path={routePath.roadmap} component={Roadmap} />
                    <Route path="/" component={Home} />
                </Switch>
            </HashRouter>
        );
    }
};