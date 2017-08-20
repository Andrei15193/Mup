const IS_PRODUCTION = (process.argv.indexOf("-p") !== -1);
const BUILD_PARAMETERS = {
    isProduction: IS_PRODUCTION,
    config: (IS_PRODUCTION ? "release" : "debug")
};

const path = require("path");
const UglifyJsPlugin = require("webpack").optimize.UglifyJsPlugin;
const HtmlWebpackPlugin = require("html-webpack-plugin");

const aliases = require('./config/aliases.json');
const config = require(resolve("./config/build.${config}.json", BUILD_PARAMETERS));

module.exports = {
    context: __dirname,
    entry: path.join(__dirname, "view", "index.jsx"),
    output: {
        path: path.join(__dirname, "build"),
        publicPath: "./",
        filename: "app.min.js?[hash]"
    },
    resolve: {
        extensions: [".js", ".jsx", ".json"],
        alias: mapAliases(__dirname, aliases, BUILD_PARAMETERS)
    },
    devtool: config.devtool,
    module: {
        loaders: [
            {
                test: /\.(js|jsx)$/,
                exclude: [/node_modules/, /lib/],
                loader: "babel-loader",
                query: {
                    presets: ["react", "es2015"],
                    plugins: ["react-html-attrs"]
                }
            },
            {
                test: /\.css$/,
                exclude: /node_modules/,
                loader: "style-loader!css-loader?camelCase&modules=true&localIdentName=[name]__[local]___[hash:base64:5]"
            },
            {
                test: /\.html$/,
                loader: "html-loader",
                options: config.html
            },
            {
                test: /\.(png|svg|jpg|gif)$/,
                exclude: /node_modules/,
                loader: "file-loader"
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/,
                include: path.join(__dirname, "lib"),
                loader: "file-loader"
            },
            {
                test: /\.js$/,
                include: path.join(__dirname, "lib"),
                loader: "file-loader?name=[name].[ext]"
            }
        ]
    },
    plugins: [
        new UglifyJsPlugin({
            compress: config.uglifyJs.compress,
            beautify: config.uglifyJs.beautify,
            sourceMap: config.uglifyJs.sourceMap,
            warningsFilter: getWarningsFilter(config.uglifyJs.warnings)
        }),
        new HtmlWebpackPlugin({
            template: path.join(__dirname, "view", "index.html")
        })
    ]
};

function mapAliases(rootFolder, aliases, parameters) {
    var result = {};
    Object
        .getOwnPropertyNames(aliases)
        .forEach(function (aliasKey) {
            var alias = aliases[aliasKey];
            Object.defineProperty(
                result,
                aliasKey,
                {
                    enumerable: true,
                    writable: false,
                    configurable: false,
                    value: path.join(rootFolder, resolve(alias, parameters))
                });
        });
    return result;
}

function resolve(path, parameters) {
    if (parameters) {
        const allowedParameterNamePattern = /\w+/;
        const parameterSubstitutionPattern = new RegExp("\\$\\{("
            + Object
                .getOwnPropertyNames(parameters)
                .filter(parameterName => allowedParameterNamePattern.test(parameterName))
                .join("|")
            + ")\\}", "gi");
        path = path.replace(parameterSubstitutionPattern, (match, parameterName) => parameters[parameterName]);
    }
    return path;
}

function getWarningsFilter(warningsConfig) {
    if (warningsConfig.exclude && warningsConfig.exclude.length > 0) {
        var regExp = new RegExp("(\/|^)(" + warningsConfig.exclude.join("|") + ")(\/|$)");
        return regExp.test.bind(regExp);
    }
    else
        return () => false;
}