const isProduction = !!process.argv.find(item => item == "-p");

const path = require("path");
const webpack = require("webpack");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const UglifyJsWebpackPlugin = require("uglifyjs-webpack-plugin");

const imports = require("./webpack.imports").call({
    isProduction: isProduction
});
const aliases = Object
    .getOwnPropertyNames(imports)
    .reduce((result, name) => Object.defineProperty(
        result,
        name,
        {
            enumerable: true,
            value: path.resolve(__dirname, imports[name])
        }), {});

module.exports = {
    entry: "./app.jsx",
    resolve: {
        extensions: [".js", ".jsx", ".json"],
        alias: aliases
    },
    devtool: (isProduction ? false : "source-map"),
    output: {
        path: path.join(__dirname, "build"),
        filename: "app.js"
    },
    module: {
        rules: [
            {
                test: /\.js(x?)$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                }
            },
            {
                test: /\.png$/,
                use: "file-loader"
            },
            {
                test: /\.scss$/,
                use: ExtractTextPlugin.extract({
                    fallback: 'style-loader',
                    use: [
                        {
                            loader: "css-loader",
                            options: {
                                camelCase: true,
                                localIdentName: (isProduction ? "[hash:base64]" : "[path][name]__[local]__[hash:base64:5]"),
                                minimize: isProduction,
                                modules: true
                            }
                        },
                        {
                            loader: "postcss-loader",
                            options: {
                                plugins: () => [
                                    require("precss"),
                                    require("autoprefixer")
                                ]
                            }
                        },
                        {
                            loader: "sass-loader"
                        }
                    ]
                })
            }
        ]
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery",
            "window.jQuery": "jquery",
            Popper: ["popper.js", "default"]
        }),
        new ExtractTextPlugin({
            filename: 'style.css'
        }),
        new UglifyJsWebpackPlugin({
            sourceMap: !isProduction
        }),
        new HtmlWebpackPlugin({
            title: "Mup - Markup for Everyone",
            hash: true,
            favicon: aliases["mup/images/favicon"],
            minify: {
                collapseBooleanAttributes: isProduction,
                collapseWhitespace: isProduction,
                quoteCharacter: "\"",
                removeComments: true,
                removeScriptTypeAttributes: true
            }
        })
    ].filter(plugin => !!plugin)
};