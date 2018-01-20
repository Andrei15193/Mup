import React from "react";
import { Link } from "react-router-dom";

import { Page } from "mup/views/layout";
import Routes from "mup/routes";
import Style from "mup/style";
import { Documentation, FriendlyNames, LinkTo } from "./documentation/documentation";
import { definitions as MupDefinitions } from "./documentation/mup";
import { GettingStarted } from "./documentation/getting-started";

export default class extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const documentationComponent = getDocumentationComponentFor(this.props.match.params.member);
        return (
            <Page >
                <h1>Documentation</h1>
                {documentationComponent}
            </Page>
        );
    }
};

function getDocumentationComponentFor(member) {
    const memberId = (member && member.toLocaleLowerCase());
    if (memberId in MupDefinitions) {
        const definition = MupDefinitions[memberId];
        switch (definition.type) {
            case "interface":
                return (
                    <Interface definition={definition} />
                );

            case "class":
                return (
                    <Class definition={definition} />
                );

            case "constructor":
            case "method":
                return (
                    <Method definition={definition} />
                );

            case "property":
                return (
                    <Property definition={definition} />
                );
        }
    }

    return (
        <MupNamespace />
    );
}

class MupNamespace extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const mupNamespace = MupDefinitions.mup;
        const types = mupNamespace.enums.sort()
            .concat(mupNamespace.delegates.sort())
            .concat(mupNamespace.interfaces.sort())
            .concat(mupNamespace.classes.sort())
            .concat(mupNamespace.structs.sort());
        return (
            <div>
                <nav aria-label="breadcrumb" role="navigation">
                    <ol className={Style.breadcrumb}>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">Mup</li>
                    </ol>
                </nav>
                <h2>Mup Namespace</h2>
                <table className={[Style.table, Style.tableHover].join(" ")}>
                    <thead>
                        <tr>
                            <th>Type</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        {types.map(typeId =>
                            <tr key={typeId}>
                                <td>
                                    <Link to={Routes.documentation({ member: typeId })}>
                                        <MemberName definition={MupDefinitions[typeId]} />
                                    </Link>
                                </td>
                                <td className={Style.documentationTable}>
                                    <Documentation elements={MupDefinitions[typeId].documentation.summary} />
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <GettingStarted />
            </div>
        );
    }
}

class Interface extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;
        return (
            <div>
                <nav aria-label="breadcrumb" role="navigation">
                    <ol className={Style.breadcrumb}>
                        <li className={Style.breadcrumbItem} aria-current="page">
                            <Link to={Routes.documentation({ member: definition.namespaceId })}>{definition.namespace}</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">
                            <MemberName definition={definition} />
                        </li>
                    </ol>
                </nav>
                <h2><MemberName definition={definition} /> Interface</h2>
                <Documentation elements={definition.documentation.summary} />
                <TypeInheritanceSummary definition={definition} />
                <PropertiesList properties={definition.properties} />
                <MethodsList methods={definition.methods} />
            </div>
        );
    }
}

class Class extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;
        return (
            <div>
                <nav aria-label="breadcrumb" role="navigation">
                    <ol className={Style.breadcrumb}>
                        <li className={Style.breadcrumbItem} aria-current="page">
                            <Link to={Routes.documentation({ member: definition.namespaceId })}>{definition.namespace}</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">
                            <MemberName definition={definition} />
                        </li>
                    </ol>
                </nav>
                <h2><MemberName definition={definition} /> Class</h2>
                <Documentation elements={definition.documentation.summary} />
                <TypeInheritanceSummary definition={definition} />
                <GenericParametersSummary definition={definition} />
                <ConstructorsList includeAccessModifier constructors={definition.constructors} />
                <PropertiesList includeAccessModifier properties={definition.properties} />
                <MethodsList includeAccessModifier methods={definition.methods} />
            </div>
        );
    }
}

class Method extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;
        const declaringTypeDefinition = MupDefinitions[definition.declaringType.id]
        return (
            <div>
                <nav aria-label="breadcrumb" role="navigation">
                    <ol className={Style.breadcrumb}>
                        <li className={Style.breadcrumbItem} aria-current="page">
                            <Link to={Routes.documentation({ member: declaringTypeDefinition.namespaceId })}>{declaringTypeDefinition.namespace}</Link>
                        </li>
                        <li className={Style.breadcrumbItem} aria-current="page">
                            <Link to={Routes.documentation({ member: declaringTypeDefinition.id })}>
                                <MemberName definition={declaringTypeDefinition} />
                            </Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">
                            <MemberName definition={definition} />
                        </li>
                    </ol>
                </nav>
                <h2><MemberName definition={definition} /> Method</h2>
                <Documentation elements={definition.documentation.summary} />
                <MethodInheritanceSummary definition={definition} />
                <GenericParametersSummary definition={definition} />
                <ParametersSummary definition={definition} />
                <ReturnsSummary definition={definition} />
                <ExceptionsList definition={definition} />
            </div>
        );
    }
}

class Property extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;
        const declaringTypeDefinition = MupDefinitions[definition.declaringType.id]
        return (
            <div>
                <nav aria-label="breadcrumb" role="navigation">
                    <ol className={Style.breadcrumb}>
                        <li className={Style.breadcrumbItem} aria-current="page">
                            <Link to={Routes.documentation({ member: declaringTypeDefinition.namespaceId })}>{declaringTypeDefinition.namespace}</Link>
                        </li>
                        <li className={Style.breadcrumbItem} aria-current="page">
                            <Link to={Routes.documentation({ member: declaringTypeDefinition.id })}>
                                <MemberName definition={declaringTypeDefinition} />
                            </Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">
                            <MemberName definition={definition} />
                        </li>
                    </ol>
                </nav>
                <h2><MemberName definition={definition} /> Property</h2>
                <Documentation elements={definition.documentation.summary} />
                <PropertyInheritanceSummary definition={definition} />
                <PropertyAccessorSummary definition={definition} />
                <ParametersSummary definition={definition} />
                <ExceptionsList definition={definition} />
            </div>
        );
    }
}

class MemberName extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        let name;
        const definition = this.props.definition;
        if (definition.genericParameters && definition.genericParameters.length > 0)
            name = (
                definition.name
                + "<"
                + definition
                    .genericParameters
                    .map(genericParameter => genericParameter.name)
                    .join(", ")
                + ">");
        else
            name = definition.name

        if (definition.type == "method" || definition.type == "constructor")
            if (definition.parameters && definition.parameters.length > 0)
                name += (
                    "(" +
                    definition.parameters.map(
                        parameter => getTypeName(parameter.type)
                    ).join(", ") +
                    ")"
                );
            else
                name += "()";

        return name;
    }
}

function getTypeName(typeReference) {
    if (typeReference.id in FriendlyNames)
        return FriendlyNames[typeReference.id];

    let typeName = typeReference.name;
    if (typeReference.genericArguments.length > 0)
        typeName += "<" + typeReference.genericArguments.map(genericArgument => {
            if (typeof (genericArgument.value) === "string")
                return genericArgument.value;
            else
                return getTypeName(genericArgument.value);
        }).join(", ") + ">";
    return typeName;
}

class ConstructorsList extends React.PureComponent {
    static get defaultProps() {
        return {
            includeAccessModifier: false
        };
    }

    constructor(props) {
        super(props);
    }

    render() {
        const constructors = this.props.constructors;
        if (constructors && constructors.length > 0)
            return (
                <div>
                    <h3>Constructors</h3>
                    <table className={[Style.table, Style.tableHover].join(" ")}>
                        <thead>
                            <tr>
                                <th>Name</th>
                                {this.props.includeAccessModifier ? <th>Access Modifier</th> : null}
                                <th>Summary</th>
                            </tr>
                        </thead>
                        <tbody>
                            {constructors.map(constructorId => {
                                const constructorDefinition = MupDefinitions[constructorId];
                                return (
                                    <tr key={constructorId}>
                                        <td>
                                            <Link to={Routes.documentation({ member: constructorId })}>
                                                <MemberName definition={constructorDefinition} />
                                            </Link>
                                        </td>
                                        {this.props.includeAccessModifier ? <td><AccessModifier access={constructorDefinition.access} /></td> : null}
                                        <td className={Style.documentationTable}>
                                            <Documentation elements={constructorDefinition.documentation.summary} />
                                        </td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </div>
            );
        else
            return null;
    }
}

class PropertiesList extends React.PureComponent {
    static get defaultProps() {
        return {
            includeAccessModifier: false
        };
    }

    constructor(props) {
        super(props);
    }

    render() {
        const properties = this.props.properties;
        const includeAccessModifier = this.props.includeAccessModifier;
        if (properties && properties.length > 0)
            return (
                <div>
                    <h3>Properties</h3>
                    <table className={[Style.table, Style.tableHover].join(" ")}>
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Accessors</th>
                                <th>Summary</th>
                            </tr>
                        </thead>
                        <tbody>
                            {properties.map(propertyId => {
                                const propertyDefinition = MupDefinitions[propertyId];
                                return (
                                    <tr key={propertyId}>
                                        <td>
                                            <Link to={Routes.documentation({ member: propertyId })}>
                                                <MemberName definition={propertyDefinition} />
                                            </Link>
                                        </td>
                                        <td>
                                            <PropertyAccessor includeAccessModifier={includeAccessModifier} definition={propertyDefinition} />
                                        </td>
                                        <td className={Style.documentationTable}>
                                            <Documentation elements={propertyDefinition.documentation.summary} />
                                        </td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </div>
            );
        else
            return null;
    }
}

class PropertyAccessor extends React.PureComponent {
    static get defaultProps() {
        return {
            includeAccessModifier: false
        };
    }

    constructor(props) {
        super(props);
    }

    render() {
        const propertyDefinition = this.props.definition;
        if (this.props.includeAccessModifier)
            if (propertyDefinition.get && propertyDefinition.set)
                return [
                    <AccessModifier key="1" access={propertyDefinition.get.access} />,
                    " get; ",
                    <AccessModifier key="2" access={propertyDefinition.set.access} />,
                    " set"
                ];
            else if (propertyDefinition.get)
                return [
                    <AccessModifier key="1" access={propertyDefinition.get.access} />,
                    " get"
                ];
            else
                return [
                    <AccessModifier key="1" access={propertyDefinition.set.access} />,
                    " set"
                ];
        else
            if (propertyDefinition.get && propertyDefinition.set)
                return "get; set";
            else if (propertyDefinition.get)
                return "get";
            else
                return "set";
    }
}

class MethodsList extends React.PureComponent {
    static get defaultProps() {
        return {
            includeAccessModifier: false
        };
    }

    constructor(props) {
        super(props);
    }

    render() {
        const methods = this.props.methods;
        if (methods && methods.length > 0)
            return (
                <div>
                    <h3>Methods</h3>
                    <table className={[Style.table, Style.tableHover].join(" ")}>
                        <thead>
                            <tr>
                                <th>Name</th>
                                {this.props.includeAccessModifier ? <th>Access Modifier</th> : null}
                                <th>Summary</th>
                            </tr>
                        </thead>
                        <tbody>
                            {methods.map(methodId => {
                                const methodDefinition = MupDefinitions[methodId];
                                return (
                                    <tr key={methodId}>
                                        <td>
                                            <Link to={Routes.documentation({ member: methodId })}>
                                                <MemberName definition={methodDefinition} />
                                            </Link>
                                        </td>
                                        {this.props.includeAccessModifier ? <td><AccessModifier access={methodDefinition.access} /></td> : null}
                                        <td className={Style.documentationTable}>
                                            <Documentation elements={methodDefinition.documentation.summary} />
                                        </td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </div>
            );
        else
            return null;
    }
}

class AccessModifier extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        switch (this.props.access) {
            case "public":
                return "public";

            case "protected":
            case "internal protected":
                return "protected";

            case "internal":
                return "internal";

            case "private":
                return "private";

            default:
                console.warn("Unknown access modifier '" + this.props.access + "'.");
        }
    }
}

class TypeInheritanceSummary extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;
        const paragraphs = [];
        if (definition.base && definition.interfaces.length > 0) {
            let isFirst = true;
            paragraphs.push(
                <p key="inheritance">
                    Extends <LinkTo reference={definition.base} />.
                    Implements {definition.interfaces.map(
                        (interfaceReference, interfaceReferenceIndex) => {
                            if (isFirst) {
                                isFirst = false;
                                return (
                                    <LinkTo key={interfaceReferenceIndex} reference={interfaceReference} />
                                );
                            }
                            else
                                return [
                                    ", ",
                                    (
                                        <LinkTo key={interfaceReferenceIndex} reference={interfaceReference} />
                                    )
                                ];
                        })}.
                </p>
            );
        }
        else if (definition.base)
            paragraphs.push(
                <p key="inheritance">
                    Extends <LinkTo reference={definition.base} />.
                </p>
            );
        else if (definition.interfaces.length > 0) {
            let isFirst = true;
            paragraphs.push(
                <p key="inheritance">
                    Extends {definition.interfaces.map(
                        (interfaceReference, interfaceReferenceIndex) => {
                            if (isFirst) {
                                isFirst = false;
                                return (
                                    <LinkTo key={interfaceReferenceIndex} reference={interfaceReference} />
                                );
                            }
                            else
                                return [
                                    ", ",
                                    (
                                        <LinkTo key={interfaceReferenceIndex} reference={interfaceReference} />
                                    )
                                ];
                        })}.
                </p>
            );
        }

        if (definition.isStatic)
            paragraphs.push(
                <p key="modifier">
                    This is a <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/static" target="_blank">static</a> class.
                </p>
            );
        else if (definition.isAbstract)
            paragraphs.push(
                <p key="modifier">
                    This is an <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/abstract" target="_blank">abstract</a> class.
                </p>
            );
        else if (definition.isSealed)
            paragraphs.push(
                <p key="modifier">
                    This is a <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/sealed" target="_blank">sealed</a> class.
                </p>
            );

        if (paragraphs.length > 0)
            return paragraphs;
        else
            return null;
    }
}

class GenericParametersSummary extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;
        if (definition.genericParameters && definition.genericParameters.length > 0)
            return (
                <div>
                    <h3>Generic Parameters</h3>
                    <ul>
                        {definition.genericParameters.map(
                            genericParameter => (
                                <li key={genericParameter.name}>
                                    <strong>{genericParameter.name}</strong>
                                    <Documentation elements={definition.documentation.genericParameters[genericParameter.name]} />
                                    <GenericParameterConstraints definition={genericParameter} />
                                </li>
                            )
                        )}
                    </ul>
                </div>
            );
        else
            return null;
    }
}

class GenericParameterConstraints extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        let varianceDescription;
        const definition = this.props.definition;
        switch (definition.variance) {
            case "covariant":
                varianceDescription = "covariant";
                break;

            case "contravariant":
                varianceDescription = "contravariant";
                break;
        }

        const constraintDescriptions = [];
        switch (definition.typeKind) {
            case "reference":
                constraintDescriptions.push(" a reference type");
                break;

            case "value":
                constraintDescriptions.push(" a value type");
                break;
        }
        if (definition.base)
            constraintDescriptions.push(
                " extend ",
                <LinkTo key="base" reference={definition.base} />
            );
        if (definition.interfaces.length > 0) {
            constraintDescriptions.push(" implement ");
            definition.interfaces.forEach(interfaceReference => {
                constraintDescriptions.push(
                    <LinkTo key={interfaceReference.id} reference={interfaceReference} />
                );
            });
        }
        if (definition.defaultConstructor)
            constraintDescriptions.push(" have a default constructor.");

        const descriptions = [];
        if (varianceDescription)
            descriptions.push(
                "Generic arguments are ",
                varianceDescription
            );

        if (constraintDescriptions.length > 0) {
            if (varianceDescription)
                descriptions.push(
                    (constraintDescriptions.length === 1 ? " and " : ", "),
                    "must ");
            else
                descriptions.push("Generic arguments must ");

            constraintDescriptions.forEach((constraintDescription, index) => {
                descriptions.push(
                    (index === 0 ? "" : (index === (constraintDescriptions.length - 1) ? " and " : ", ")),
                    constraintDescription
                );
            });
        }
        if (descriptions.length === 0)
            return null;

        descriptions.push(".");
        return descriptions;
    }
}

class MethodInheritanceSummary extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;

        if (definition.type === "method") {
            if (definition.isStatic && definition.isExtension)
                return (
                    <p>This is an <a href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods" target="_blank">extension method</a>.</p>
                );

            if (definition.isStatic)
                return (
                    <p>This is a <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/static" target="_blank">static</a> method.</p>
                );

            if (definition.isAbstract)
                return (
                    <p>This is an <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/abstract" target="_blank">abstract</a> method.</p>
                );

            if (definition.isOverride && definition.isSealed)
                return (
                    <p>This is a <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/sealed" target="_blank">overridden</a> <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/override" target="_blank">overridden</a> method.</p>
                );

            if (definition.isOverride)
                return (
                    <p>This is an <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/override" target="_blank">overridden</a> method.</p>
                );

            if (definition.isVirtual)
                return (
                    <p>
                        This is a <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/virtual" target="_blank">virtual</a> method.
                    </p>
                );
        }

        return null;
    }
}

class PropertyInheritanceSummary extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;

        if (definition.type === "property") {
            if (definition.isStatic)
                return (
                    <p>This is a <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/static" target="_blank">static</a> property.</p>
                );

            if (definition.isAbstract)
                return (
                    <p>This is an <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/abstract" target="_blank">abstract</a> property.</p>
                );

            if (definition.isOverride && definition.isSealed)
                return (
                    <p>This is a <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/sealed" target="_blank">overridden</a> <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/override" target="_blank">overridden</a> property.</p>
                );

            if (definition.isOverride)
                return (
                    <p>This is an <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/override" target="_blank">overridden</a> property.</p>
                );

            if (definition.isVirtual)
                return (
                    <p>
                        This is a <a href="https://docs.microsoft.com/en-gb/dotnet/csharp/language-reference/keywords/virtual" target="_blank">virtual</a> property.
                    </p>
                );
        }

        return null;
    }
}

class PropertyAccessorSummary extends React.PureComponent{    
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;

        if ((definition.get || {}).access !== "private" && (definition.set || {}).access !== "private")
            return (
                <p>
                    This property exposes a <AccessModifier access={definition.get.access} /> getter and setter.
                </p>
            );
        else if ((definition.get || {}).access !== "private")
            return (
                <p>
                    This property exposes a <AccessModifier access={definition.get.access} /> getter.
                </p>
            );
        else
            return (
                <p>
                    This property exposes a <AccessModifier access={definition.set.access} /> setter.
                </p>
            );
    }
}

class ParametersSummary extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;
        if (definition.parameters && definition.parameters.length > 0)
            return (
                <div>
                    <h3>Parameters</h3>
                    <ul>
                        {definition.parameters.map(
                            parameter => (
                                <li key={parameter.name}>
                                    <div>
                                        <strong>{parameter.name}</strong> <LinkTo reference={parameter.type} />
                                    </div>
                                    <Documentation elements={definition.documentation.parameters[parameter.name]} />
                                </li>
                            )
                        )}
                    </ul>
                </div>
            );
        else
            return null;
    }
}

class ReturnsSummary extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;
        if (definition.documentation.returns)
            return (
                <div>
                    <h3>
                        Returns
                        {" "}
                        <LinkTo reference={definition.return.type} />
                    </h3>
                    <Documentation elements={definition.documentation.returns} />
                </div>
            );
        else
            return null;
    }
}

class ExceptionsList extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;
        const exceptions = definition.documentation.exceptions;
        if (exceptions && exceptions.length > 0)
            return (
                <div>
                    <h3>
                        Exceptions
                    </h3>
                    <ul>
                        {exceptions.map(
                            exception => (
                                <li key={exception.type.id}>
                                    <strong><LinkTo reference={exception.type} /></strong>
                                    <Documentation elements={exception.description} />
                                </li>
                            )
                        )}
                    </ul>
                </div>
            );
        else
            return null;
    }
}