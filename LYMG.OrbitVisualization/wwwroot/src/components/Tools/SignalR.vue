<template>
    <!--https://docs.microsoft.com/zh-cn/aspnet/core/signalr/javascript-client?view=aspnetcore-3.1#automatically-reconnect-->
    <el-badge value="自动重连" :hidden="connection.connectionState != 'Reconnecting'">
        <el-popover placement="bottom" title="SignalR连接" width="300" trigger="hover">
            <button class="cesium-button cesium-toolbar-button signalR" v-bind:class="{connected}" slot="reference">
                <svg class="cesium-svgPath-svg" viewBox="0 0 1024 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="8604">
                    <path d="M92 92m4 0l832 0q4 0 4 4l0 832q0 4-4 4l-832 0q-4 0-4-4l0-832q0-4 4-4Z" class="bg"></path>
                    <path d="M920 104v816H104V104h816m8-24H96a16 16 0 0 0-16 16v832a16 16 0 0 0 16 16h832a16 16 0 0 0 16-16V96a16 16 0 0 0-16-16z" fill="#333333" p-id="8606"></path>
                    <path d="M315.36 794.96V704a12 12 0 0 0-12-12H232V416.8h561.84v274.96h-72a12 12 0 0 0-12 12v91.2z" fill="#929FFF" p-id="8607"></path>
                    <path d="M781.84 428.8V680h-59.68a24 24 0 0 0-24 24v79.2H327.36V704a24 24 0 0 0-24-24h-59.68V428.8h538.16m24-24H219.68V704h83.68v103.2h418.8V704h83.68V404.8z" fill="#333333" p-id="8608"></path>
                    <path d="M320 597.84l6.88-9.44a31.2 31.2 0 0 0 19.92 8c8.48 0 12.4-4.08 12.4-9.44s-8-9.2-16-12.16c-9.36-3.6-20.08-8.56-20.08-20.48s9.44-20.64 24.96-20.64a37.04 37.04 0 0 1 22.88 8l-6.88 9.12a26.32 26.32 0 0 0-16-6.16c-8 0-11.28 3.84-11.28 8.72s7.28 8 14.96 11.12c9.84 3.68 20.72 8 20.72 21.36 0 11.76-9.36 21.36-26.72 21.36A43.28 43.28 0 0 1 320 597.84zM384 570.8c0-23.52 16-37.12 33.12-37.12s33.12 13.6 33.12 37.12-16 36.72-33.12 36.72S384 594.08 384 570.8z m51.2 0c0-14.96-6.88-24.96-18-24.96s-17.92 10-17.92 24.96 6.88 24.72 17.92 24.72 17.84-9.84 17.84-24.72zM462.4 570.8a34.56 34.56 0 0 1 34.8-37.12 30.24 30.24 0 0 1 20.56 8l-7.28 9.6a18.4 18.4 0 0 0-12.64-5.52c-11.76 0-20.24 10-20.24 24.96s8 24.72 19.84 24.72a24 24 0 0 0 15.36-6.4l6 9.68a34.32 34.32 0 0 1-22.8 8.8c-18.96 0-33.6-13.44-33.6-36.72zM533.52 504h14.32v65.76L576 535.52h16l-24 28.48 26.64 41.84h-16l-19.04-32-11.92 13.68v18.16h-14.16zM598.96 570.8c0-22.88 16-37.12 32-37.12 18.64 0 28.64 13.44 28.64 33.36a38.08 38.08 0 0 1-0.64 7.44h-45.52a21.28 21.28 0 0 0 21.76 21.52 29.84 29.84 0 0 0 16.8-5.28l5.04 9.28a42.72 42.72 0 0 1-24 7.44 33.84 33.84 0 0 1-34.08-36.64z m48-6.4c0-12.16-5.52-19.2-16-19.2a19.12 19.12 0 0 0-18 19.2zM677.12 582.88v-35.6h-10.24V536l10.88-0.8 1.68-19.44h12.4v19.44h18.32v11.76h-18.32v35.68c0 8.48 2.96 12.8 10.24 12.8a21.04 21.04 0 0 0 7.52-1.68l2.4 11.2a44.96 44.96 0 0 1-13.84 2.56c-15.52 0-21.04-9.84-21.04-24.64z"></path>
                    <path d="M273.36 210.8h478.88V416H273.36z" class="bg"></path>
                    <path d="M740.24 222.8V404H285.36V222.8h454.88m24-24H261.36V428h502.88V198.8z" fill="#333333" p-id="8611"></path>
                    <path d="M345.04 270.08h24v136.16h-24zM447.92 270.08h24v136.16h-24zM550.8 270.08h24v136.16h-24zM653.6 270.08h24v136.16h-24z" fill="#333333" p-id="8612"></path>
                </svg>
            </button>
            <section>
                连接状态：{{connection.connectionState}}
                <el-button :type="connected ? 'info' : 'primary'" size="mini" @click="connect()">重新连接</el-button>
            </section>
        </el-popover>
    </el-badge>
</template>
<script>
    import * as signalR from "@microsoft/signalr";
    export default {
        inject: ["cesiumVue"],
        created() {
            // 配置SignalR连接
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl("https://localhost:44389/cesiumHub/")
                .withAutomaticReconnect()//默认情况下，它不会自动重新连接
                .build();

            // 配置SignalR回调
            this.connection.on("eval", (cid, script, gs) => {
                var ret = eval(script);
                if (cid) {
                    if (ret && ret.then)
                        ret.then(res => this.connection.send("ViewerResponse", cid, res));
                    else
                        this.connection.send("ViewerResponse", cid, ret);
                }
                if (gs) { return; }
            });

            this.connect();
        }, data() {
            return {
                connection: null,
                svgStyle: {
                    fill:"red"
                },
            }
        }, computed: {
            connected() { return this.connection.connectionState == 'Connected'; }
        }, methods: {
            async connect() { // 连接SignalR服务
                await this.connection.start();
                this.connection.send("ViewerEnter", this.cesiumVue.name);
            }
        }
    }
</script>
<style>
    button.signalR {
        fill: pink;
    }
    button.signalR.connected {
        fill:#9FC;
    }
</style>