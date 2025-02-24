let scanner = new Instascan.Scanner({ video: document.querySelector('#log-scan') });
let currentCamera = null;
$(function () {
    Instascan.Camera.getCameras().then(function (cameras) {
        if (cameras.length > 0) {
            currentCamera = cameras[0]; // Store the current camera
            scanner.start(currentCamera);
        } else {
            console.error('No cameras found.');
        }
    }).catch(function (e){
        console.error(e);
    });
    $("#btn-close-camera").on("click", function () {
        if (currentCamera) {
            scanner.stop(currentCamera); // Stop the current camera
            currentCamera = null; // Reset the current camera
            $("#scanner-container").addClass("d-none");
            $("#allow-camera-text").removeClass("d-none")
            $("#btn-open-camera").removeClass("d-none")
            $(this).addClass("d-none")
        }
    });
    $("#btn-open-camera").on("click", function () {
        Instascan.Camera.getCameras().then(function (cameras) {
            currentCamera = cameras[0]; // Store the current camera
            scanner.start(currentCamera);
            if (cameras.length > 0) {

            } else {
                console.error('No cameras found.');
            }
        }).catch(function (e) {
            console.error(e);
        });
        $("#scanner-container").removeClass("d-none");
        $("#allow-camera-text").addClass("d-none")
        $(this).addClass("d-none")
        $("#btn-close-camera").removeClass("d-none")
    });
})
