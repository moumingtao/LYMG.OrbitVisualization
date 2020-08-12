<template>
    <el-popover placement="bottom" title="探测需求" width="300" trigger="hover">
        <button class="cesium-button cesium-toolbar-button el-icon-map-location" style="font-size:26px;" slot="reference"></button>
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