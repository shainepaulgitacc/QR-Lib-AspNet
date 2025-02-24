function checkTime() {
    var currentTime = new Date();
    var hours = currentTime.getHours();
    var minutes = currentTime.getMinutes();

    // Check if the current time is 2:40 PM
    if (hours === 15 && minutes === 10) {
        // Show a notification
        alert("It is time!");
    }
}
checkTime();
// Check the time every 1 minute
setInterval(checkTime, 60000);
        // Initial check when the page load