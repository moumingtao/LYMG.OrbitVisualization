<template>
    <el-container>
        <el-aside>
            <el-scrollbar style="height: 100vh" wrap-class="scrollbar-wrapper">
                <el-menu default-active="1-4-1" background-color="initial">
                    <el-submenu index="1">
                        <template slot="title">
                            <i class="el-icon-location"></i>
                            <span slot="title">导航一</span>
                        </template>
                        <el-menu-item-group>
                            <span slot="title">分组一</span>
                            <el-menu-item index="1-1">选项1</el-menu-item>
                            <el-menu-item index="1-2">选项2</el-menu-item>
                        </el-menu-item-group>
                        <el-menu-item-group title="分组2">
                            <el-menu-item index="1-3">选项3</el-menu-item>
                        </el-menu-item-group>
                        <el-submenu index="1-4">
                            <span slot="title">选项4</span>
                            <el-menu-item index="1-4-1">选项1</el-menu-item>
                        </el-submenu>
                    </el-submenu>
                    <el-menu-item index="2">
                        <i class="el-icon-menu"></i>
                        <span slot="title">导航二</span>
                    </el-menu-item>
                    <el-menu-item index="3" disabled>
                        <i class="el-icon-document"></i>
                        <span slot="title">导航三</span>
                    </el-menu-item>
                    <el-menu-item index="4">
                        <i class="el-icon-setting"></i>
                        <span slot="title">导航四</span>
                    </el-menu-item>
                </el-menu>
            </el-scrollbar>
            <!--<el-autocomplete placeholder="请输入内容" style="width:100%;">
                <i class="el-icon-search el-input__icon" slot="suffix"></i>
            </el-autocomplete>-->
        </el-aside>
        <el-main>
            <el-tabs type="card" closable>
                <el-tab-pane label="消息中心">消息中心</el-tab-pane>
                <el-tab-pane label="角色管理">角色管理</el-tab-pane>
                <el-tab-pane label="定时任务补偿">定时任务补偿</el-tab-pane>
            </el-tabs>
        </el-main>
    </el-container>
</template>
<script>
</script>
<style scoped>
    .el-menu{border-right:initial;}
    .el-aside{background-color:#9CB;}
    .el-container{height:100vh;}
    .el-main{padding:0;}
</style>
<style>
    .el-scrollbar__wrap {
        overflow: scroll;
        width: 110%;
        height: 120%;
    }
</style>