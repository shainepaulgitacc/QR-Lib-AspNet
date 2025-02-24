$(function () {
    connection.on("ReceiveStAttendance", function (name, timeIn, timeOut,userType, countUserLogToday) {
        $("#stAttendanceTable tbody").append($(`<tr>
           <td>${name}</td>
            <td>${userType}</td>
            <td>${timeIn} </td>
            <td>${timeOut}</td>
        </tr>`));
        $("#countUserLogToday").text(countUserLogToday);
    })
    connection.on("ReceiveEmpAttendance", function (name, timeIn, timeOut, countEmpAttendanceToday) {
        $("#empAttendanceTable tbody").append($(`<tr>
           <td>${name}</td>
            <td>${timeIn} </td>
            <td>${timeOut}</td>
        </tr>`));
        $("#countEmpAttendanceToday").text(countEmpAttendanceToday);
    })
})