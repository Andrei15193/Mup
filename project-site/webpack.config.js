const isProduction = !!process.argv.find(item => item == '-p');

const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    mode: isProduction ? 'production' : 'development',
    devtool: isProduction ? 'none' : 'eval-source-map',
    entry: {
        app: './app.jsx'
    },
    output: {
        path: path.resolve(__dirname, 'build')
    },
    resolve: {
        extensions: ['.js', '.jsx']
    },
    module: {
        rules: [
            {
                test: /\.js(x?)$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ["@babel/preset-env", "@babel/preset-react"]
                    }
                }
            },
            {
                test: /\.s[ac]ss$/i,
                use: ['style-loader', 'css-loader', 'sass-loader',],
            },
            {
                test: /\.png$/,
                use: {
                    loader: 'file-loader',
                    options: {
                        name: '[name].[ext]?[hash]'
                    }
                }
            }
        ]
    },
    plugins: [
        new HtmlWebpackPlugin({
            title: 'Mup - Markup for Everyone',
            hash: true,
            favicon: './images/favicon.ico',
            minify: {
                collapseBooleanAttributes: isProduction,
                collapseWhitespace: isProduction,
                quoteCharacter: '\'',
                removeComments: true,
                removeScriptTypeAttributes: true
            },
            templateContent: ({ htmlWebpackPlugin }) => `
                <!DOCTYPE html>
                <html>
                    <head>
                        <meta charset="utf-8">
                        <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate">
                        <meta http-equiv="Pragma" content="no-cache">
                        <meta http-equiv="Expires" content="0">
                        ${htmlWebpackPlugin.tags.headTags}
                        <title>${htmlWebpackPlugin.options.title}</title>
                        <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css" integrity="sha384-9aIt2nRpC12Uk9gS9baDl411NQApFmC26EwAOH8WgZl5MYYxFfc+NcPb1dKGj7Sk" crossorigin="anonymous">
                    </head>
                    <body>
                        <div id='app'></div>
                        <script crossorigin src='https://unpkg.com/react@16/umd/react.${isProduction ? 'production.min' : 'development'}.js'></script>
                        <script crossorigin src='https://unpkg.com/react-dom@16/umd/react-dom.${isProduction ? 'production.min' : 'development'}.js'></script>
                        ${htmlWebpackPlugin.tags.bodyTags}
                    </body>
                </html>`
        })
    ],
    externals: {
        'react': 'React',
        'react-dom': 'ReactDOM'
    }
};
