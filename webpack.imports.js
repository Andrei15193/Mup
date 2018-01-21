module.exports = function () {
    return {
        "mup/style": "./views/style.scss",
        "mup/images/logo": "./images/logo.png",
        "mup/images/favicon": "./images/favicon.ico",
        "mup/dependency-container": "./dependency-container",
        "mup/config": (this.isProduction ? "./webpack.imports.js.release" : "./webpack.imports.js.debug"),
        "mup/action-data": "./action-data",
        "mup/actions/request": "./actions/request",
        "mup/actions/editor-actions": "./actions/editor-actions",
        "mup/actions/parse-actions": "./actions/parse-actions",
        "mup/stores/editor-store": "./stores/editor-store",
        "mup/stores/preview-store": "./stores/preview-store",
        "mup/views/layout": "./views/layout/module",
        "mup/views/common": "./views/common/module"
    }
};