import React from "react";

import Style from "mup/style";
import CSharpKeywords from "./c-sharp-keywords.json";
import JavaScriptKeywords from "./javascript-keywords.json";
import SqlKeywords from "./sql-keywords.json";

const ElementType = {
    heading1: "heading1",
    heading2: "heading2",
    heading3: "heading3",
    heading4: "heading4",
    heading5: "heading5",
    heading6: "heading6",
    paragraph: "paragraph",
    preformattedBlock: "preformattedblock",
    table: "table",
    tableRow: "tablerow",
    tableHeaderCell: "tableheadercell",
    tableCell: "tablecell",
    unorderedList: "unorderedlist",
    listItem: "listitem",
    horizontalRule: "horizontalrule",
    orderedList: "orderedlist",
    strong: "strong",
    emphasis: "emphasis",
    lineBreak: "linebreak",
    hyperlink: "hyperlink",
    image: "image",
    code: "code",
    text: "text"
};

export default class JsonPreview extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        this._counter = 0;
        const children = (
            Array.isArray(this.props.json) ?
                this.props.json.map(element => renderElement.call(this, element), this) :
                renderElement.call(this, this.props.json)
        );
        delete this._counter;

        return (
            <div>
                {children}
            </div>
        );
    }
}

function renderContent(content) {
    return content.map((element) => renderElement.call(this, element), this);
}

function renderPreformattedBlock(preformattedContent) {
    const preformattedText = preformattedContent.preformattedText;
    let keywordFinder = getKeywordFinder.call(this, preformattedContent.language);
    if (keywordFinder != null)
        return highlighSyntax.call(this, preformattedText, keywordFinder);
    else
        return preformattedText;
}

function getKeywordFinder(language) {
    if (language)
        switch (language.toLowerCase()) {
            case "c#":
            case "cs":
            case "csharp":
                return getCSharpKeywordFinder();

            case "javascript":
            case "js":
                return getJavaScriptKeywordFinder();

            case "sql":
                return getSqlKeywordFinder();
                break;
        }
    else
        return null;
}

function highlighSyntax(preformattedText, keywordFinder) {
    const result = [];
    let match;
    let lastEndIndex = 0;
    while ((match = keywordFinder(preformattedText)) !== null) {
        if (lastEndIndex < match.index)
            result.push(preformattedText.substr(lastEndIndex, (match.index - lastEndIndex)));

        result.push(
            <span key={this._counter++} className={Style.textPrimary}>
                {match.keyword}
            </span>
        );
        lastEndIndex = (match.index + match.keyword.length);
    }
    if (lastEndIndex != preformattedText.length)
        result.push(preformattedText.substr(lastEndIndex));
    return result;
}

function getCSharpKeywordFinder() {
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

function getJavaScriptKeywordFinder() {
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

function getSqlKeywordFinder() {
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

function renderElement(element) {
    switch (element.type) {
        case ElementType.heading1:
            return (
                <h1 key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </h1>
            );

        case ElementType.heading2:
            return (
                <h2 key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </h2>
            );

        case ElementType.heading3:
            return (
                <h3 key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </h3>
            );

        case ElementType.heading4:
            return (
                <h4 key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </h4>
            );

        case ElementType.heading5:
            return (
                <h5 key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </h5>
            );

        case ElementType.heading6:
            return (
                <h6 key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </h6>
            );

        case ElementType.paragraph:
            return (
                <p key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </p>
            );

        case ElementType.preformattedBlock:
            return (
                <pre key={this._counter++}><code>
                    {renderPreformattedBlock.call(this, element.content)}
                </code></pre>
            );

        case ElementType.table:
            return (
                <table key={this._counter++} className={[Style.table, Style.tableHover].join(" ")}><tbody>
                    {renderContent.call(this, element.content)}
                </tbody></table>
            );

        case ElementType.tableRow:
            return (
                <tr key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </tr>
            );

        case ElementType.tableHeaderCell:
            return (
                <th key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </th>
            );

        case ElementType.tableCell:
            return (
                <td key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </td>
            );

        case ElementType.unorderedList:
            return (
                <ul key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </ul>
            );

        case ElementType.listItem:
            return (
                <li key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </li>
            );

        case ElementType.horizontalRule:
            return (
                <hr key={this._counter++} />
            );

        case ElementType.orderedList:
            return (
                <ol key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </ol>
            );

        case ElementType.strong:
            return (
                <strong key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </strong>
            );

        case ElementType.emphasis:
            return (
                <em key={this._counter++}>
                    {renderContent.call(this, element.content)}
                </em>
            );

        case ElementType.lineBreak:
            return (
                <br key={this._counter++} />
            );

        case ElementType.hyperlink:
            return (
                <a key={this._counter++} href={element.destination}>
                    {renderContent.call(this, element.content)}
                </a>
            );

        case ElementType.image:
            return (
                <img key={this._counter++} src={element.source} alt={element.alternative} />
            );

        case ElementType.code:
            return (
                <code key={this._counter++}>
                    {element.content}
                </code>
            );

        case ElementType.text:
            return element.content;

        default:
            return (
                <p key={this._counter++}>
                    {element.content || element}
                </p>
            );
    }
}