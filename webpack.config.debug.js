module.exports = {
    devtool: "source-map",
    uglifyJs: {
        compress: false,
        beautify: true,
        sourceMap: true,
        warningsFilter: (fileName) => /(\/|^)node_modules(\/|$)/.test(fileName)
    },
    html: {
        minimize: false
    }
};