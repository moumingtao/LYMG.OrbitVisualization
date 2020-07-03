<template>
    <div>
        <TopLeftToolBar></TopLeftToolBar>
    </div>
</template>
<script>
    import * as Cesium from 'cesium/Build/Cesium/Cesium.js'
    import 'cesium/Build/Cesium/Widgets/widgets.css'
    import TopLeftToolBar from "./TopLeftToolBar.vue"
    import cesiumZH from '@/modules/CesiumZH'

    export default {
        props: {
            name: { type: String }
        }, components: {
            TopLeftToolBar
        }, mounted() {
            this.$el.insertBefore(this.viewerContainer, this.$el.children[0]);
            cesiumZH(this.$el);
        }, data() {
            return {
                viewer: null,
                viewerContainer: null,
            }
        }, provide() {
            if (!this.viewer) {
                Cesium.buildModuleUrl.setBaseUrl('/Cesium/')
                var div = document.createElement("div")
                div.style.setProperty("width", "100%")
                div.style.setProperty("height", "100%")
                this.viewer = new Cesium.Viewer(div);
                this.viewerContainer = div;
            }
            return { "cesiumVue": this, }
        }
    }
</script>
<style scoped>
    div{width:100%;height:100%;}
</style>