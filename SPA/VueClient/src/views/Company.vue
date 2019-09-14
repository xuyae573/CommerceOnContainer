
<template>
  <div class="company">
    <h1>This is an Company List page</h1>
    <el-button @click="create">Load</el-button>
    <el-table :data="tableData"
      style="width: 100%">
      <el-table-column
        prop="name"
        label="Name"
        width="180">
      </el-table-column>
      <el-table-column
        prop="industry"
        label="Industry"
        width="180">
      </el-table-column>
      <el-table-column
        prop="size"
        label="Size">
      </el-table-column>
    </el-table>
  </div>
</template>
<script>
import Vue from 'vue'
import VueResource from 'vue-resource'
Vue.use(VueResource)
Vue.http.options.emulateJSON = true
const http=Vue.http
  export default {
      data() {
        return {
          tableData: []
        }
      },
      methods:{
          create(){
           http.get('http://localhost:5106/api/Company').then(response => {
                // get body data
                console.log(response);
                this.tableData = response.body.result;
            }, response => {
                // error callback
            });
            }
        }
    }
</script>