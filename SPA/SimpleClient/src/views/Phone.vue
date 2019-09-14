<template>
<div class="company">
  <div class="action_header">
  
    <el-row>
      <el-col :span="24">
        <span class="text-primary">Phone List (Redis Cached)</span>
      </el-col>
     
    </el-row>

  </div>
  <div class="actions_section">
    <el-row>
      <el-col :span="8">
        <el-form>
              <el-form-item>
                    <el-input autocomplete="off" placeholder="Search" v-model="state1">
                    <el-button slot="append" icon="el-icon-search"></el-button>
                </el-input>
              </el-form-item>
        </el-form>
      </el-col>
      <el-col :span="5" style="margin-left:15px;">
           <el-button @click="addNewPhone" type="primary" ><i class="el-icon-edit"></i>
               New</el-button>
      </el-col>
    </el-row>
  </div>

  <el-row>
      <el-col :span="24">
        <el-table :data="tableData"
         v-loading="loading2"
         :default-sort = "{prop: 'asin', order: 'ascending'}"
         height="400"
         border
          style="width: 100%"
          @sort-change="handleSortChange"
          >
          <el-table-column
             sortable
            prop="asin"
            label="ASIN"
            width="120px">
          </el-table-column>
          <el-table-column
             sortable
            prop="name"
            label="Name">
             <template slot-scope="scope">
              <a :href="scope.row.phoneUrl">{{scope.row.name}}</a>
            </template>
          </el-table-column>
          <el-table-column
              fixed="right"
              label="Action"
              width="100">
              <template slot-scope="scope">
                
               <el-button @click="handleClick(scope.row)" type="success" plain>Edit</el-button>
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
     
    <div>
      <el-dialog :title="dialogTitle" :visible.sync="dialogFormVisible">
            <el-form :model="phone">
              
              <el-form-item label="Name" :label-width="formLabelWidth">
                <el-input v-model="phone.name" autocomplete="off"></el-input>
              </el-form-item>
              <el-form-item label="ASIN" :label-width="formLabelWidth">
                <el-input v-model="phone.asin" autocomplete="off"></el-input>
              </el-form-item>
              <el-form-item label="Url" :label-width="formLabelWidth">
                <el-input v-model="phone.phoneUrl" autocomplete="off"></el-input>
              </el-form-item>
            
            </el-form>
            <div slot="footer" class="dialog-footer">
              <el-button @click="dialogFormVisible = false">Cancel</el-button>
              <el-button type="primary" @click="submitAddRequest">Save</el-button>
            </div>
      </el-dialog>
    </div> 
  </div>

</template>
 <style>
 .company{
  margin: 10px auto;
  width: 1000px;
 }
 
 .action_header,.actions_section{
   margin: 10px 0 0 0;
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
import _ from 'lodash'

Vue.use(VueResource)
 Vue.http.options.emulateJSON = false;
const http=Vue.http
  export default {
      data() {
        return {
          tableData: [],
          currentPage:1,
          pageSize:10,
          total:0,
          loading2: true,
          sortCol:"asin",
          order:"ascending",
          phone:{},
          dialogFormVisible: false,
          formLabelWidth: '120px',
          dialogTitle:"Add New Phone Details",
          state1: "",
          keyword:""
        }
      },
      watch:{
        state1: function(val){
            this.filterResult();
        }
      },
      methods:{
         filterResult :_.debounce(function(){
              console.log("New Filter:"+ this.state1);
              this.keyword = this.state1;
              this.create();
          },1000),
        
          handleSortChange(sender){
              console.log(sender)
              if(sender.prop!==null){
                this.sortCol = sender.prop;
              }
              if(sender.order!= null){
                this.order =  sender.order;
              }
              this.create();
          },
         handleClick(row) {
            console.log(row);
            this.phone = JSON.parse(JSON.stringify(row));
            this.dialogTitle = "Edit Phone Details";
            this.dialogFormVisible = true
          },
          create(){
             this.loading2=true;
          
              var url ='http://localhost:5106/api/Products/'+ this.currentPage.toString()+"/"+this.pageSize.toString()
              +"/"+this.sortCol+"/"+this.order+"?q="+this.keyword;
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
            },
            addNewPhone(){
                this.phone = {
                    id:"",
                    name:"",
                    asin:"",
                    url:""
                };
                this.dialogTitle ="Add New Phone Details";
                this.dialogFormVisible = true
            },
            submitAddRequest(){
             
              var url ='http://localhost:5106/api/Products/create';
              if(this.phone.id.length >0){
                console.log(this.phone.id);
                var saveUrl ='http://localhost:5106/api/Products/update';
                url= saveUrl;
              }
              console.log(this.phone);
              http.post(url,this.phone).then(response => {
                  // get body data
                  console.log(response);
                   this.create();
                   this.dialogFormVisible = false;
              }, response => {
                  // error callback
              });
            }
        },
      created:function(){
         //this.create();
      }
    }
</script>
