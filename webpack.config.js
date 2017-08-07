const IS_PRODUCTION = (process.argv.indexOf("-p") !== -1);

const path = require("path");
const Webpack = require("webpack");
const HtmlWebpackPlugin = require("html-webpack-plugin");

const aliases = getAliases.call(require('./config/aliases.json'));
function getAliases() {
    var result = {};
    Object
        .getOwnPropertyNames(this)
        .forEach(function (aliasKey) {
            var alias = this[aliasKey];
            Object.defineProperty(
                result,
                aliasKey,
                {
                    enumerable: true,
                    writable: false,
                    configurable: false,
                    value: path.join(__dirname, alias)
                });
        }, this);
    return result;
}

module.exports = {
    context: __dirname,
    devtool: (IS_PRODUCTION ? false : "inline-sourcemap"),
    entry: path.join(__dirname, "index.jsx"),
    resolve: {
        extensions: [".js", ".jsx", ".json"],
        alias: aliases
    },
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
                options: {
                    minimize: IS_PRODUCTION
                }
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
    output: {
        path: path.join(__dirname, "build"),
        publicPath: "./",
        filename: "app.min.js"
    },
    plugins: [
        (IS_PRODUCTION ?
            new Webpack.DefinePlugin({
                "process.env": {
                    NODE_ENV: JSON.stringify("production")
                }
            }) : null),
        (IS_PRODUCTION ? new Webpack.optimize.UglifyJsPlugin() : null),
        new HtmlWebpackPlugin({
            template: path.join(__dirname, "index.html")
        })
    ].filter(plugin => plugin != null)
};