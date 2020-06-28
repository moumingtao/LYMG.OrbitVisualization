import Vue from 'vue'
import App from './App.vue'
import router from './router'
import VueCesium from 'vue-cesium'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import dataV from '@jiaminghi/data-view'

Vue.config.productionTip = false

Vue.use(VueCesium, {
    // cesiumPath ��ָ���õ�Cesium.js·��
    cesiumPath: '/Cesium/Cesium.js',
    // ָ��Cesium.Ion.defaultAccessToken��ʹ��Cesium ion������Դ��Ҫ��https://cesium.com/ion/����һ���˻�����ȡAccess Token����ָ���Ļ����ܵ��� Cesium ����Ӱ����ز���
    accessToken: ''
})
Vue.use(ElementUI);
Vue.use(dataV)

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
