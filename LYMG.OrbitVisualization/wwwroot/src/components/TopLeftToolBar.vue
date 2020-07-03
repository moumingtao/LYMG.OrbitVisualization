<template>
    <nav>
        <SignalR></SignalR>
    </nav>
</template>
<script>
    import SignalR from "./Tools/SignalR.vue"
    export default {
        components: { SignalR }
    }
</script>
<style scoped>
    nav {
        position: absolute;
        top: 5px;
        left: 5px;
    }
</style>