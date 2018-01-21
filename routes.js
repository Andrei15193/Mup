export const RoutePaths = {
    "home": "/",
    "onlineParser": "/online-parser",
    "documentation": "/documentation/:member?",
    "license": "/license"
};

export const Routes = {
    home: fillParameters.bind({ route: RoutePaths.home }),
    onlineParser: fillParameters.bind({ route: RoutePaths.onlineParser }),
    documentation: fillParameters.bind({ route: RoutePaths.documentation }),
    license: fillParameters.bind({ route: RoutePaths.license })
};

function fillParameters(params) {
    const routeParamsRegex = new RegExp("(^|/):([_a-zA-Z][_a-zA-Z0-9]*)(\\?)?(?=/|$)", "gi");

    const result = this.route.replace(routeParamsRegex, function (match, prefix, paramName, isOptional) {
        const paramValue = (params ? params[paramName] : undefined);
        if (paramValue !== undefined && paramValue !== null)
            return (prefix + paramValue.toString().replace("/", "%2F"));
        else if (isOptional || paramValue === null)
            return "";
        else
            throw new Error("Value not provided for '" + paramName + "'");
    });

    return result;
}