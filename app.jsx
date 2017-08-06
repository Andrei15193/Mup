import React from "react";
import { HashRouter, Route, Switch } from "react-router-dom";

import Routes from "common/routes";
import { Home, TestCases, About } from "view/pages";

export default class App extends React.Component {
    render() {
        document.title = "Mup - Parsers for Everyone";
        return (
            <HashRouter>
                <Switch>
                    <Route exact path={Routes.home.path} component={Home} />
                    <Route exact path={Routes.testCases.path} component={TestCases} />
                    <Route exact path={Routes.about.path} component={About} />
                    <Route path="/" component={Home} />
                </Switch>
            </HashRouter>
        );
    }
};