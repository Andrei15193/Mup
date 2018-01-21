import React from "react";
import { Link } from "react-router-dom"

import Routes from "mup/routes";
import Style from "mup/style";
import MsdnLinks from "./msdn-links";

export class DocumentationView extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const elements = this.props.elements;
        if (elements)
            return this.props.elements.map(getElement);
        else
            return null;
    }
}

function getElement(element, key) {
    switch (element.type) {
        case "paragraph":
            return (
                <p key={key}>
                    {element.content.map(
                        (contentElement, contentElementIndex) => getElement(
                            contentElement,
                            contentElementIndex
                        )
                    )}
                </p>
            );

        case "reference":
            return getReference(element.reference, key);

        case "parameterReference":
            return (
                <em key={key}>
                    {element.name}
                </em>
            );

        case "text":
            return element.value;

        case "code":
            return (
                <code key={key}>
                    {element.text}
                </code>
            );

        default:
            throw new Error("Unknown element '" + (element.type || JSON.stringify(element)) + "'.");
    }
}

export const FriendlyNames = {
    "system.sbyte": "sbyte",
    "system.byte": "byte",
    "system.int16": "short",
    "system.uint16": "ushort",
    "system.int32": "int",
    "system.uint32": "uint",
    "system.int64": "long",
    "system.uint64": "ulong",
    "system.single": "float",
    "system.double": "double",
    "system.decimal": "decimal",
    "system.string": "string",
    "system.char": "char"
}

export class LinkTo extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return getReference(this.props.reference, 0);
    }
}

function getReference(elementReference, key) {
    if (typeof (elementReference) === "string")
        return (
            <em key={key}>
                {elementReference}
            </em>
        );
    else if (!elementReference.genericArguments || elementReference.genericArguments.every(genericArgument => typeof (genericArgument.value) === "string"))
        return getReferenceForGenericDefinition(elementReference, key);
    else
        return getReferenceForGenericType(elementReference, key);
}

function getReferenceForGenericDefinition(elementReference, key) {
    let typeName = [FriendlyNames[elementReference.id] || elementReference.name];

    if (elementReference.genericArguments.length > 0) {
        typeName.push("<");
        elementReference
            .genericArguments
            .forEach((genericArgument, genericArgumentIndex) =>
                typeName.push(
                    getReference(genericArgument.value, genericArgumentIndex),
                    ", "))
        typeName[typeName.length - 1] = ">";
    }

    let referenceComponent;
    if (elementReference.declaringAssembly.name === "Mup")
        referenceComponent = (
            <Link key={key} to={Routes.documentation({ member: elementReference.id })}>
                {typeName}
            </Link>
        );
    else {
        const msdnLink = MsdnLinks[elementReference.id];
        if (!msdnLink)
            console.warn("MSDN Link not defined for '" + elementReference.id + "'.");
        referenceComponent = (
            <a key={key} href={msdnLink} target="_blank">
                {typeName}
            </a>
        );
    }

    return referenceComponent;
}

function getReferenceForGenericType(elementReference, key) {
    let referenceComponent;
    if (elementReference.declaringAssembly.name === "Mup")
        referenceComponent = (
            <Link key={key} to={Routes.documentation({ member: elementReference.id })}>
                {elementReference.name}
            </Link>
        );
    else {
        const msdnLink = MsdnLinks[elementReference.id];
        if (!msdnLink)
            console.warn("MSDN Link not defined for '" + elementReference.id + "'.");
        referenceComponent = (
            <a key={key} href={msdnLink} target="_blank">
                {(FriendlyNames[elementReference.id] || elementReference.name)}
            </a>
        );
    }

    if (elementReference.genericArguments.length > 0) {
        return [
            referenceComponent,
            "<",
            elementReference
                .genericArguments
                .map((genericArgument, genericArgumentIndex) =>
                    getReference(genericArgument.value, (key + "." + genericArgumentIndex))
                ),
            ">"
        ];
    }
    else
        return referenceComponent;
}