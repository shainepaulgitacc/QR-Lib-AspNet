var connection = new signalR.HubConnectionBuilder().withUrl('/basehub').build();
$(function () {
    connection.start().then(function () {
        console.log("success connection to hub");
    }).catch(function (err) {
        console.log(err);
    });
})