import Routes from "./config/routes.json";

const routeParamsRegex = new RegExp("(^|/):([_a-zA-Z][_a-zA-Z0-9]*)(\\?)?(?=/|$)", "gi");

function fillParameters(route, params) {
    return route.replace(routeParamsRegex, function (match, prefix, paramName, isOptional) {
        const paramValue = params[paramName];
        if (paramValue !== undefined && paramValue !== null)
            return (prefix +paramValue.toString().replace("/", "%2F"));
        else if (isOptional || paramValue === null)
            return "";
        else
            throw new Error("Value not provided for '" + paramName + "'");
    });
}

let routePath = function (name, params) {
    const route = this[name];
    if (params)
        return fillParameters(route, params);
    else
        return route;
}.bind(Routes);

routePath.names = Object.getOwnPropertyNames(Routes);
routePath
    .names
    .forEach(function (routeName) {
        const route = Routes[routeName];
        routePath[routeName] = route;
        if (!routePath.default && route === "/")
            routePath.default = route;
    });

export default routePath;