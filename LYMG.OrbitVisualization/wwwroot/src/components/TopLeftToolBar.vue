<template>
    <nav>
        <SignalR></SignalR>
        <ViewerSetting></ViewerSetting>
    </nav>
</template>
<script>
    import SignalR from "./Tools/SignalR.vue"
    import ViewerSetting from "./Tools/ViewerSetting.vue"
    export default {
        components: { SignalR, ViewerSetting }
    }
</script>
<style scoped>
    nav {
        position: absolute;
        top: 5px;
        left: 5px;
    }
</style>