scanner.addListener('scan', function (qr_data) {
    if (qr_data != null) {
        $("#student-id").val(qr_data);
        $("#form-log-scan").submit();
    }
});

let barrowScanner = new Instascan.Scanner({ video: document.querySelector('#barrow-scan') });
$("#barrow-btn").on("click", function () {
    Instascan.Camera.getCameras().then(function (cameras) {
        if (cameras.length > 0) {
            scanner.stop(currentCamera); // Stop the current camera
           $("#log-scan").addClass("d-none")
            currentCamera = cameras[0]; // Store the current camera
            barrowScanner.start(currentCamera);
        } else {
            console.error('No cameras found.');
        }
    }).catch(function (e) {
        console.error(e);
    });
})
$("#exit-barrow-btn").on("click", function () {
    Instascan.Camera.getCameras().then(function (cameras) {
        
        barrowScanner.stop(currentCamera);
        $("#log-scan").removeClass("d-none")
        currentCamera = cameras[0]; // Store the current camera
        scanner.start(currentCamera); // Start the camera 
    }).catch(function (e) {
        console.error(e);
    });
})
barrowScanner.addListener('scan', function (qr_data) {
    if (qr_data != null) {
        $("#user-barrow-id").val(qr_data);
        $("#borrow-scan-form").submit();
        $("#barrow-modal-qr").modal("hide");
    }
});