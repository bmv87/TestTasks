const path = require("path");

module.exports = {
    mode: 'development',
    entry: {
        "jquery-3.0.0.slim.min": path.resolve(__dirname, "./Scripts/jquery-3.0.0.slim.min.js"),
        "popper": path.resolve(__dirname, "./Scripts/popper.js"),
        "popper-utils": path.resolve(__dirname, "./Scripts/popper-utils.js"),
        "bootstrap": path.resolve(__dirname, "./Scripts/bootstrap.js")
    },
    output: {
        path: path.resolve(__dirname, "./Scripts/app"),
        filename: "[name].js"
    },
    resolve: {
        modules: [
            path.join(__dirname, "Scripts"),
            "node_modules"
        ]
    },
    module: {
        rules: [
            {
                loader: 'babel-loader',
                test: /\.js$/,
                exclude: /node_modules/
            }
        ]
    }
}