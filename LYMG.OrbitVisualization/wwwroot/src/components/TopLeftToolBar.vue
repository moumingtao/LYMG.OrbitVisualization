<template>
    <nav>
        <SignalR></SignalR>
        <ViewerSetting></ViewerSetting>
        <Scene></Scene>
        <Satellite></Satellite>
        <GroundStation></GroundStation>
        <TargetDetection></TargetDetection>
    </nav>
</template>
<script>
    import SignalR from "./Tools/SignalR.vue"
    import ViewerSetting from "./Tools/ViewerSetting.vue"
    import Satellite from "./Tools/Satellite.vue"
    import GroundStation from "./Tools/GroundStation.vue"
    import Scene from "./Tools/Scene.vue"
    import TargetDetection from "./Tools/TargetDetection.vue"
    export default {
        components: { SignalR, ViewerSetting, Satellite, GroundStation, Scene, TargetDetection }
    }
</script>
<style scoped>
    nav {
        position: absolute;
        top: 5px;
        left: 5px;
    }
</style>