const webpack = require("webpack");
const CopyWebpackPlugin = require("copy-webpack-plugin");
const cesiumSource = "node_modules/cesium/Source";
const cesiumWorkers = "../Build/Cesium/Workers";
module.exports = {
	configureWebpack: {
		resolve: {
			alias: {
				'@': resolve('src'),
				// cesium 1
				cesium: path.resolve(__dirname, cesiumSource) // ��source�ᵼ��ÿ��build����cesium js����DebugDir�������widgets.css��������
			}
		},
		amd: {
			// cesium 2
			toUrlUndefined: true
		},
		module: {
			// cesium 3 ����������ûᱨrequire���뾯��
			unknownContextCritical: false
		},
		// cesium 4
		plugins: [
			new webpack.DefinePlugin({
				// Define relative base path in cesium for loading assets
				CESIUM_BASE_URL: JSON.stringify("")
			}), // ��build��Ч��������distĿ¼�¡��磺dist/Assets
			new CopyWebpackPlugin([{
				from: path.join(cesiumSource, cesiumWorkers),
				to: "Workers"
			}]),
			new CopyWebpackPlugin([{
				from: path.join(cesiumSource, "Assets"),
				to: "Assets"
			}]),
			new CopyWebpackPlugin([{
				from: path.join(cesiumSource, "Widgets"),
				to: "Widgets"
			}]),
			new webpack.ProvidePlugin({
				Cesium: ["cesium/Cesium"], // Cesium����ʵ������ÿ��js��ʹ�ö�����import
			})
		],
		optimization: {
			minimize: process.env.NODE_ENV === "production" ? true : false
		}
	}
};