import React from "react";
import { Link } from "react-router-dom"

import Style from "mup/style";
import { Routes } from "../../routes";
import MsdnLinks from "./msdn-links.json";

export class DocumentationView extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const elements = this.props.elements;
        if (elements)
            return this.props.elements.map((element, index) => getElement(element, index, this.props.selectedFramework));
        else
            return null;
    }
}

function getElement(element, key, selectedFramework) {
    switch (element.type) {
        case "paragraph":
            return (
                <p key={key}>
                    {element.content.map(
                        (contentElement, contentElementIndex) => getElement(
                            contentElement,
                            contentElementIndex,
                            selectedFramework
                        )
                    )}
                </p>
            );

        case "reference":
            return getReference(element.reference, key, selectedFramework);

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
    "system.char": "char",
    "system.object": "object"
}

export class LinkTo extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return getReference(this.props.reference, 0, this.props.selectedFramework);
    }
}

function getReference(elementReference, key, selectedFramework) {
    if (typeof (elementReference) === "string")
        return (
            <em key={key}>
                {elementReference}
            </em>
        );
    else if (!elementReference.genericArguments || elementReference.genericArguments.every(genericArgument => typeof (genericArgument.value) === "string"))
        return getReferenceForGenericDefinition(elementReference, key, selectedFramework);
    else
        return getReferenceForGenericType(elementReference, key, selectedFramework);
}

function getReferenceForGenericDefinition(elementReference, key, selectedFramework) {
    let typeName = [FriendlyNames[elementReference.id] || elementReference.name];

    if (elementReference.genericArguments.length > 0) {
        typeName.push("<");
        elementReference
            .genericArguments
            .forEach((genericArgument, genericArgumentIndex) =>
                typeName.push(
                    getReference(genericArgument.value, genericArgumentIndex, selectedFramework),
                    ", "))
        typeName[typeName.length - 1] = ">";
    }

    let referenceComponent;
    if (elementReference.declaringAssembly.name === "Mup")
        referenceComponent = (
            <Link key={key} to={Routes.documentation({ member: elementReference.id, framework: selectedFramework })}>
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

function getReferenceForGenericType(elementReference, key, selectedFramework) {
    let referenceComponent;
    if (elementReference.declaringAssembly.name === "Mup")
        referenceComponent = (
            <Link key={key} to={Routes.documentation({ member: elementReference.id, framework: selectedFramework })}>
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
                    getReference(genericArgument.value, (key + "." + genericArgumentIndex), selectedFramework)
                ),
            ">"
        ];
    }
    else
        return referenceComponent;
}