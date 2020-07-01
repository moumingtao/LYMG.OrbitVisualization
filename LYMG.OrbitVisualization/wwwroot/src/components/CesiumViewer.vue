<template>
    <div ref="viewer"></div>
</template>
<script>
    import * as Cesium from 'cesium/Build/Cesium/Cesium.js'
    import 'cesium/Build/Cesium/Widgets/widgets.css'
    import * as signalR from "@microsoft/signalr";
    export default {
        props: {
            name: { type: String }
        }, mounted() {
            Cesium.buildModuleUrl.setBaseUrl('/Cesium/')
            this.viewer = new Cesium.Viewer(this.$refs.viewer);
            this.viewer._cesiumWidget._creditContainer.style.display = "none";
            this.viewer.imageryLayers.addImageryProvider(
                new Cesium.UrlTemplateImageryProvider({
                    url: "https://localhost:44389/tile/googleTiles/?x={x}&y={y}&z={z}"
                }));
            const connection = new signalR.HubConnectionBuilder()
                .withUrl("https://localhost:44389/cesiumHub/")
                .build();

            connection.on("messageReceived", (msg) => {
                console.log(msg);
            });

            connection.start().catch(err => console.log(err));

        }, data() {
            return {
                viewer:null
            }
        }
    }
</script>
<style scoped>
    div{width:100%;height:100%;}
</style>