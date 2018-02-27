import React from "react";
import { Link, withRouter } from "react-router-dom";

import Style from "mup/style";
import { Routes } from "../../routes";
import MsdnLinks from "./msdn-links.json";

export const GettingStarted = withRouter(class extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <h2>Getting Started</h2>

                <h3>Introduction</h3>

                <p>The library has a few core types that make it work: <Link to={Routes.documentation({ member: "mup.imarkupparser", framework: this.props.match.params.framework })}>IMarkupParser</Link>, <Link to={Routes.documentation({ member: "mup.iparsetree", framework: this.props.match.params.framework })}>IParseTree</Link> and <Link to={Routes.documentation({ member: "mup.parsetreevisitor", framework: this.props.match.params.framework })}>ParseTreeVisitor</Link>.</p>

                <p>Each parser (currently just Creole) implements the <Link to={Routes.documentation({ member: "mup.imarkupparser", framework: this.props.match.params.framework })}>IMarkupParser</Link> interface which exposes a number of methods that allow parsing a <a href={MsdnLinks["system.string"]} target="_blank">string</a> or text from a <a href={MsdnLinks["system.io.textreader"]} target="_blank">TextReader</a>. Each parser supports both synchronous and asynchronous models allowing its users to consume the <abbr title="Application Programming Interface">API</abbr> any way they want.</p>

                <p>The result of any parse method is ultimately an <Link to={Routes.documentation({ member: "mup.iparsetree", framework: this.props.match.params.framework })}>IParseTree</Link>. Surprisingly or not, this interface does not expose something like a root node or anything related to what one would expect when seeing the word &quot;tree&quot;.</p>

                <p>This is because trees can have different representations. For instance, we can have the usual example where we have a root node which exposes a property containing a number of nodes that are in fact child nodes, each child node also exposes such a property that contains their child nodes and so on. A different representation can be a flat one where the entire tree is stored as a list of elements that mark the beginning and end of each node.</p>

                <p>Regardless of how we represent a parse tree, we need to be able to traverse it in order to generate a specific output, say <abbr title="HyperText Markup Language">HTML</abbr>. This is where a <Link to={Routes.documentation({ member: "mup.parsetreevisitor", framework: this.props.match.params.framework })}>ParseTreeVisitor</Link> comes into play. Any <Link to={Routes.documentation({ member: "mup.iparsetree", framework: this.props.match.params.framework })}>IParseTree</Link> exposes methods that accept a <Link to={Routes.documentation({ member: "mup.parsetreevisitor", framework: this.props.match.params.framework })}>ParseTreeVisitor</Link>, the entire logic for traversing the tree is encapsulated inside itself. Each time a node is being visited, a specific method for that node is called on the visitor. This helps keep the interface clean and completely decouple the language that is being parsed from the desired output format. Any new markup parser will work with existing visitors and any new visitor will work with any existing parser.</p>

                <p>The one common rule for all parse trees is that they are all traversed in pre-order (see <a href="https://en.wikipedia.org/wiki/Tree_traversal">Tree Traversal <small>(Wikipedia)</small></a> for more about this topic).</p>

                <p>For instance, given the Creole block <code>plain //emphasized// text</code> the visitor will have the following methods called:</p>

                <ol>
                    <li>VisitParagraphBeginning()</li>
                    <li>VisitPlainText(&quot;plain &quot;)</li>
                    <li>VisitEmphasisBeginning()</li>
                    <li>VisitPlainText(&quot;emphasized&quot;)</li>
                    <li>VisitEmphasisEnding()</li>
                    <li>VisitPlainText(&quot; text&quot;)</li>
                    <li>VisistParagraphEnding()</li>
                </ol>

                <p>When outputting <abbr title="HyperText Markup Language">HTML</abbr> we will write the corresponding tags for each of the visitor methods. The <code>VisitParagraphBeginning</code> will write <code>&lt;p&gt;</code> while <code>VisistParagraphEnding</code> will write <code>&lt;/p&gt;</code>. Similar for <code>VisitEmphasisBeginning</code> and <code>VisitEmphasisEnding</code> by writing <code>&lt;em&gt;</code> and <code>&lt;/em&gt;</code>, respectively. Thus, we have our output: <code>&lt;p&gt;plain &lt;em&gt;emphasized&lt;/em&gt; text&lt;/p&gt;</code>.</p>

                <p>For each desired output format (e.g.: <abbr title="HyperText Markup Language">HTML</abbr>, <abbr title="eXtensible HyperText Markup Language">XHTML</abbr>, <abbr title="eXtensible Markup Language">XML</abbr>, Word Document and so on) there will be one visitor. This will keep the responsibilities clear for each visitor, additionally they may be configurable depending on the output format (e.g.: indentation settings for <abbr title="HyperText Markup Language">HTML</abbr> output, minification and so on).</p>

                <p>In some cases, we would like to write directly to the file system or use the visitor to send data over a network as the parse tree is being visited. When working with streams the recommended way to use them is through asynchronous programming. This will help us keep the application responsive and free up thread time while hardware components do the actual writing.</p>

                <p>Mup is built with asynchronous programming in mind, from start until end there is support for <a href="https://docs.microsoft.com/en-us/dotnet/csharp/async" target="_blank">async methods</a>. When implementing a custom <Link to={Routes.documentation({ member: "mup.parsetreevisitor", framework: this.props.match.params.framework })}>ParseTreeVisitor</Link> (the library only exposes one that outputs <abbr title="HyperText Markup Language">HTML</abbr> to a <a href={MsdnLinks["system.string"]} target="_blank">string</a>) we can override both the asynchronous method and its synchronous counterpart.</p>

                <p>Below is the pattern after each method pair is defined:</p>

                <pre><code>
                    <Keyword>protected</Keyword> <Keyword>virtual</Keyword> <Keyword>async</Keyword>{` Task Visit`}<em>{`{Element}`}</em>{`Async(CancellationToken cancellationToken)
{
    Visit`}<em>{`{Element}`}</em>{`();
    `}<Keyword>return</Keyword>{` CompletedTask;
}

`}<Keyword>protected</Keyword> <Keyword>virtual</Keyword> <Keyword>void</Keyword>{` Visit`}<em>{`{Element}`}</em>{`()
{
}`}
                </code></pre>

                <p>The synchronous method always gets called from its asynchronous counterpart which in turn is called when visiting the corresponding element. If we are implementing a visitor that outputs to something in-memory (e.g.: simply writing to a <a href={MsdnLinks["system.text.stringbuilder"]} target="_blank">StringBuilder</a>) we can just override the synchronous methods and not bother with asynchrony at all.</p>

                <p><strong>NOTE!</strong> When we are overriding the asynchronous method then the synchronous counterpart will not be invoked anymore unless we do that explicitly or by calling the base implementation (e.g. <code>{`base.Visit{Element}Async(cancellationToken)`}</code> or <code>{`Visit{Element}()`}</code>).</p>

                <h3>Using the Library</h3>

                <p>To exemplify how easy it is to get started with Mup, we will build a small web application that exposes just one endpoint which parses the body of a request and returns the <abbr title="HyperText Markup Language">HTML</abbr> for it, similar to what this site uses for the <Link to={Routes.onlineParser()}>Online Parser</Link> (if you haven&#39;t tried it, you should definitely give it a go, it&#39;s really cool).</p>

                <p>For our little web project we will be using .NET Core for convenience and because it is cross-platform, we do not really need to use Windows to go through this example.</p>

                <p>Now let us finally get on with it, let's create a folder for our little project called <code>mup-example</code> and run <code>dotnet new webapi</code> in the command line inside that folder. I use <a href="https://code.visualstudio.com/" target="_blank">Visual Studio Code</a> which has an integrated terminal so I can do my code editing and use the command line from one window.</p>

                <p>Now that we have our demo project created, we will continue by adding a <a href="https://www.nuget.org/" target="_blank">NuGet</a> dependency towards the <a href="https://www.nuget.org/packages/Mup" target="_blank">Mup package</a>. In the same command line that we used to initialize our project we only need to run <code>dotnet add package Mup</code>. This will add the latest release of Mup to our project, that's it with the configuration!</p>

                <p>Next, we will create our controller that will be using Mup to parse Creole and return the <abbr title="HyperText Markup Language">HTML</abbr> for it. Under <em>Controllers</em> we will create a new file: <em>CreoleController.cs</em>, the controller will have just one method for handling <strong>POST</strong> requests on the <strong>api/creole</strong> endpoint:</p>

                <pre><code>
                    <Keyword>{`using`}</Keyword>{` System.Threading;
`}<Keyword>using</Keyword>{` System.Threading.Tasks;
`}<Keyword>using</Keyword>{` Microsoft.AspNetCore.Mvc;
`}<Keyword>using</Keyword>{` Mup;

`}<Keyword>{`namespace`}</Keyword>{` mup_example.Controllers
{
    [Route(`}<StringLiteral>{`"api/creole"`}</StringLiteral>{`)]
    `}<Keyword>public</Keyword> <Keyword>class</Keyword>{` CreoleParserController : Controller
    {
        `}<Comment>// POST api/creole</Comment>{`
        [HttpPost]
        `}<Keyword>public</Keyword> <Keyword>async</Keyword>{` Task<IActionResult> Parse([FromBody] `}<Keyword>string</Keyword>{` text, CancellationToken cancellationToken)
        {
            CreoleParser parser = `}<Keyword>new</Keyword>{` CreoleParser();
            IParseTree parseTree = `}<Keyword>await</Keyword>{` parser.ParseAsync(text, cancellationToken);
            HtmlWriterVisitor htmlWriterVisitor = `}<Keyword>new</Keyword>{` HtmlWriterVisitor();
            `}<Keyword>string</Keyword>{` html = `}<Keyword>await</Keyword>{` parseTree.AcceptAsync(htmlWriterVisitor, cancellationToken);
            `}<Keyword>return</Keyword>{` Ok(html);
        }
    }
}`}
                </code></pre>

                <p>Time to test this, let's execute the application and use a Web API testing tool so we can make some requests! I use <a href="https://www.getpostman.com/" target="_blank">Postman</a> for this as I can organize my requests in collections and have them synced across devices (or just use Cloud storage for backup, fewer things to worry about).</p>

                <p>Usually, a .NET Core Web App will run on port 5000 locally. When we start the application in debug mode it will automatically open our favourite browser and from the address bar we can grab the base URL for our request: <code>http://localhost:5000/</code>. A 404 error shows in the browser, it is alright. The application only exposes one <abbr title="HyperText Transfer Protocol">HTTP</abbr> endpoint and no static content (like <em>index.html</em>), there is no default route.</p>

                <p>Now to make our request, we only need to append to the base URL the route we have defined for parsing Creole text: <code>http://localhost:5000/api/creole</code>, keep in mind that it is a <strong>POST</strong> request. In the body we can add the following (any Creole content works actually):</p>

                <pre><code>
                    <StringLiteral>{`"== Hey There!

This is a test"`}</StringLiteral>
                </code></pre>

                <p>When we call the endpoint we should get <code>&lt;h2&gt;Hey There!&lt;/h2&gt;&lt;p&gt;This is a test&lt;/p&gt;</code>.</p>

                <h3>Customizing the Result</h3>

                <p>We only have few lines for using the core feature that Mup offers, but we can turn it all into one line! We can make use of extension methods to combine the asynchronous methods into one so we get rid of the boilerplate code:</p>

                <pre><code>
                    <Keyword>public</Keyword> <Keyword>async</Keyword>{` Task<IActionResult> Parse([FromBody] `}<Keyword>string</Keyword>{` text, CancellationToken cancellationToken)
{
    `}<Keyword>string</Keyword>{` html = `}<Keyword>await</Keyword> <Keyword>new</Keyword>{` CreoleParser().ParseAsync(text, cancellationToken).With(`}<Keyword>new</Keyword>{` HtmlWriterVisitor());
    `}<Keyword>return</Keyword>{` Ok(html);
}`}
                </code></pre>

                <p>If we look at the base class for <Link to={Routes.documentation({ member: "mup.htmlwritervisitor", framework: this.props.match.params.framework })}>HtmlWriterVisitor</Link> we will notice that it does not extend <Link to={Routes.documentation({ member: "mup.parsetreevisitor", framework: this.props.match.params.framework })}>ParseTreeVisitor</Link> directly, it inherits <Link to={Routes.documentation({ member: "mup.parsetreevisitor<tresult>", framework: this.props.match.params.framework })}>ParseTreeVisitor&lt;TResult&gt;</Link> which in turn inherits <Link to={Routes.documentation({ member: "mup.parsetreevisitor", framework: this.props.match.params.framework })}>ParseTreeVisitor</Link>. The difference between the two base classes is that the former provides an in-memory result after visiting the entire parse tree while the latter provides a more indirect result (e.g.: writing to a file or to a network stream). The <Link to={Routes.documentation({ member: "mup.htmlwritervisitor", framework: this.props.match.params.framework })}>HtmlWriterVisitor</Link> provides a <a href={MsdnLinks["system.string"]} target="_blank">string</a> containing the <abbr title="HyperText Markup Language">HTML</abbr> corresponding to the given Creole text.</p>

                <p>Depending on what we need the visitor to do we will inherit from one or the other, the only thing that <Link to={Routes.documentation({ member: "mup.parsetreevisitor<tresult>", framework: this.props.match.params.framework })}>ParseTreeVisitor&lt;TResult&gt;</Link> adds is an abstract method for providing the result and a virtual asynchronous counterpart.</p>

                <p>If we look at the <abbr title="HyperText Markup Language">HTML</abbr> result you get from the <Link to={Routes.onlineParser()}>Online Parser</Link> we will notice that it looks a bit different than what our endpoint returns. The <Link to={Routes.documentation({ member: "mup.htmlwritervisitor", framework: this.props.match.params.framework })}>HtmlWriterVisitor</Link> that is provided with the library tries to generate the result as compact as possible, but we can change that by overriding a few methods and add a blank line so we get a more readable result. Ideally, we will get a more configurable <Link to={Routes.documentation({ member: "mup.htmlwritervisitor", framework: this.props.match.params.framework })}>HtmlWriterVisitor</Link> in a future release.</p>

                <p>The <a href={MsdnLinks["system.text.stringbuilder"]} target="_blank">StringBuilder</a> with which <abbr title="HyperText Markup Language">HTML</abbr> is being generated is exposed through the <Link to={Routes.documentation({ member: "mup.htmlwritervisitor.htmlstringbuilder", framework: this.props.match.params.framework })}>HtmlStringBuilder</Link> protected property, we can append new lines and then call the base implementation. When we need to append text we should be using <Link to={Routes.documentation({ member: "mup.htmlwritervisitor.appendhtmlsafe(system.char)", framework: this.props.match.params.framework })}>AppendHtmlSafe(char)</Link> or <Link to={Routes.documentation({ member: "mup.htmlwritervisitor.appendhtmlsafe(system.string)", framework: this.props.match.params.framework })}>AppendHtmlSafe(string)</Link> to have special characters encoded to <abbr title="HyperText Markup Language">HTML</abbr> entities.</p>

                <p>We want to generate paragraphs that are separated by blank lines with the exception of the first one. We do not want our <abbr title="HyperText Markup Language">HTML</abbr> to start with an empty line. First, we will write a custom visitor which inherits from <Link to={Routes.documentation({ member: "mup.htmlwritervisitor.htmlstringbuilder", framework: this.props.match.params.framework })}>HtmlStringBuilder</Link>, we don&#39;t need to reinvent the wheel, we&#39;ll just override the methods that need to do a little extra.</p>

                <pre><code>
                    <Keyword>public</Keyword> <Keyword>class</Keyword>{` PrettyHtmlWriterVisitor : HtmlWriterVisitor
{
    `}<Keyword>protected</Keyword> <Keyword>override</Keyword> <Keyword>void</Keyword>{` VisitParagraphBeginning()
    {
        AddBlankLineIfNecessary();
        `}<Keyword>base</Keyword>{`.VisitParagraphBeginning();
    }

    `}<Keyword>private</Keyword> <Keyword>void</Keyword>{` AddBlankLineIfNecessary()
    {
        `}<Keyword>if</Keyword>{` (HtmlStringBuilder.Length > 0)
            HtmlStringBuilder.AppendLine().AppendLine();
    }
}`}
                </code></pre>

                <p>And now to update the controller action:</p>

                <pre><code>
                    <Keyword>public</Keyword> <Keyword>async</Keyword>{` Task<IActionResult> Parse([FromBody] `}<Keyword>string</Keyword>{` text, CancellationToken cancellationToken)
{
    `}<Keyword>string</Keyword>{` html = `}<Keyword>await</Keyword> <Keyword>new</Keyword>{` CreoleParser().ParseAsync(text, cancellationToken).With(`}<Keyword>new</Keyword>{` PrettyHtmlWriterVisitor());
    `}<Keyword>return</Keyword>{` Ok(html);
}`}
                </code></pre>

                <p>That&#39;s it! Let&#39;s take this for a test run. When we call the endpoint with the following payload:</p>

                <pre><code>
                    <StringLiteral>{`"paragraph 1

paragraph 2"`}</StringLiteral>
                </code></pre>

                <p>We should be receiving the following <abbr title="HyperText Markup Language">HTML</abbr>:</p>

                <pre><code>
                    <Tag>{`<p>`}</Tag>paragraph 1<Tag>{`</p>`}</Tag>{`

`}<Tag>{`<p>`}</Tag>paragraph 2<Tag>{`</p>`}</Tag>
                </code></pre>

                <p>We can continue to do the same for more elements like tables or lists, however that goes beyond what this example proposes. Writing a custom visitor might take a bit of time, but it will work with any markup parser.</p>
            </div>
        );
    }
});

class Keyword extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <span className={Style.textPrimary}>
                {this.props.children}
            </span>
        );
    }
}

class StringLiteral extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <span className={Style.textDanger}>
                {this.props.children}
            </span>
        );
    }
}

class Comment extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <span className={Style.textMuted}>
                {this.props.children}
            </span>
        );
    }
}

class Tag extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <span className={Style.textSecondary}>
                {this.props.children}
            </span>
        );
    }
}

class AttributeName extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <span className={Style.textPrimary}>
                {this.props.children}
            </span>
        );
    }
}

class AttributeValue extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <span className={Style.textDanger}>
                {this.props.children}
            </span>
        );
    }
}