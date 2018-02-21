import React from "react";
import { Link } from "react-router-dom";

import Style from "mup/style";
import { Page } from "./layout";
import { Routes } from "../routes";
import { DocumentationView, FriendlyNames, LinkTo } from "./documentation/documentation-view";
import { definitions as MupDefinitions } from "./documentation/mup.json";
import { GettingStarted } from "./documentation/getting-started";

export class Documentation extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const documentationComponent = getDocumentationComponentFor(this.props.match.params.member, this.props.match.params.framework);
        return (
            <Page>
                <h1>Documentation</h1>
                {documentationComponent}
            </Page>
        );
    }
};

function getDocumentationComponentFor(member, selectedFramework) {
    const memberId = (member && member.toLocaleLowerCase());
    if (memberId in MupDefinitions) {
        const definition = MupDefinitions[memberId];
        switch (definition.type) {
            case "interface":
                return (
                    <Interface selectedFramework={selectedFramework} definition={definition} />
                );

            case "class":
                return (
                    <Class selectedFramework={selectedFramework} definition={definition} />
                );

            case "constructor":
            case "method":
                return (
                    <Method selectedFramework={selectedFramework} definition={definition} />
                );

            case "property":
                return (
                    <Property selectedFramework={selectedFramework} definition={definition} />
                );
        }
    }

    return (
        <MupNamespace selectedFramework={selectedFramework} />
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
                <AvailableFrameworks memberId="mup" selected={this.props.selectedFramework}>{mupNamespace.availableFrameworks}</AvailableFrameworks>
                <table className={[Style.table, Style.tableHover].join(" ")}>
                    <thead>
                        <tr>
                            <th>Type</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>
                        {types
                            .filter(typeId => (!this.props.selectedFramework || MupDefinitions[typeId].availableFrameworks.includes(this.props.selectedFramework)))
                            .map(
                                typeId => (
                                    <tr key={typeId}>
                                        <td>
                                            <Link to={Routes.documentation({ member: typeId, framework: this.props.selectedFramework })}>
                                                <MemberName definition={MupDefinitions[typeId]} />
                                            </Link>
                                        </td>
                                        <td className={Style.documentationTable}>
                                            <DocumentationView elements={MupDefinitions[typeId].documentation.summary} selectedFramework={this.props.selectedFramework} />
                                        </td>
                                    </tr>
                                )
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
                            <Link to={Routes.documentation({ member: definition.namespaceId, framework: this.props.selectedFramework })}>{definition.namespace}</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">
                            <MemberName definition={definition} />
                        </li>
                    </ol>
                </nav>
                <h2><MemberName definition={definition} /> Interface</h2>
                <DocumentationView elements={definition.documentation.summary} selectedFramework={this.props.selectedFramework} />
                <TypeInheritanceSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <AvailableFrameworks memberId={definition.id} selected={this.props.selectedFramework}>{definition.availableFrameworks}</AvailableFrameworks>
                <PropertiesList selectedFramework={this.props.selectedFramework} properties={definition.properties} />
                <MethodsList selectedFramework={this.props.selectedFramework} methods={definition.methods} />
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
                            <Link to={Routes.documentation({ member: definition.namespaceId, framework: this.props.selectedFramework })}>{definition.namespace}</Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">
                            <MemberName definition={definition} />
                        </li>
                    </ol>
                </nav>
                <h2><MemberName definition={definition} /> Class</h2>
                <DocumentationView elements={definition.documentation.summary} selectedFramework={this.props.selectedFramework} />
                <TypeInheritanceSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <AvailableFrameworks memberId={definition.id} selected={this.props.selectedFramework}>{definition.availableFrameworks}</AvailableFrameworks>
                <GenericParametersSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <ConstructorsList includeAccessModifier selectedFramework={this.props.selectedFramework} constructors={definition.constructors} />
                <PropertiesList includeAccessModifier selectedFramework={this.props.selectedFramework} properties={definition.properties} />
                <MethodsList includeAccessModifier selectedFramework={this.props.selectedFramework} methods={definition.methods} />
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
                            <Link to={Routes.documentation({ member: declaringTypeDefinition.namespaceId, framework: this.props.selectedFramework })}>{declaringTypeDefinition.namespace}</Link>
                        </li>
                        <li className={Style.breadcrumbItem} aria-current="page">
                            <Link to={Routes.documentation({ member: declaringTypeDefinition.id, framework: this.props.selectedFramework })}>
                                <MemberName definition={declaringTypeDefinition} />
                            </Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">
                            <MemberName definition={definition} />
                        </li>
                    </ol>
                </nav>
                <h2><MemberName definition={definition} /> {definition.type === "constructor" ? "Constructor" : "Method"}</h2>
                <DocumentationView elements={definition.documentation.summary} selectedFramework={this.props.selectedFramework} />
                <MethodInheritanceSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <AvailableFrameworks memberId={definition.id} selected={this.props.selectedFramework}>{definition.availableFrameworks}</AvailableFrameworks>
                <GenericParametersSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <ParametersSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <ReturnsSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <ExceptionsList definition={definition} selectedFramework={this.props.selectedFramework} />
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
                            <Link to={Routes.documentation({ member: declaringTypeDefinition.namespaceId, framework: this.props.selectedFramework })}>{declaringTypeDefinition.namespace}</Link>
                        </li>
                        <li className={Style.breadcrumbItem} aria-current="page">
                            <Link to={Routes.documentation({ member: declaringTypeDefinition.id, framework: this.props.selectedFramework })}>
                                <MemberName definition={declaringTypeDefinition} />
                            </Link>
                        </li>
                        <li className={[Style.breadcrumbItem, Style.active].join(" ")} aria-current="page">
                            <MemberName definition={definition} />
                        </li>
                    </ol>
                </nav>
                <h2><MemberName definition={definition} /> Property (<LinkTo reference={definition.propertyType} selectedFramework={this.props.selectedFramework} />)</h2>
                <DocumentationView elements={definition.documentation.summary} selectedFramework={this.props.selectedFramework} />
                <PropertyInheritanceSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <PropertyAccessorSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <AvailableFrameworks memberId={definition.id} selected={this.props.selectedFramework}>{definition.availableFrameworks}</AvailableFrameworks>
                <ParametersSummary definition={definition} selectedFramework={this.props.selectedFramework} />
                <ExceptionsList definition={definition} selectedFramework={this.props.selectedFramework} />
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
                            {constructors
                                .map(constructorId => MupDefinitions[constructorId])
                                .filter(constructorDefinition => (!this.props.selectedFramework || constructorDefinition.availableFrameworks.includes(this.props.selectedFramework)))
                                .sort(methodComparer)
                                .map(
                                    constructorDefinition => (
                                        <tr key={constructorDefinition.id}>
                                            <td>
                                                <Link to={Routes.documentation({ member: constructorDefinition.id, framework: this.props.selectedFramework })}>
                                                    <MemberName definition={constructorDefinition} />
                                                </Link>
                                            </td>
                                            {this.props.includeAccessModifier ? <td><AccessModifier access={constructorDefinition.access} /></td> : null}
                                            <td className={Style.documentationTable}>
                                                <DocumentationView elements={constructorDefinition.documentation.summary} selectedFramework={this.props.selectedFramework} />
                                            </td>
                                        </tr>
                                    )
                                )}
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
                            {properties
                                .map(propertyId => MupDefinitions[propertyId])
                                .filter(propertyDefinition => (!this.props.selectedFramework || propertyDefinition.availableFrameworks.includes(this.props.selectedFramework)))
                                .sort((left, right) => left.name < right.name ? -1 : left.name > right.name ? 1 : 0)
                                .map(
                                    propertyDefinition => (
                                        <tr key={propertyDefinition.id}>
                                            <td>
                                                <Link to={Routes.documentation({ member: propertyDefinition.id, framework: this.props.selectedFramework })}>
                                                    <MemberName definition={propertyDefinition} />
                                                </Link>
                                            </td>
                                            <td>
                                                <PropertyAccessor includeAccessModifier={includeAccessModifier} definition={propertyDefinition} />
                                            </td>
                                            <td className={Style.documentationTable}>
                                                <DocumentationView elements={propertyDefinition.documentation.summary} selectedFramework={this.props.selectedFramework} />
                                            </td>
                                        </tr>
                                    )
                                )}
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
            if (propertyDefinition.get && propertyDefinition.get.access !== "private" && propertyDefinition.set && propertyDefinition.set.access !== "private")
                return [
                    <AccessModifier key="getter" access={propertyDefinition.get.access} />,
                    " get; ",
                    <AccessModifier key="setter" access={propertyDefinition.set.access} />,
                    " set"
                ];
            else if (propertyDefinition.get && propertyDefinition.get.access !== "private")
                return [
                    <AccessModifier key="getter" access={propertyDefinition.get.access} />,
                    " get"
                ];
            else
                return [
                    <AccessModifier key="setter" access={propertyDefinition.set.access} />,
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
                            {methods
                                .map(methodId => MupDefinitions[methodId])
                                .filter(methodDefinition => (!this.props.selectedFramework || methodDefinition.availableFrameworks.includes(this.props.selectedFramework)))
                                .sort(methodComparer)
                                .map(
                                    methodDefinition => (
                                        <tr key={methodDefinition.id}>
                                            <td>
                                                <Link to={Routes.documentation({ member: methodDefinition.id, framework: this.props.selectedFramework })}>
                                                    <MemberName definition={methodDefinition} />
                                                </Link>
                                            </td>
                                            {this.props.includeAccessModifier ? <td><AccessModifier access={methodDefinition.access} /></td> : null}
                                            <td className={Style.documentationTable}>
                                                <DocumentationView elements={methodDefinition.documentation.summary} selectedFramework={this.props.selectedFramework} />
                                            </td>
                                        </tr>
                                    )
                                )}
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
                    Extends <LinkTo reference={definition.base} selectedFramework={this.props.selectedFramework} />.
                    Implements {definition.interfaces.map(
                        (interfaceReference, interfaceReferenceIndex) => {
                            if (isFirst) {
                                isFirst = false;
                                return (
                                    <LinkTo key={interfaceReferenceIndex} reference={interfaceReference} selectedFramework={this.props.selectedFramework} />
                                );
                            }
                            else
                                return [
                                    ", ",
                                    (
                                        <LinkTo key={interfaceReferenceIndex} reference={interfaceReference} selectedFramework={this.props.selectedFramework} />
                                    )
                                ];
                        })}.
                </p>
            );
        }
        else if (definition.base)
            paragraphs.push(
                <p key="inheritance">
                    Extends <LinkTo reference={definition.base} selectedFramework={this.props.selectedFramework} />.
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
                                    <LinkTo key={interfaceReferenceIndex} reference={interfaceReference} selectedFramework={this.props.selectedFramework} />
                                );
                            }
                            else
                                return [
                                    ", ",
                                    (
                                        <LinkTo key={interfaceReferenceIndex} reference={interfaceReference} selectedFramework={this.props.selectedFramework} />
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
                                    <DocumentationView elements={definition.documentation.genericParameters[genericParameter.name]} selectedFramework={this.props.selectedFramework} />
                                    <GenericParameterConstraints definition={genericParameter} selectedFramework={this.props.selectedFramework} />
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
                <LinkTo key="base" reference={definition.base} selectedFramework={this.props.selectedFramework} />
            );
        if (definition.interfaces.length > 0) {
            constraintDescriptions.push(" implement ");
            definition.interfaces.forEach(interfaceReference => {
                constraintDescriptions.push(
                    <LinkTo key={interfaceReference.id} reference={interfaceReference} selectedFramework={this.props.selectedFramework} />
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

class PropertyAccessorSummary extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const definition = this.props.definition;

        if (definition.get && definition.get.access !== "private" && definition.set && definition.set.access !== "private")
            return (
                <p>
                    This property exposes a <AccessModifier access={definition.get.access} /> getter and setter.
                </p>
            );
        else if (definition.get && definition.get.access !== "private")
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
                                        <strong>{parameter.name}</strong> <LinkTo reference={parameter.type} selectedFramework={this.props.selectedFramework} />
                                    </div>
                                    <DocumentationView elements={definition.documentation.parameters[parameter.name]} selectedFramework={this.props.selectedFramework} />
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
                        <LinkTo reference={definition.return.type} selectedFramework={this.props.selectedFramework} />
                    </h3>
                    <DocumentationView elements={definition.documentation.returns} selectedFramework={this.props.selectedFramework} />
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
                                    <strong><LinkTo reference={exception.type} selectedFramework={this.props.selectedFramework} /></strong>
                                    <DocumentationView elements={exception.description} selectedFramework={this.props.selectedFramework} />
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

const AvailableFrameworkDisplayName = {
    "net20": ".NET Framework 2.0",
    "net45": ".NET Framework 4.5",
    "netcoreapp1.0": ".NET Core 1.0",
    "netstandard1.0": ".NET Standard 1.0"
}

class AvailableFrameworks extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const elements = [];
        React.Children.forEach(
            this.props.children,
            availableFramework => {
                const isSelected = (availableFramework === this.props.selected);
                const badgeType = (isSelected ? Style.badgeSuccess : Style.badgeSecondary);
                const routeParams = (isSelected ? { member: this.props.memberId } : { member: this.props.memberId, framework: availableFramework });

                elements.push(
                    <Link key={availableFramework} to={Routes.documentation(routeParams)} className={[Style.badge, badgeType].join(" ")} onClick={e => e.target.blur()}>
                        {(AvailableFrameworkDisplayName[availableFramework] || availableFramework)}
                    </Link>
                );
                elements.push(" ");
            }
        );
        return (
            <div className={Style.mb3}>
                {elements}
            </div>
        );
    }
}

function methodComparer(left, right) {
    let order = 0;
    if (left.name < right.name)
        order = -1;
    else if (left.name > right.name)
        order = 1;
    if (left.name === right.name)
        if (left.parameters.length < right.parameters.length)
            order = -1;
        else if (left.parameters.length > right.parameters.length)
            order = 1;
        else {
            let index = 0;
            while (order === 0 && index < left.parameters.length) {
                if (left.parameters[index].type.name < right.parameters[index].type.name)
                    order = -1;
                else if (left.parameters[index].type.name > right.parameters[index].type.name)
                    order = 1;
                else
                    return order;
                index++;
            }
        }
    return order;
}