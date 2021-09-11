import "./style.css";
const useSignalRConnection = (callback) => {
  let connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/hubs/notification")
    .withAutomaticReconnect()
    .build();
  connection.on("NotifyAll", (info) => {
    callback(info);
  });
  connection.on("Notify", (info) => {
    callback(info);
  });
  connection.start().catch((err) => {
    console.log(`Connection start error ${err}`);
  });
};
const notification = {
  setup() {
    const notifications = Vue.ref([]);
    useSignalRConnection((info) => {
      notifications.value.push(info);
    });
    const clear = (id) => {
      notifications.value = notifications.value.filter((el) => el.id != id);
    };
    return { notifications, clear };
  },
};
Vue.createApp(notification).mount("#app");
