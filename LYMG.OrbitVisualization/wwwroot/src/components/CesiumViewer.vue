<template>
    <div>
        <div ref="viewer"></div>
        <TopLeftToolBar></TopLeftToolBar>
    </div>
</template>
<script>
    import * as Cesium from 'cesium/Build/Cesium/Cesium.js'
    import 'cesium/Build/Cesium/Widgets/widgets.css'
    import TopRightToolBarExtend from "./TopRightToolBarExtend.vue"
    import TopLeftToolBar from "./TopLeftToolBar.vue"
    import Vue from 'vue'
    export default {
        props: {
            name: { type: String }
        }, components: {
            TopLeftToolBar
        }, async mounted() {
            Cesium.buildModuleUrl.setBaseUrl('/Cesium/')
            this.viewer = new Cesium.Viewer(this.$refs.viewer);

            // 扩展工具栏
            var span = document.createElement("span");
            var toolbarExt = new Vue({
                render: h => h(TopRightToolBarExtend)
            }).$mount(span)
            this.$refs.viewer
                .getElementsByClassName("cesium-navigationHelpButton-wrapper")[0]
                .before(toolbarExt.$el);
        }, data() {
            return {
                viewer: null,
            }
        }, provide() {
            return { "cesiumVue":this, }
        }
    }
</script>
<style scoped>
    div{width:100%;height:100%;}
</style>