<template>
    <button class="cesium-button">Tile</button>
</template>
<script>
    import * as Cesium from 'cesium/Build/Cesium/Cesium.js'
    export default {
        inject: ["cesiumVue"],
        created() {
            this.cesiumVue.viewer._cesiumWidget._creditContainer.style.display = "none";
            this.cesiumVue.viewer.imageryLayers.addImageryProvider(
                new Cesium.UrlTemplateImageryProvider({
                    url: "https://localhost:44389/tile/googleTiles?x={x}&y={y}&z={z}"
                }));
        }
    }
</script>