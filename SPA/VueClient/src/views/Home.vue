<template>
<div class="company">
  <el-row>
    <el-col :span="24">
      <h1 class="text-primary">Company List</h1>
    </el-col>
  </el-row>
  <el-row>
      <el-col :span="24">
        
        <el-table :data="tableData"
         v-loading="loading2"
       :default-sort = "{prop: 'name', order: 'descending'}"
         height="400"
         border
          style="width: 100%">
          <el-table-column
            prop="name"
            label="Name"
            width="180">
          </el-table-column>
          <el-table-column
            prop="size"
            label="Size">
          </el-table-column>
          <el-table-column
            prop="headquarter"
            label="Headquarter">
          </el-table-column>
              <el-table-column
            prop="revenue"
            label="Revenue">
          </el-table-column>
          <el-table-column
            prop="newIndustry"
            label="Industry">
             <template slot-scope="scope">
              <el-tag
                :type="scope.row.newIndustry === 'Finance' ? 'primary' : 'success'"
                disable-transitions>{{scope.row.newIndustry}}</el-tag>
            </template>
          </el-table-column>

           <el-table-column
              fixed="right"
              label="Action"
              width="100">
              <template slot-scope="scope">
                <el-button @click="handleClick(scope.row)" type="text" size="small">View</el-button>
                <el-button type="text" size="small">Edit</el-button>
              </template>
            </el-table-column>
        </el-table>
        
      </el-col>
  </el-row>
  <div class="pager-block">
    <el-row>
        <el-col :span="24">
          <el-pagination
            background
            layout="total,prev, pager, next"
            :total="total"
              @current-change="handleCurrentChange"
            >
          </el-pagination>
        </el-col>
      </el-row>
  </div>
  <el-dialog title="Company Details" :visible.sync="dialogFormVisible">
      <el-form :model="company">
        <el-form-item label="Name" :label-width="formLabelWidth">
          <el-input v-model="company.name" autocomplete="off"></el-input>
        </el-form-item>
     
      </el-form>
      <h3>Job Information: </h3>
         <el-table :data="company.jobs"
         height="400"
         border
          style="width: 100%">
          <el-table-column
            prop="title"
            label="Title"
            width="180">
          </el-table-column>
          <el-table-column
            prop="averageSalary"
            label="Average Salary">
          </el-table-column>
           <el-table-column
            prop="level"
            label="Level">
          </el-table-column> 
        </el-table>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="dialogFormVisible = false">确 定</el-button>
      </div>
    </el-dialog>
  </div>
 
</template>
 <style>
 .company{
  margin: 10px auto;
  width: 1000px;
 }
 .text-primary{
   color:#409EFF;
 }
 .pager-block{
   padding: 24px;
   text-align: right;
   padding-right: 0px;
 }
 </style>
 
<script>
import Vue from 'vue'
import VueResource from 'vue-resource'
Vue.use(VueResource)
const http=Vue.http
  export default {
      data() {
        return {
          tableData: [],
          currentPage:1,
          pageSize:10,
          total:0,
          loading2: true,
          company:{},
          dialogFormVisible: false,
          formLabelWidth: '120px'
        }
      },
      methods:{
         handleClick(row) {
            console.log(row);
            this.company = row;
            this.dialogFormVisible = true
          },
          create(){
             this.loading2=true;
              var url ='http://localhost:5106/api/Company/'+ this.currentPage.toString()+"/"+this.pageSize.toString();
              http.get(url).then(response => {
                  // get body data
                  console.log(response);
                  this.tableData = response.body.result.items;
                  this.total = response.body.result.total;
                  this.loading2 =false;
              }, response => {
                  // error callback
              });
            },
            handleCurrentChange(val) {
              console.log(`current page: ${val}`);
              this.currentPage= val;
              this.create();
            }
        },
      created:function(){
        this.create();
      }
    }
</script>
