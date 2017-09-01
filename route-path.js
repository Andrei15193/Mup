import Routes from "./config/routes.json";

const routeParamsRegex = new RegExp("(^|/):([_a-zA-Z][_a-zA-Z0-9]*)(\\?)?(?=/|$)", "gi");

function fillParameters(route, params) {
    return route.replace(routeParamsRegex, function (match, prefix, paramName, isOptional) {
        const paramValue = params[paramName];
        if (paramValue !== undefined && paramValue !== null)
            return (prefix + paramValue.toString().replace("/", "%2F"));
        else if (isOptional || paramValue === null)
            return "";
        else
            throw new Error("Value not provided for '" + paramName + "'");
    });
}

let routePath = function (name, params) {
    const route = Routes[name];
    if (route === undefined)
        throw new Error("Unknown route '" + name + "'.");
    return fillParameters(route, (params || {}));
};

routePath.names = Object.getOwnPropertyNames(Routes);
routePath
    .names
    .forEach(function (routeName) {
        const route = Routes[routeName];
        let routeMapper = (params) => fillParameters(route, (params || {}));
        routeMapper.path = route;

        routePath[routeName] = routeMapper;
        if (!routePath.default && route === "/") {
            let defaultRouteMapper = (params) => fillParameters(route, (params || {}));
            defaultRouteMapper.path = route;
            routePath.default = defaultRouteMapper;
        }
    });

export default routePath;