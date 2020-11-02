<template>
    <el-popover placement="bottom" title="地面站" width="300" trigger="hover">
        <button class="cesium-button cesium-toolbar-button" slot="reference">
            <img src="../../assets/GroundStation.svg" />
        </button>
        <el-table :data="tableData" style="width: 100%">
            <el-table-column type="selection" width="28" />
            <el-table-column prop="date" label="名称" />
            <el-table-column align="right">
                <template slot="header">
                    <el-input v-model="search" size="mini" placeholder="输入关键字搜索" />
                </template>
                <template slot-scope="scope">
                    <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">Edit</el-button>
                    <el-button size="mini" type="danger" @click="handleDelete(scope.$index, scope.row)">Delete</el-button>
                </template>
            </el-table-column>
        </el-table>
    </el-popover>
</template>
<script>
export default {
    data(){
        return {
            tableData:[],
            search:""
        }
    }
}
</script>