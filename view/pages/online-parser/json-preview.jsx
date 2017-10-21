import React from "react";
import join from "classnames";

import Bootstrap from "css/bootstrap";

import ElementType from "./element-type";
import CSharpKeywords from "./keywords/c-sharp.json";
import JavaScriptKeywords from "./keywords/javascript.json";
import SqlKeywords from "./keywords/sql.json";

export default class JsonPreview extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        this._counter = 0;
        const components = this.props.elements.map(element => this._renderElement(element), this);
        return (
            <div>
                {components}
            </div>
        );
    }

    _renderContent(content) {
        return content.map((element) => this._renderElement(element), this);
    }

    _renderPreformattedBlock(preformattedContent) {
        const preformattedText = preformattedContent.preformattedText;
        let keywordFinder = this._getKeywordFinder(preformattedContent.language);
        if (keywordFinder != null)
            return this._highlighSyntax(preformattedText, keywordFinder);
        else
            return preformattedText;
    }

    _getKeywordFinder(language) {
        if (language)
            switch (language.toLowerCase()) {
                case "c#":
                case "cs":
                case "csharp":
                    return this._getCSharpKeywordFinder();

                case "javascript":
                case "js":
                    return this._getJavaScriptKeywordFinder();

                case "sql":
                    return this._getSqlKeywordFinder();
                    break;
            }
        else
            return null;
    }

    _highlighSyntax(preformattedText, keywordFinder) {
        const result = [];
        let match;
        let lastEndIndex = 0;
        while ((match = keywordFinder(preformattedText)) !== null) {
            if (lastEndIndex < match.index)
                result.push(preformattedText.substr(lastEndIndex, (match.index - lastEndIndex)));

            result.push(
                <span key={this._counter++} class={Bootstrap.textPrimary}>
                    {match.keyword}
                </span>
            );
            lastEndIndex = (match.index + match.keyword.length);
        }
        if (lastEndIndex != preformattedText.length)
            result.push(preformattedText.substr(lastEndIndex));
        return result;
    }

    _getCSharpKeywordFinder() {
        const regex = new RegExp("(^|[^@])\\b(" + CSharpKeywords.join("|") + ")\\b", "g");
        return function (text) {
            const match = regex.exec(text);
            if (match !== null)
                return {
                    index: (match.index + match[1].length),
                    keyword: match[2]
                };
            else
                return null;
        };
    }

    _getJavaScriptKeywordFinder() {
        const regex = new RegExp("\\b(" + JavaScriptKeywords.join("|") + ")\\b", "g");
        return function (text) {
            const match = regex.exec(text)
            if (match !== null)
                return {
                    index: match.index,
                    keyword: match[1]
                };
            else
                return null;
        };
    }

    _getSqlKeywordFinder() {
        const regex = new RegExp("(^|[^\\[])\\b(" + SqlKeywords.join("|") + ")\\b([^\\]]|$)", "gi");
        return function (text) {
            const match = regex.exec(text);
            if (match !== null)
                return {
                    index: (match.index + match[1].length),
                    keyword: match[2]
                };
            else
                return null;
        };
    }

    _renderElement(element) {
        switch (element.type) {
            case ElementType.Heading1:
                return (
                    <h1 key={this._counter++}>
                        {this._renderContent(element.content)}
                    </h1>
                );

            case ElementType.Heading2:
                return (
                    <h2 key={this._counter++}>
                        {this._renderContent(element.content)}
                    </h2>
                );

            case ElementType.Heading3:
                return (
                    <h3 key={this._counter++}>
                        {this._renderContent(element.content)}
                    </h3>
                );

            case ElementType.Heading4:
                return (
                    <h4 key={this._counter++}>
                        {this._renderContent(element.content)}
                    </h4>
                );

            case ElementType.Heading5:
                return (
                    <h5 key={this._counter++}>
                        {this._renderContent(element.content)}
                    </h5>
                );

            case ElementType.Heading6:
                return (
                    <h6 key={this._counter++}>
                        {this._renderContent(element.content)}
                    </h6>
                );

            case ElementType.Paragraph:
                return (
                    <p key={this._counter++}>
                        {this._renderContent(element.content)}
                    </p>
                );

            case ElementType.PreformattedBlock:
                return (
                    <pre key={this._counter++}><code>
                        {this._renderPreformattedBlock(element.content)}
                    </code></pre>
                );

            case ElementType.Table:
                return (
                    <table key={this._counter++} class={join(Bootstrap.table, Bootstrap.tableHover)}><tbody>
                        {this._renderContent(element.content)}
                    </tbody></table>
                );

            case ElementType.TableRow:
                return (
                    <tr key={this._counter++}>
                        {this._renderContent(element.content)}
                    </tr>
                );

            case ElementType.TableHeaderCell:
                return (
                    <th key={this._counter++}>
                        {this._renderContent(element.content)}
                    </th>
                );

            case ElementType.TableCell:
                return (
                    <td key={this._counter++}>
                        {this._renderContent(element.content)}
                    </td>
                );

            case ElementType.UnorderedList:
                return (
                    <ul key={this._counter++}>
                        {this._renderContent(element.content)}
                    </ul>
                );

            case ElementType.ListItem:
                return (
                    <li key={this._counter++}>
                        {this._renderContent(element.content)}
                    </li>
                );

            case ElementType.HorizontalRule:
                return (
                    <hr key={this._counter++} />
                );

            case ElementType.OrderedList:
                return (
                    <ol key={this._counter++}>
                        {this._renderContent(element.content)}
                    </ol>
                );

            case ElementType.Strong:
                return (
                    <strong key={this._counter++}>
                        {this._renderContent(element.content)}
                    </strong>
                );

            case ElementType.Emphasis:
                return (
                    <em key={this._counter++}>
                        {this._renderContent(element.content)}
                    </em>
                );

            case ElementType.LineBreak:
                return (
                    <br key={this._counter++} />
                );

            case ElementType.Hyperlink:
                return (
                    <a key={this._counter++} href={element.destination}>
                        {this._renderContent(element.content)}
                    </a>
                );

            case ElementType.Image:
                return (
                    <img key={this._counter++} src={element.source} alt={element.alternative} />
                );

            case ElementType.Code:
                return (
                    <code key={this._counter++}>
                        {element.content}
                    </code>
                );

            case ElementType.Text:
                return element.content;

        }
    }
};