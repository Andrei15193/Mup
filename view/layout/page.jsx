import React from "react";
import join from "classnames";

import Content from "view/layout/content";
import { Container, Row, MainRow } from "view/layout/stacking";

import Header from "./header";
import Navigation from "./navigation";
import Footer from "./footer";

export default class Page extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Container>
                <Row>
                    <Header />
                </Row>
                <Row>
                    <Navigation />
                </Row>
                <MainRow>
                    <Content fullHeight>
                        <Container>
                            <Row>
                                <h1>{this.props.title}</h1>
                            </Row>
                            <MainRow>
                                {this.props.children}
                            </MainRow>
                        </Container>
                    </Content>
                </MainRow>
                <Row>
                    <Footer />
                </Row>
            </Container>
        );
    }
};