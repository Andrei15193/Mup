import React from "react";
import Page from "view/layout/page";

import GnuLesserGeneralPublicLicense from "./licenses/gnu-lesser-general-public-license";

export default class Home extends React.PureComponent {
    render() {
        return (
            <Page title="License">
                <p>
                    Mup Copyright &copy; 2017 Andrei Fangli
                </p>

                <p>
                    This library is free software: you can redistribute it and/or modify it under the terms of the <strong>GNU Lesser General Public License</strong> as
                    published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
                </p>

                <p>
                    This program is distributed in the hope that it will be useful, but <strong>without any warranty</strong>; without even the implied warranty
                    of <strong>merchantability</strong> or <strong>fitness for a particular purpose</strong>. See the <strong>GNU Lesser General Public License</strong> for more details.
                </p>

                <hr />

                <GnuLesserGeneralPublicLicense />
            </Page>
        );
    }
};