import $ from "js/jquery";
import BootstrapJs from "js/bootstrap";

import React from "react";
import ReactDOM from "react-dom";

import IndexPage from "./index.html";
import Style from "./index.css";
import Favicon from "./favicon.ico";
import App from "./app";

const rootElement = document.getElementById("root");
rootElement.className = Style.root;

ReactDOM.render(<App />, rootElement);