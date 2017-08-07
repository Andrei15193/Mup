import Routes from "./config/routes.json";

var routePath = function (name, params) {
    var route = this[name];
    if (params)
        Object
            .getOwnPropertyNames(params)
            .filter((paramName) => /^[_a-zA-Z][_a-zA-Z0-9]*$/.test(paramName))
            .forEach(function (paramName) {
                const paramValue = params[paramName];
                const paramStringValue = (paramValue ? paramValue.toString() : "");
                const regExp = new RegExp(":" + paramName + "(?=/|$)");
                route = route.replace(regExp, paramStringValue);
            });

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