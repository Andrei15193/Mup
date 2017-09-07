// This is a generated file.
import React from "react";
import { Link } from "react-router-dom";
import join from "classnames";

import routePath from "route-path";
import Bootstrap from "css/bootstrap";

export default class extends React.PureComponent {
    constructor(props) {
       super(props);
    }

    render () {
        return (
            <div>
                <ol class={Bootstrap.breadcrumb}>
                    <li>
                        <Link to={routePath.documentation({ "member": "Mup" })}>Mup</Link>
                    </li>
                    <li class={Bootstrap.active}>HtmlWriterVisitor</li>
                </ol>
                <h2>HtmlWriterVisitor Class</h2>
                <p>Visitor for generating HTML from <Link to={routePath.documentation({ member: "Mup.IParseTree" })}>IParseTree</Link>.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>class</span> HtmlWriterVisitor : ParseTreeVisitor&lt;<span class={Bootstrap.textPrimary}>string</span>&gt;</code></pre>
                <h3>Constructors</h3>
                <table class={join(Bootstrap.table, Bootstrap.tableHover)}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Access Modifier</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.HtmlWriterVisitor()" })}>HtmlWriterVisitor()</Link>
                            </td>
                            <td>public</td>
                            <td>Initializes a new instance of the <Link to={routePath.documentation({ member: "Mup.HtmlWriterVisitor" })}>HtmlWriterVisitor</Link> class.</td>
                        </tr>
                    </tbody>
                </table>
                <h3>Properties</h3>
                <table class={join(Bootstrap.table, Bootstrap.tableHover)}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Access Modifier</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.HtmlStringBuilder" })}>HtmlStringBuilder</Link>
                            </td>
                            <td>protected get</td>
                            <td>Gets the <a href="https://msdn.microsoft.com/en-us/library/system.text.stringbuilder.aspx">StringBuilder</a> where the HTML is being written.</td>
                        </tr>
                    </tbody>
                </table>
                <h3>Methods</h3>
                <table class={join(Bootstrap.table, Bootstrap.tableHover)}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Access Modifier</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.AppendHtmlSafe(System.Char)" })}>AppendHtmlSafe(char)</Link>
                            </td>
                            <td>protected</td>
                            <td>Appends the HTML encoded, if necessary, character.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.AppendHtmlSafe(System.String)" })}>AppendHtmlSafe(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Appends the HTML encoded, if necessary, text.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.BeginVisit()" })}>BeginVisit()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of the visit operation. This method is called before any other visit method.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.BeginVisitAsync(System.Threading.CancellationToken)" })}>BeginVisitAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of the visit operation. This method is called before any other visit method.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.EndVisit()" })}>EndVisit()</Link>
                            </td>
                            <td>protected</td>
                            <td>Completes the visit operation. This method is called after all other methods.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.EndVisitAsync(System.Threading.CancellationToken)" })}>EndVisitAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously completes the visit operation. This method is called after all other methods.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.GetResult()" })}>GetResult()</Link>
                            </td>
                            <td>protected</td>
                            <td>Gets the visitor result. This values is used only after the visit operation completes.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitEmphasisBeginning()" })}>VisitEmphasisBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of an emphasised element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitEmphasisEnding()" })}>VisitEmphasisEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of an emphasised element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading1Beginning()" })}>VisitHeading1Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 1 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading1Ending()" })}>VisitHeading1Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 1 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading2Beginning()" })}>VisitHeading2Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 2 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading2Ending()" })}>VisitHeading2Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 2 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading3Beginning()" })}>VisitHeading3Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 3 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading3Ending()" })}>VisitHeading3Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 3 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading4Beginning()" })}>VisitHeading4Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 4 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading4Ending()" })}>VisitHeading4Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 4 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading5Beginning()" })}>VisitHeading5Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 5 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading5Ending()" })}>VisitHeading5Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 5 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading6Beginning()" })}>VisitHeading6Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 6 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHeading6Ending()" })}>VisitHeading6Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 6 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHorizontalRule()" })}>VisitHorizontalRule()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a horizontal rule.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHyperlinkBeginning(System.String)" })}>VisitHyperlinkBeginning(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a hyperlink.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitHyperlinkEnding()" })}>VisitHyperlinkEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a hyperlink.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitImage(System.String,System.String)" })}>VisitImage(string, string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits an image.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitLineBreak()" })}>VisitLineBreak()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a line break.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitListItemBeginning()" })}>VisitListItemBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a list item.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitListItemEnding()" })}>VisitListItemEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a list item.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitOrderedListBeginning()" })}>VisitOrderedListBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of an ordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitOrderedListEnding()" })}>VisitOrderedListEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of an ordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitParagraphBeginning()" })}>VisitParagraphBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a paragraph.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitParagraphEnding()" })}>VisitParagraphEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a paragraph.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitPlugin(System.String)" })}>VisitPlugin(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a plugin.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitPreformattedBlock(System.String)" })}>VisitPreformattedBlock(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a preformatted block.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitPreformattedText(System.String)" })}>VisitPreformattedText(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a preformatted text inside a block (e.g.: paragraph, list item or table).</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitStrongBeginning()" })}>VisitStrongBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a strong element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitStrongEnding()" })}>VisitStrongEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a strong element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitTableBeginning()" })}>VisitTableBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a table.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitTableCellBeginning()" })}>VisitTableCellBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a table cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitTableCellEnding()" })}>VisitTableCellEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a table cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitTableEnding()" })}>VisitTableEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a table.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitTableHeaderCellBeginning()" })}>VisitTableHeaderCellBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a table header cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitTableHeaderCellEnding()" })}>VisitTableHeaderCellEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a table header cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitTableRowBeginning()" })}>VisitTableRowBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a table row.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitTableRowEnding()" })}>VisitTableRowEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a table row.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitText(System.String)" })}>VisitText(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits plain text. This method may be called multiple times consecutively.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitUnorderedListBeginning()" })}>VisitUnorderedListBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of an unordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.HtmlWriterVisitor.VisitUnorderedListEnding()" })}>VisitUnorderedListEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of an unordered list.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
