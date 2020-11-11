<template>
    <el-popover placement="bottom" title="数据源" width="450" trigger="hover">
        <button class="cesium-button cesium-toolbar-button el-icon-picture-outline-round" style="font-size:26px;" slot="reference"></button>
        <el-button @click="loadCZML">加载CZML</el-button>
    </el-popover>
</template>
<script>
import * as Cesium from 'cesium/Build/Cesium/Cesium.js'
export default {
    inject: ["cesiumVue"],
    data(){
        return {
            tableData:[],
            search:""
        }
    },methods:{
        loadCZML(){
            this.cesiumVue.viewer.dataSources.add(
                Cesium.CzmlDataSource.load("https://sandcastle.cesium.com/SampleData/simple.czml")
            );
        }
    }
}
</script>