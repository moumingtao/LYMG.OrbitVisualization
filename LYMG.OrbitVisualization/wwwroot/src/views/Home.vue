<template>
    <div class="viewer">
        <vc-viewer v-bind="settings">
            <vc-layer-imagery>
                <vc-provider-imagery-urltemplate url="https://localhost:44389/tile/googleTiles/?x={x}&y={y}&z={z}"></vc-provider-imagery-urltemplate>
            </vc-layer-imagery>
            <vc-navigation :options="options"></vc-navigation>
        </vc-viewer>
        <div style="position:fixed;left:2px;top:5px;">
            <button type="button" class="cesium-button cesium-toolbar-button">卫星</button>
            <button type="button" class="cesium-button cesium-toolbar-button">
                <span class="icon fill-parent">
                    233
                </span>
            </button>
        </div>
        <dv-border-box-1 style="position:fixed;width:28%;height:auto;top:2.4em;left:0;bottom:120px;"></dv-border-box-1>
        <dv-border-box-4 style="position:fixed;width:40%;height:initial;top:350px;right:0;bottom:54px;" :reverse="true"></dv-border-box-4>
    </div>
</template>
<script>
    import 'vue-cesium/lib/vc-navigation.css'
    export default {
        data() {
            return {
                settings: {
                    animation: true,// 是否显示动画控件
                    fullscreenButton: true,// 是否显示全屏切换按钮
                    geocoder: true,// 是否显示地理编码器搜索框
                    homeButton: true,// 是否显示主页按钮
                    sceneModePicker: true,//是否显示场景模式切换按钮
                    timeline: true,// 是否显示时间轴控件
                    navigationHelpButton: true,//是否显示导航帮助按钮
                    //scene3DOnly:true,// 如果为true，则每个几何实例仅以3D形式呈现以节省GPU内存。
                    shadows: true,//确定阴影是否由太阳投射形成。
                    projectionPicker:true,
                    logo:false,
                },
                options: {
                    enableCompass: true,
                    enableZoomControl: true,
                    enableDistanceLegend: true,
                    enableLocationBar: true,
                    enableCompassOuterRing: true,
                    enablePrintView: true,
                    enableMyLocation: {
                        // 使用高德api定位
                        amap: {
                            key: '42d22e6ed83f077bc28b7864718726de'
                        }
                    }
                }, visible:true
            }
        }
    }
</script>
<style>
    .viewer {
        width: 100%;
        height: 100%;
        color:#999;
    }
    .viewer .el-tabs__item{color:#999}
    .viewer .el-tabs__content{color:#999;}
</style>