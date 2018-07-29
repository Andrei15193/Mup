import React from "react";
import PropTypes from "prop-types";

import Style from "mup/style";
import { Page } from "./layout";

export class Home extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <Page>
                <h1>Home</h1>
                <Description />
                <LanguageSupport />
            </Page>
        );
    }
};

class Description extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <p>
                    Mup, which is short for <strong>M</strong>ark<strong>u</strong>p <strong>P</strong>arser, is a cross-platform library written in
                    C#. It targets .NET Standard 1.0 making it available for a wide variety of devices and applications.
                </p>
                <p>
                    The main purpose of the library is to support
                    parsing <a href="https://en.wikipedia.org/wiki/Lightweight_markup_language" target="_blank">Lightweight Markup Languages</a> into
                    various output formats, such as HTML, XHTML, XML, Word Documents, Excel Documents, and any other type of document.
                </p>
                <p>
                    The library does not expose types for each mentioned format, but it is made to be extensible. Any parsed text can be run through
                    a custom visitor which traverses the resulting parse tree allowing the developer to specify what exactly needs to be generated at every step.
                </p>
                <p>
                    To keep it lightweight, the library only provides parsers for several languages (see below) and
                    an <abbr title="HyperText Markup Language">HTML</abbr> visitor which allows users to
                    generate <abbr title="HyperText Markup Language">HTML</abbr> from parsed text. With each increment (or major version), the library will bring
                    a new parser into the fold and thus supporting more languages. The end goal is to support most, if not all, <a href="https://en.wikipedia.org/wiki/Lightweight_markup_language" target="_blank">Lightweight Markup Languages</a>.
                </p>
            </div>
        );
    }
}

class LanguageSupport extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <h3>Language Support</h3>
                <table className={[Style.table, Style.tableHover].join(" ")}>
                    <thead>
                        <tr>
                            <th>Language</th>
                            <th>Phase</th>
                            <th>Release Version</th>
                            <th width="60%">Elements</th>
                        </tr>
                    </thead>
                    <tbody>
                        <CreoleStatus />
                        <CommonMarkStatus />
                    </tbody>
                </table>
            </div>
        );
    }
}

const Phase = {
    Planned: "Planned",
    Alpha: "Alpha",
    Beta: "Beta",
    Released: "Released"
};

const ElementStatus = {
    Planned: "planned",
    InProgress: "in-progress",
    Done: "done"
};

class CreoleStatus extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <WikiLanguage name="Creole" siteUrl="http://www.wikicreole.org/wiki/Home/" phase={Phase.Released} version={{ major: 1, minor: 0, patch: 0 }}>
                <Element name="headings" status={ElementStatus.Done} />
                <Element name="paragraphs" status={ElementStatus.Done} />
                <Element name="lists" status={ElementStatus.Done} />
                <Element name="tables" status={ElementStatus.Done} />
                <Element name="inline code" status={ElementStatus.Done} />
                <Element name="code blocks" status={ElementStatus.Done} />
                <Element name="horizontal rules" status={ElementStatus.Done} />
                <Element name="emphasis (italics)" status={ElementStatus.Done} />
                <Element name="strong (bold)" status={ElementStatus.Done} />
                <Element name="inline hyperlinks" status={ElementStatus.Done} />
                <Element name="hyperlinks" status={ElementStatus.Done} />
                <Element name="images" status={ElementStatus.Done} />
                <Element name="line breaks" status={ElementStatus.Done} />
                <Element name="plugins" status={ElementStatus.Done} />
            </WikiLanguage>
        );
    }
}

class CommonMarkStatus extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <WikiLanguage name="CommonMark" siteUrl="http://commonmark.org/" phase={Phase.Planned} version={{ major: 2, minor: 0, patch: 0 }}>
                <Element name="headings" status={ElementStatus.Planned} />
                <Element name="paragraphs" status={ElementStatus.Planned} />
                <Element name="lists" status={ElementStatus.Planned} />
                <Element name="block quotes" status={ElementStatus.Planned} />
                <Element name="inline code" status={ElementStatus.Planned} />
                <Element name="code blocks" status={ElementStatus.Planned} />
                <Element name="horizontal rules" status={ElementStatus.Planned} />
                <Element name="emphasis (italics)" status={ElementStatus.Planned} />
                <Element name="strong (bold)" status={ElementStatus.Planned} />
                <Element name="inline hyperlinks" status={ElementStatus.Planned} />
                <Element name="hyperlinks" status={ElementStatus.Planned} />
                <Element name="images" status={ElementStatus.Planned} />
                <Element name="line breaks" status={ElementStatus.Planned} />
            </WikiLanguage>
        );
    }
}

class WikiLanguage extends React.PureComponent {
    static get propTypes() {
        return {
            name: PropTypes.string.isRequired,
            siteUrl: PropTypes.string.isRequired,
            phase: PropTypes.oneOf(Object.getOwnPropertyNames(Phase).map(name => Phase[name])).isRequired,
            version: PropTypes.shape({
                major: PropTypes.number.isRequired,
                minor: PropTypes.number.isRequired,
                patch: PropTypes.number.isRequired
            }).isRequired
        };
    }

    constructor(props) {
        super(props);
    }

    render() {
        const components = [];
        React.Children.forEach(this.props.children, child => {
            components.push(child);
            components.push(' ');
        });
        if (components.length > 0)
            components.pop();

        return (
            <tr>
                <td><strong><a href={this.props.siteUrl} target="_blank">{this.props.name}</a></strong></td>
                <td>{this.props.phase} </td>
                <td>{this.props.version.major}.{this.props.version.minor}.{this.props.version.patch}</td>
                <td>{components}</td>
            </tr>
        )
    }
}

class Element extends React.PureComponent {
    static get propTypes() {
        return {
            name: PropTypes.string.isRequired,
            status: PropTypes.oneOf(Object.getOwnPropertyNames(ElementStatus).map(name => ElementStatus[name])).isRequired
        };
    }

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <span className={[Style.badge, this.getBadgeColorClassName()].join(" ")}>
                {this.props.name}
            </span>
        )
    }

    getBadgeColorClassName() {
        switch (this.props.status) {
            case ElementStatus.InProgress:
                return Style.badgeInfo;

            case ElementStatus.Done:
                return Style.badgeSuccess;

            default:
                return Style.badgeSecondary;
        }
    }
}