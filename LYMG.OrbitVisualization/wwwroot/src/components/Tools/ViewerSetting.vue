<template>
    <el-popover placement="bottom" title="设置" width="300" trigger="hover">
        <button class="cesium-button cesium-toolbar-button el-icon-setting" style="font-size:28px" slot="reference"></button>
    </el-popover>
</template>
<script>
    import * as Cesium from 'cesium/Build/Cesium/Cesium.js'
    export default {
        inject: ["cesiumVue"],
        mounted() {
            var viewer = this.cesiumVue.viewer;
            viewer._cesiumWidget._creditContainer.style.display = "none";
            viewer.imageryLayers.removeAll();
            viewer.imageryLayers.addImageryProvider(
                new Cesium.UrlTemplateImageryProvider({
                    url: "https://localhost:44389/tile/googleTiles?x={x}&y={y}&z={z}"
                }));
        }
    }
</script>