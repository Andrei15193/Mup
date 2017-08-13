import { Dispatcher } from "flux";

import ParserActions from "actions/parser-actions";
import ParserStore from "stores/parser-store";

module.exports = new DependencyContainer({
    dispatcher: new Dispatcher(),
    parserActions: (dispatcher) => new ParserActions(dispatcher),
    parserStore: singleton((dispatcher) => new ParserStore(dispatcher))
});

function singleton(factory) {
    if (typeof (factory) !== "function")
        throw new Error("Expected factory to be a function.");

    var value = null;
    return function () {
        if (value === null)
            value = resolveFunc.call(this, factory);
        return value;
    };
}

function DependencyContainer(dependencies) {
    Object
        .getOwnPropertyNames(dependencies)
        .forEach(function (dependencyKey) {
            const dependencyValue = dependencies[dependencyKey];

            var propertyConfig;
            if (typeof (dependencyValue) === "function")
                propertyConfig = {
                    enumerable: true,
                    configurable: false,
                    get: resolveFunc.bind(this, dependencyValue)
                };
            else
                propertyConfig = {
                    enumerable: true,
                    writable: false,
                    configurable: false,
                    value: dependencyValue
                };

            Object.defineProperty(
                this,
                dependencyKey,
                propertyConfig);
        }, this);

    Object.freeze(this);
}

function resolveFunc(func) {
    var parameterNames = getParameterNames(func);
    var parameters = parameterNames.map(parameterName => resolve.call(this, parameterName, [parameterName]));
    return func.apply(this, parameters);
}

function resolve(key, visitedKeys) {
    var value = this[key];
    if (value === undefined)
        value = this[toPascalCase(key)];
    if (value === undefined)
        throw new Error("Dependency '" + key + "' is undefined");

    if (typeof (value) === "function") {
        var parameterNames = getParameterNames(value);
        var parameters = parameterNames.map(parameterName => resolve.call(this, parameterName, visitedKeys.concat([parameterName])));
        return value.apply(this, parameters);
    }

    return value;
}

function getParameterNames(func) {
    var parameterNamesString = func.toString().match(/function[^{]*?\(([^)]*)\)/)[1];
    var parameterNames = parameterNamesString.split(",").map(parameterName => parameterName.trim()).filter(parameterName => parameterName.length > 0);
    return parameterNames;
}

function toPascalCase(value) {
    if (value && value.length > 0)
        return (value[0].toUpperCase() + value.substr(1));
    else
        return value;
}