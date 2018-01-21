import React from "react";
import { Link } from "react-router-dom";

import Style from "mup/style";
import { Page } from "mup/views/layout";
import Routes from "mup/routes";

export class GettingStarted extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <h2>Getting Started</h2>

                <h3>Introduction</h3>

                <p>The library has a few core types that make it work: IParser, IParseTree and ParseTreeVisitor.</p>

                <p>Each parser (currently just Creole) implements the IParser interface which exposes a number of methods that allow parsing a string or text from a TextReader with support for asynchronous programming.</p>

                <p>The result of any parse method is ultimately an IParseTree. Surprising, or not, this interface does not expose something like a root node or anything related to what one would expect when reading &quot;tree&quot; in a type name.</p>

                <p>This is because trees can have different representations. For instance, we can have the usual example where we have the root node and the root node exposes a collection property of nodes which are the child nodes, the child nodes also have such a property for grandchild nodes and so on. A different representation is a flat one where the entire tree is stored as a list of pair elements that mark the beginning and end of each node, respectively.</p>

                <p>Regardless of how we chose to represent the parse tree, that is not what we are really after. We want the parse tree so we can traverse it in order to generate a specific output, say HTML. Here is where the ParseTreeVisitor comes into play, regardless of how the tree is represented, the IParseTree interface exposes methods that accept a ParseTreeVisitor. When the parse tree is traversed specific methods from the visitor are called.</p>

                <p>For instance, given the Creole block <code>plain //emphasized// text</code> the visitor will have the following methods invoked:</p>

                <ol>
                    <li>VisitParagraphBeginning()</li>
                    <li>VisitPlainText(&quot;plain &quot;)</li>
                    <li>VisitEmphasisBeginning()</li>
                    <li>VisitPlainText(&quot;emphasized&quot;)</li>
                    <li>VisitEmphasisEnding()</li>
                    <li>VisitPlainText(&quot; text&quot;)</li>
                    <li>VisistParagraphEnding()</li>
                </ol>

                <p>When outputting HTML we will write corresponding tags for each of the visitor methods. The VisitParagraphBeginning will write <code>&lt;p&gt;</code> while VisistParagraphEnding will write <code>&lt;/p&gt;</code>. Similar for VisitEmphasisBeginning and VisitEmphasisEnding by writing <code>&lt;em&gt;</code> and <code>&lt;/em&gt;</code>, respectively. Thus we have our output: <code>&lt;p&gt;plain &lt;em&gt;emphasized&lt;/em&gt; text&lt;/p&gt;</code>.</p>

                <p>For each desired output format (e.g.: HTML, XHTML, XML, Word Document and so on) there should be one visitor. Each visitor can be reused across parser implementations meaning that the same one can be used for the Creole parser or the WikiMedia parser since both of them actually expose an IParseTree. There is no direct relationship between an IParser and a ParseTreeVisitor.</p>

                <p>Sometimes we just want to parse a block of text and generate an HTML string out of it. However, sometimes we would like to write directly to the file system, or use the visitor to send data over a network based on what it visits. Usually when working with streams the recommended way is to use asynchronous programming in order to keep the application responsive and free up thread time while the hardware components do the actual writing.</p>

                <p>Mup is built with asynchronous programming in mind, from parsing until visiting there is support for async methods. When implementing a custom ParseTreeVisitor (the library only exposes one that outputs HTML to a string) one can override both asynchronous method and their synchronous counterpart.</p>

                <p>The pattern after each method pair is defined as follows:</p>

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

                <p>The synchronous method always gets called from its asynchronous counterpart which in turn is called when visiting the corresponding element. If you are implementing a visitor that outputs to something in-memory (you are simply writing to a StringBuilder) you can just override the synchronous methods and not bother yourself with asynchrony at all.</p>

                <p><strong>NOTE!</strong> When overriding the asynchronous method then the synchronous counterpart will not be invoked anymore unless called explicitly or calling the base implementation (e.g. <code>{`base.Visit{Element}Async(cancellationToken)`}</code> or <code>{`Visit{Element}()`}</code>).</p>

                <h3>Using the Library</h3>

                <p>To exemplify how easy it is to get starting with Mup we will build a small web application that exposes just one endpoint which parses the body of a request and returns the HTML, similar to what this site uses for the <Link to={Routes.onlineParser()}>Online Parser</Link> (if you haven&#39;t tried it, you should definitely give it a go, it&#39;s really cool).</p>

                <p>For our little web project we will be using .NET Core for convenience and because it is cross-platform, you do not really need to run Windows to go though this example. Besides, the library is cross-platform itself.</p>

                <p>Now let us finally get on with it, create a folder where you usually have your projects, I will call mine <code>mup-example</code>, go to that folder and run <code>netcore new webapi</code> in the command line. I use <a href="https://code.visualstudio.com/" target="_blank">Visual Studio Code</a> which has an integrated terminal so I can do my code editing and use the command line from one place.</p>

                <p>Now that we have our demo project created, we will start by adding a <a href="https://www.nuget.org/" target="_blank">NuGet</a> dependency towards the <a href="https://www.nuget.org/packages/Mup" target="_blank">Mup package</a>. Open <em>mup-example.csproj</em> (assuming you have the same folder name as I do, otherwise the <em>.csproj</em> file will have the same name as the folder in which you created your .NET Core application) and add the following line: <code>&lt;PackageReference Include=&quot;Mup&quot; Version=&quot;1.0.0&quot; /&gt;</code>. The <em>.csproj</em> file should look something like this:</p>

                <pre><code>
                    <Tag>{`<Project`}</Tag> <AttributeName>Sdk</AttributeName>=<AttributeValue>"Microsoft.NET.Sdk.Web"</AttributeValue><Tag>{`>`}</Tag>{`

  `}<Tag>{`<PropertyGroup>`}</Tag>{`
    `}<Tag>{`<TargetFramework>`}</Tag>netcoreapp1.1<Tag>{`</TargetFramework>`}</Tag>{`
    `}<Tag>{`<PackageTargetFallback>`}</Tag>$(PackageTargetFallback);portable-net45+win8+wp8+wpa81;<Tag>{`</PackageTargetFallback>`}</Tag>{`
  `}<Tag>{`</PropertyGroup>`}</Tag>{`

  `}<Tag>{`<ItemGroup>`}</Tag>{`
    `}<Tag>{`<Folder`}</Tag> <AttributeName>Include</AttributeName>=<AttributeValue>"wwwroot\"</AttributeValue> <Tag>{`/>`}</Tag>{`
  `}<Tag>{`</ItemGroup>`}</Tag>{`

  `}<Tag>{`<ItemGroup>`}</Tag>{`
    `}<Tag>{`<PackageReference`}</Tag> <AttributeName>Include</AttributeName>=<AttributeValue>"Microsoft.AspNetCore"</AttributeValue> <AttributeName>Version</AttributeName>=<AttributeValue>"1.1.2"</AttributeValue> <Tag>{`/>`}</Tag>{`
    `}<Tag>{`<PackageReference`}</Tag> <AttributeName>Include</AttributeName>=<AttributeValue>"Microsoft.AspNetCore.Mvc"</AttributeValue> <AttributeName>Version</AttributeName>=<AttributeValue>"1.1.3"</AttributeValue> <Tag>{`/>`}</Tag>{`
    `}<Tag>{`<PackageReference`}</Tag> <AttributeName>Include</AttributeName>=<AttributeValue>"Microsoft.Extensions.Logging.Debug"</AttributeValue> <AttributeName>Version</AttributeName>=<AttributeValue>"1.1.2"</AttributeValue> <Tag>{`/>`}</Tag>{`
    `}<strong><Tag>{`<PackageReference`}</Tag> <AttributeName>Include</AttributeName>=<AttributeValue>"Mup"</AttributeValue> <AttributeName>Version</AttributeName>=<AttributeValue>"1.0.0"</AttributeValue> <Tag>{`/>`}</Tag></strong>{`
  `}<Tag>{`</ItemGroup>`}</Tag>{`

  `}<Tag>{`<ItemGroup>`}</Tag>{`
    `}<Tag>{`<DotNetCliToolReference`}</Tag> <AttributeName>Include</AttributeName>=<AttributeValue>"Microsoft.VisualStudio.Web.CodeGeneration.Tools"</AttributeValue> <AttributeName>Version</AttributeName>=<AttributeValue>"1.0.1"</AttributeValue> <Tag>{`/>`}</Tag>{`
  `}<Tag>{`</ItemGroup>`}</Tag>{`

`}<Tag>{`</Project>`}</Tag>
                </code></pre>

                <p>Next we will create our controller that will be using Mup to parse Creole and return HTML. Under <em>Controllers</em> create a new file: <em>CreoleController.cs</em>, the controller will have just one method for handing <strong>POST</strong> requests on the <strong>api/creole</strong> endpoint:</p>

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

                <p>Time to test this, run the application and open a Web API testing tool so we can make some requests! I use <a href="https://www.getpostman.com/" target="_blank">Postman</a> for this as I can create collections and keep my requests organized and synced across devices (or just use Cloud storage for backup, fewer things to worry about).</p>

                <p>Usually the .NET Core App will run on port 5000 locally, when you start the application in debug mode it will automatically open your favourite browser and in the address bar you can grab the base URL for your request. For me it is: <code>http://localhost:5000/</code>. If a 404 error shows when you start the application it is alright, the application only exposes HTTP endpoints and no static content (like <em>index.html</em>).</p>

                <p>Now to make our request, append to the base URL the route we have defined for parsing Creole text: <code>http://localhost:5000/api/creole</code>, keep in mind that it is a <strong>POST</strong> request and in the body add the following:</p>

                <pre><code>
                    <StringLiteral>{`"== Hey There!

This is a test"`}</StringLiteral>
                </code></pre>

                <p>When you call the API you should get <code>&lt;h2&gt;Hey There!&lt;/h2&gt;&lt;p&gt;This is a test&lt;/p&gt;</code>.</p>

                <h3>Customizing the Result</h3>

                <p>As you can see, we have quite a few lines for using the Core feature that Mup offers, fear not as we can turn it into a one line! We can make use of extension methods to combine the asynchronous methods into one so we get rid of the boilerplate code:</p>

                <pre><code>
                    <Keyword>public</Keyword> <Keyword>async</Keyword>{` Task<IActionResult> Parse([FromBody] `}<Keyword>string</Keyword>{` text, CancellationToken cancellationToken)
{
    `}<Keyword>string</Keyword>{` html = `}<Keyword>await</Keyword> <Keyword>new</Keyword>{` CreoleParser().ParseAsync(text, cancellationToken).With(`}<Keyword>new</Keyword>{` HtmlWriterVisitor());
    `}<Keyword>return</Keyword>{` Ok(html);
}`}
                </code></pre>

                <p>If you look at the base class for <code>HtmlWriterVisitor</code> you will notice that it does not extend <code>ParseTreeVisitor</code> directly, it inherits <code>ParseTreeVisitor&lt;TResult&gt;</code> which in turn inherits <code>ParseTreeVisitor</code>. The difference between the two visitor base classes is that the former provides an in-memory result after visiting the entire parse tree while the latter provides a more indirect result (e.g.: writing to a file or to a network stream). The <code>HtmlWriterVisitor</code> provides a <a href="https://msdn.microsoft.com/en-us/library/system.string.aspx" target="_blank">string</a> containing the HTML corresponding to the given Creole text.</p>

                <p>Depending on your needs you will inherit from one or the other, the only thing that <code>ParseTreeVisitor&lt;TResult&gt;</code> adds is an abstract method for providing the result and a virtual asynchronous counterpart.</p>

                <p>If you look at the HTML result you get from the <Link to={Routes.onlineParser()}>Online Parser</Link> you will notice that it looks a bit different than what our endpoint returns. The HtmlWriterVisitor that is provided with the library tries to generate the result as compact as possible, but we can change that by overriding a few methods and add a blank line so we get a more readable result.</p>

                <p>The <a href="https://msdn.microsoft.com/en-us/library/system.text.stringbuilder.aspx" target="_blank">StringBuilder</a> with which HTML is being generated is exposed through the <code>HtmlStringBuilder</code> protected property. We can append new lines and then call the base implementation. If you need to append text you should be using one of the <code>AppendHtmlSafe</code> overloads as special characters are encoded into HTML entities.</p>

                <p>We want to generate paragraphs that are separated by blank lines with the exception of the first one. We do not want an empty line before the first paragraph in our HTML. First we will write a custom visitor which inherits from <code>HtmlWriterVisitor</code>, we don&#39;t need to reinvent the wheel, we&#39;ll just override the methods that we want to change.</p>

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

                <p>That&#39;s it! Let&#39;s take this for a test run. Update the body for the request to the following:</p>

                <pre><code>
                    <StringLiteral>{`"paragraph 1

paragraph 2"`}</StringLiteral>
                </code></pre>

                <p>When we call the endpoint we will be receiving the following HTML:</p>

                <pre><code>
                    <Tag>{`<p>`}</Tag>paragraph 1<Tag>{`</p>`}</Tag>{`

`}<Tag>{`<p>`}</Tag>paragraph 2<Tag>{`</p>`}</Tag>
                </code></pre>
            </div>
        );
    }
};

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