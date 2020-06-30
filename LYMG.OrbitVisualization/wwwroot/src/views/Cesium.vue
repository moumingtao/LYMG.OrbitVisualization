<template>
    <CesiumViewer :name="$route.params.name"></CesiumViewer>
</template>
<script>
    import CesiumViewer from '@/components/CesiumViewer.vue'
    export default {
        components: { CesiumViewer }
    }
</script>