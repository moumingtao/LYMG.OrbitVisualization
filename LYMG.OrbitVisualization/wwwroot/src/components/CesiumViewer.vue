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
        }, async mounted() {
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

            connection.on("SayHello", () => {
                alert("Hello !");
            });

            await connection.start();
            connection.send("ViewerEnter", this.name);
            //connection.send("ViewerInvoke", this.name, "SayHello", "233");
        }, data() {
            return {
                viewer: null
            }
        }
    }
</script>
<style scoped>
    div{width:100%;height:100%;}
</style>