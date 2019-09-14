import Vue from 'vue'
import Router from 'vue-router'
//import Home from './views/Home.vue'
import Country from './views/Countries.vue';

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Country
    },
    {
      path: '/phone',
      name: 'phone',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/Phone.vue')
    },
    {
      path: '/company',
      name: 'company',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ './views/Company.vue')
    }
  ]
})
