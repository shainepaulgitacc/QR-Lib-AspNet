$(function () {
    $("#btnSendStudentAttendance").on("click", function (e) {
        connection.invoke("SendStudentAttendance", "Shaine Paul", "01:00", "02:34")
            .catch(function (err) {
                console.log(err.toString());
            });
        e.preventDefault();
    })
})