<template>
  <div id="app">
  <el-tabs v-model="activeName" tab-position="left">
    <el-tab-pane label="卫星管理" name="first">
      <el-button>创建</el-button>
      <el-button>删除所选(0)</el-button>
      <el-button>批量设置(0)</el-button>
      <el-input v-model="sarchManaged" placeholder="搜索在管" style="width:8em;" clearable autocomplete></el-input>
      <el-table>
        <el-table-column type="index" width="55"></el-table-column>
        <el-table-column type="selection" width="55"></el-table-column>
        <el-table-column prop="date" label="ID" width="64" sortable>
        </el-table-column>
        <el-table-column prop="date" label="卫星名称" width="120" sortable>
        </el-table-column>
        <el-table-column prop="date" label="最新根数" width="120" sortable>
          点击打开
        </el-table-column>
        <el-table-column prop="date" label="在管数" width="86" sortable>
          点击打开
        </el-table-column>
        <el-table-column prop="date" label="同步字" width="86" sortable>
        </el-table-column>
        <el-table-column prop="date" label="UID" width="72" sortable>
        </el-table-column>
        <el-table-column prop="date" label="探测角度" width="100" sortable>
        </el-table-column>
        
      </el-table>
    </el-tab-pane>
    <el-tab-pane label="卫星根数" name="second"></el-tab-pane>
    <el-tab-pane label="地面站管理" name="third"></el-tab-pane>
    <el-tab-pane label="探测区域" name="fourth"></el-tab-pane>
  </el-tabs>
  </div>
</template>

<script>
  export default {
    data() {
      return {
        activeName: 'first',
        sarchManaged:null,
      };
    },
    methods: {
      
    }
  };
</script>