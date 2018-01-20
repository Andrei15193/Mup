export const RoutePaths = {
    "home": "/",
    "onlineParser": "/online-parser",
    "documentation": "/documentation/:member?",
    "license": "/license"
};

export default Object
    .getOwnPropertyNames(RoutePaths)
    .reduce((routes, routeName) => {
        const routePath = RoutePaths[routeName].toString();
        return Object.defineProperty(
            routes,
            routeName,
            {
                enumerable: true,
                value: (params => fillParameters(routePath, params))
            });
    }, {});

function fillParameters(route, params) {
    const routeParamsRegex = new RegExp("(^|/):([_a-zA-Z][_a-zA-Z0-9]*)(\\?)?(?=/|$)", "gi");

    const result = route.replace(routeParamsRegex, function (match, prefix, paramName, isOptional) {
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