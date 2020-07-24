import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
    {
        path: '/cesium/:name',
        component: () => import('@/views/Cesium.vue')
    },
    {
        path: '/Home',
        component: () => import('@/views/Home.vue')
    }
]

const router = new VueRouter({
  routes
})

export default router
