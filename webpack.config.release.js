module.exports = {
    devtool: false,
    uglifyJs: {
        compress: true,
        beautify: false,
        sourceMap: true,
        warningsFilter: (fileName) => /(\/|^)node_modules(\/|$)/.test(fileName)
    },
    html: {
        minimize: true
    }
};