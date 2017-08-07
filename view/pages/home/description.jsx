import React from "react";

export default class Description extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <p>
                    Mup, which is short for <strong>M</strong>ark<strong>u</strong>p <strong>P</strong>arser, is a cross-platform library written in
                    C#. It targets .NET Core 1.0 making it available for a wide variety of devices and applications.
                </p>
                <p>
                    The main purpose of the library is to support
                    parsing <a href="https://en.wikipedia.org/wiki/Lightweight_markup_language">Lightweight Markup Languages</a> into
                    various output formats, such as HTML, XHTML, XML, Word Documents, Excel Documents, and any other type of document.
                </p>
                <p>
                    The library does not expose types for each mentioned format, but it is made to be extensible. Any parsed text can be run through
                    a custom visitor which, um, “visits” the results allowing the developer to specify what exactly needs to be generated at every step.
                </p>
                <p>
                    The library does not expose types for each mentioned format, but it is made to be extensible. Any parsed text can be run through
                    a custom visitor which, um, “visits” the results allowing the developer to specify what exactly needs to be generated at every step.
                </p>
                <p>
                    To keep it lightweight, the library only provides parsers for several languages (see below) and
                    an <abbr title="HyperText Markup Language">HTML</abbr> visitor which allows users to
                    generate <abbr title="HyperText Markup Language">HTML</abbr> from parsed text. With each increment (or major version if
                    that helps), the library will bring a new parser into the fold and thus supporting more languages.
                    The end goal is to support all <a href="https://en.wikipedia.org/wiki/Lightweight_markup_language">Lightweight Markup Languages</a>, however that may
                    take some time, thus the incremental approach.
                </p>
            </div>
        );
    }
}