import React from "react";
import Page from "view/layout/page";

export default class Home extends React.Component {
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

                <p>
                    You should have received a copy of the <strong>GNU Lesser General Public License</strong> along with this program. If not, see <a href="https://www.gnu.org/licenses/" target="_blank">www.gnu.org/licenses</a>.
                </p>
            </Page>
        );
    }
};