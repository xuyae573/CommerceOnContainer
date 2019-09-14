<template>
    
    <el-menu :default-active="activeIndex" class="el-menu-custom" mode="horizontal" @select="handleSelect"
    router
    >
      <el-menu-item index="1" route="/">Company</el-menu-item>
      <el-submenu index="2">
        <template slot="title">Catalog</template>
        <el-menu-item index="2-1" route="/phone">Phone</el-menu-item>
      </el-submenu>
      <el-submenu index="3">
        <template slot="title">Rating</template>
        <el-menu-item index="3-1" route="/about">手机及周边</el-menu-item>
        <el-menu-item index="3-2">影音电器</el-menu-item>
        <el-menu-item index="3-3">数码设备</el-menu-item>
      </el-submenu>
    </el-menu>
 
</template>
<script>
    export default {
        name: 'Menu',
        data() {
            return {
                activeIndex: '1',
            };
        },
        methods: {
            handleSelect(key, keyPath) {
                console.log(key, keyPath);
                this.activeIndex = keyPath[-1];
            }
        }
    }
</script>