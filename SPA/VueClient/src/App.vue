<template>
<div id='app'>
   <el-container>
     <el-header class="header">
        
        <el-col :span="4" class="logo logo-width'">
          <h1>{{sysName}}</h1>
        </el-col>
        <el-col :span="20">
            <Menu></Menu>
        </el-col>
        
     </el-header>
     <el-container>
        <router-view/>
     </el-container>
  </el-container>
</div>
</template>
 
<script>
import Menu from './components/Menu'

export default {
  name: 'app',
  components: {
    'Menu': Menu
  },
  data(){
    return {
      sysName:"Vue Commerce"
    }
  }
}

</script>

