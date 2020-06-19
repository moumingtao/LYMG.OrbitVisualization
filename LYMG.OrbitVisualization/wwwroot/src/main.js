import Vue from 'vue'
import App from './App.vue'
import router from './router'
import VueCesium from 'vue-cesium'

Vue.config.productionTip = false

Vue.use(VueCesium, {
    // cesiumPath ��ָ���õ�Cesium.js·������
    // ��Ŀ���ص�Cesium Build����vue��Ŀ��Ҫ��Cesium Build����staticĿ¼��
    // cesiumPath: /static/Cesium/Cesium.js
    // ��������Cesium Build����
    // cesiumPath: 'https://zouyaoji.top/vue-cesium/statics/Cesium/Cesium.js'
    // ��������SuperMap Cesium Build�����ڹٷ������϶��ο��������ģ���
    // cesiumPath: 'https://zouyaoji.top/vue-cesium/statics/SuperMapCesium/Cesium.js'
    // �ٷ�����Cesium Build������CDN���٣��Ƽ��������
    cesiumPath: 'https://unpkg.com/cesium/Build/Cesium/Cesium.js',
    // ָ��Cesium.Ion.defaultAccessToken��ʹ��Cesium ion������Դ��Ҫ��https://cesium.com/ion/����һ���˻�����ȡAccess Token����ָ���Ļ����ܵ��� Cesium ����Ӱ����ز���
    accessToken: ''
})

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
