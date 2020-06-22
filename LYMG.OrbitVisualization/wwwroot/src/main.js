import Vue from 'vue'
import App from './App.vue'
import router from './router'
import VueCesium from 'vue-cesium'

Vue.config.productionTip = false

Vue.use(VueCesium, {
    // cesiumPath 是指引用的Cesium.js路径
    cesiumPath: '/Cesium/Cesium.js',
    // 指定Cesium.Ion.defaultAccessToken，使用Cesium ion的数据源需要到https://cesium.com/ion/申请一个账户，获取Access Token。不指定的话可能导致 Cesium 在线影像加载不了
    accessToken: ''
})

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
