const isProduction = !!process.argv.find(item => item == "-p");

const path = require("path");
const webpack = require("webpack");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const UglifyJsWebpackPlugin = require("uglifyjs-webpack-plugin");

module.exports = {
    entry: [
        "babel-polyfill",
        "./app.jsx"
    ],
    resolve: {
        extensions: [".js", ".jsx"],
        alias: {
            "mup/style": path.join(__dirname, "./views/style.scss"),
            "mup/config": path.join(__dirname, (isProduction ? "./webpack.config.js.release.json" : "./webpack.config.js.debug.json"))
        }
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
                                camelCase: "only",
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
            favicon: "./images/favicon.ico",
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