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
                    <li class={Bootstrap.active}>ParseTreeVisitor</li>
                </ol>
                <h2>ParseTreeVisitor Class</h2>
                <p>Base class of all parse tree visitors.</p>
                <pre><code><span class={Bootstrap.textPrimary}>public</span> <span class={Bootstrap.textPrimary}>abstract</span> <span class={Bootstrap.textPrimary}>class</span> ParseTreeVisitor</code></pre>
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
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.BeginVisit()" })}>BeginVisit()</Link>
                            </td>
                            <td>protected</td>
                            <td>Initializes the visitor. This method is called before any visit method.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.BeginVisitAsync(System.Threading.CancellationToken)" })}>BeginVisitAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously initializes the visitor. This method is called before any visit method.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.EndVisit()" })}>EndVisit()</Link>
                            </td>
                            <td>protected</td>
                            <td>Completes the visit operation. This method is called after all visit methods.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.EndVisitAsync(System.Threading.CancellationToken)" })}>EndVisitAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously completes the visit operation. This method is called after all visit methods.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitCodeFragment(System.String)" })}>VisitCodeFragment(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a code fragment inside a block (e.g.: paragraph, list item or table).</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitCodeFragmentAsync(System.String,System.Threading.CancellationToken)" })}>VisitCodeFragmentAsync(string, CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits a code fragment inside a block (e.g.: paragraph, list item or table).</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitEmphasisBeginning()" })}>VisitEmphasisBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of an emphasised element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitEmphasisBeginningAsync(System.Threading.CancellationToken)" })}>VisitEmphasisBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of an emphasised element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitEmphasisEnding()" })}>VisitEmphasisEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of an emphasised element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitEmphasisEndingAsync(System.Threading.CancellationToken)" })}>VisitEmphasisEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of an emphasised element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading1Beginning()" })}>VisitHeading1Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 1 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading1BeginningAsync(System.Threading.CancellationToken)" })}>VisitHeading1BeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a level 1 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading1Ending()" })}>VisitHeading1Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 1 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading1EndingAsync(System.Threading.CancellationToken)" })}>VisitHeading1EndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a level 1 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading2Beginning()" })}>VisitHeading2Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 2 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading2BeginningAsync(System.Threading.CancellationToken)" })}>VisitHeading2BeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a level 2 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading2Ending()" })}>VisitHeading2Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 2 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading2EndingAsync(System.Threading.CancellationToken)" })}>VisitHeading2EndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a level 2 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading3Beginning()" })}>VisitHeading3Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 3 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading3BeginningAsync(System.Threading.CancellationToken)" })}>VisitHeading3BeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a level 3 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading3Ending()" })}>VisitHeading3Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 3 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading3EndingAsync(System.Threading.CancellationToken)" })}>VisitHeading3EndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a level 3 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading4Beginning()" })}>VisitHeading4Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 4 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading4BeginningAsync(System.Threading.CancellationToken)" })}>VisitHeading4BeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a level 4 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading4Ending()" })}>VisitHeading4Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 4 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading4EndingAsync(System.Threading.CancellationToken)" })}>VisitHeading4EndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a level 4 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading5Beginning()" })}>VisitHeading5Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 5 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading5BeginningAsync(System.Threading.CancellationToken)" })}>VisitHeading5BeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a level 5 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading5Ending()" })}>VisitHeading5Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 5 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading5EndingAsync(System.Threading.CancellationToken)" })}>VisitHeading5EndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a level 5 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading6Beginning()" })}>VisitHeading6Beginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a level 6 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading6BeginningAsync(System.Threading.CancellationToken)" })}>VisitHeading6BeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a level 6 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading6Ending()" })}>VisitHeading6Ending()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a level 6 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHeading6EndingAsync(System.Threading.CancellationToken)" })}>VisitHeading6EndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a level 6 heading.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHorizontalRule()" })}>VisitHorizontalRule()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a horizontal rule.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHorizontalRuleAsync(System.Threading.CancellationToken)" })}>VisitHorizontalRuleAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits a horizontal rule.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHyperlinkBeginning(System.String)" })}>VisitHyperlinkBeginning(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a hyperlink.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHyperlinkBeginningAsync(System.String,System.Threading.CancellationToken)" })}>VisitHyperlinkBeginningAsync(string, CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a hyperlink.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHyperlinkEnding()" })}>VisitHyperlinkEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a hyperlink.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitHyperlinkEndingAsync(System.Threading.CancellationToken)" })}>VisitHyperlinkEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a hyperlink.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitImage(System.String,System.String)" })}>VisitImage(string, string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits an image.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitImageAsync(System.String,System.String,System.Threading.CancellationToken)" })}>VisitImageAsync(string, string, CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits an image.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitLineBreak()" })}>VisitLineBreak()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a line break.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitLineBreakAsync(System.Threading.CancellationToken)" })}>VisitLineBreakAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits a line break.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitListItemBeginning()" })}>VisitListItemBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a list item.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitListItemBeginningAsync(System.Threading.CancellationToken)" })}>VisitListItemBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a list item.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitListItemEnding()" })}>VisitListItemEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a list item.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitListItemEndingAsync(System.Threading.CancellationToken)" })}>VisitListItemEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a list item.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitOrderedListBeginning()" })}>VisitOrderedListBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of an ordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitOrderedListBeginningAsync(System.Threading.CancellationToken)" })}>VisitOrderedListBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of an ordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitOrderedListEnding()" })}>VisitOrderedListEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of an ordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitOrderedListEndingAsync(System.Threading.CancellationToken)" })}>VisitOrderedListEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of an ordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitParagraphBeginning()" })}>VisitParagraphBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a paragraph.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitParagraphBeginningAsync(System.Threading.CancellationToken)" })}>VisitParagraphBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a paragraph.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitParagraphEnding()" })}>VisitParagraphEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a paragraph.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitParagraphEndingAsync(System.Threading.CancellationToken)" })}>VisitParagraphEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a paragraph.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitPlugin(System.String)" })}>VisitPlugin(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a plugin.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitPluginAsync(System.String,System.Threading.CancellationToken)" })}>VisitPluginAsync(string, CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits a plugin.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitPreformattedBlock(System.String)" })}>VisitPreformattedBlock(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits a preformatted block.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitPreformattedBlockAsync(System.String,System.Threading.CancellationToken)" })}>VisitPreformattedBlockAsync(string, CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits a preformatted block.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitStrongBeginning()" })}>VisitStrongBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a strong element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitStrongBeginningAsync(System.Threading.CancellationToken)" })}>VisitStrongBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a strong element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitStrongEnding()" })}>VisitStrongEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a strong element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitStrongEndingAsync(System.Threading.CancellationToken)" })}>VisitStrongEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a strong element.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableBeginning()" })}>VisitTableBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a table.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableBeginningAsync(System.Threading.CancellationToken)" })}>VisitTableBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a table.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableCellBeginning()" })}>VisitTableCellBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a table cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableCellBeginningAsync(System.Threading.CancellationToken)" })}>VisitTableCellBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a table cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableCellEnding()" })}>VisitTableCellEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a table cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableCellEndingAsync(System.Threading.CancellationToken)" })}>VisitTableCellEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a table cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableEnding()" })}>VisitTableEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a table.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableEndingAsync(System.Threading.CancellationToken)" })}>VisitTableEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a table.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableHeaderCellBeginning()" })}>VisitTableHeaderCellBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a table header cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableHeaderCellBeginningAsync(System.Threading.CancellationToken)" })}>VisitTableHeaderCellBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a table header cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableHeaderCellEnding()" })}>VisitTableHeaderCellEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a table header cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableHeaderCellEndingAsync(System.Threading.CancellationToken)" })}>VisitTableHeaderCellEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a table header cell.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableRowBeginning()" })}>VisitTableRowBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of a table row.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableRowBeginningAsync(System.Threading.CancellationToken)" })}>VisitTableRowBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of a table row.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableRowEnding()" })}>VisitTableRowEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of a table row.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTableRowEndingAsync(System.Threading.CancellationToken)" })}>VisitTableRowEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of a table row.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitText(System.String)" })}>VisitText(string)</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits plain text. This method may be called multiple times consecutively.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitTextAsync(System.String,System.Threading.CancellationToken)" })}>VisitTextAsync(string, CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits plain text. This method may be called multiple times consecutively.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitUnorderedListBeginning()" })}>VisitUnorderedListBeginning()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the beginning of an unordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitUnorderedListBeginningAsync(System.Threading.CancellationToken)" })}>VisitUnorderedListBeginningAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the beginning of an unordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitUnorderedListEnding()" })}>VisitUnorderedListEnding()</Link>
                            </td>
                            <td>protected</td>
                            <td>Visits the ending of an unordered list.</td>
                        </tr>
                        <tr>
                            <td>
                                <Link to={routePath.documentation({ "member": "Mup.ParseTreeVisitor.VisitUnorderedListEndingAsync(System.Threading.CancellationToken)" })}>VisitUnorderedListEndingAsync(CancellationToken)</Link>
                            </td>
                            <td>protected</td>
                            <td>Asynchronously visits the ending of an unordered list.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
};
