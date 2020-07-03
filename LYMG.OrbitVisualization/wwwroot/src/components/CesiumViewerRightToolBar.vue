<template>
    <button class="cesium-button"
            v-on:focus="$el.innerText = 'focus'"
            v-on:blur="$el.innerText = 'blur'"
            >2334</button>
</template>
<script>
    export default {
        mounted() {
            
        }
    }
</script>