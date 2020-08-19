
    import * as signalR from "@microsoft/signalr";
    export const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44389/cesiumHub/")
    .withAutomaticReconnect()//默认情况下，它不会自动重新连接
    .build();
    