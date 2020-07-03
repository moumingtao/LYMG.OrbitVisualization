<template>
    <div ref="viewer"></div>
</template>
<script>
    import * as Cesium from 'cesium/Build/Cesium/Cesium.js'
    import 'cesium/Build/Cesium/Widgets/widgets.css'
    import * as signalR from "@microsoft/signalr";
    import ToolBar from "./CesiumViewerRightToolBar.vue"
    import Vue from 'vue'
    export default {
        props: {
            name: { type: String }
        }, async mounted() {
            Cesium.buildModuleUrl.setBaseUrl('/Cesium/')
            this.viewer = new Cesium.Viewer(this.$refs.viewer);
            this.viewer._cesiumWidget._creditContainer.style.display = "none";
            this.viewer.imageryLayers.addImageryProvider(
                new Cesium.UrlTemplateImageryProvider({
                    url: "https://localhost:44389/tile/googleTiles?x={x}&y={y}&z={z}"
                }));

            // 扩展工具栏
            var span = document.createElement("span");
            var toolbarExt = new Vue({
                render: h => h(ToolBar)
            }).$mount(span)
            this.$refs.viewer.getElementsByClassName("cesium-navigationHelpButton-wrapper")[0].before(toolbarExt.$el);

            // 连接SignalR
            this.connectSignalR();
        }, data() {
            return {
                viewer: null,
                connection: null,
                enterMessage: null,
            }
        }, methods: {
            async connectSignalR() {
                this.connection = new signalR.HubConnectionBuilder()
                    .withUrl("https://localhost:44389/cesiumHub/")
                    .build();
                this.connection.on("eval", this.onEval.bind(this));
                await this.connection.start();
                this.connection.send("ViewerEnter", this.name);
            }, onEval(cid, script, gs) {// SignalR远程调用
                var ret = eval(script);
                if (cid) {
                    if (ret && ret.then)
                        ret.then(res => this.connection.send("ViewerResponse", cid, res));
                    else
                        this.connection.send("ViewerResponse", cid, ret);
                }
                if (gs) { return; }
            }
        }
    }
</script>
<style scoped>
    div{width:100%;height:100%;}
</style>