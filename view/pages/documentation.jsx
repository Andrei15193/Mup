import React from "react";
import { withRouter } from "react-router";
import Page from "view/layout/page";

import Factories from "./documentation/factories";

export default withRouter(
    class Documentation extends React.PureComponent {
        constructor(props) {
            super(props);
        }

        render() {
            return (
                <Page title="Documentation">
                    {this._component}
                </Page>
            );
        }

        get _component() {
            const member = (this.props.match.params.member || "mup").toLocaleLowerCase();
            const factory = Factories[member];
            if (factory !== undefined)
                return factory();
            else
                return Factories["mup"]();
        }
    }
);