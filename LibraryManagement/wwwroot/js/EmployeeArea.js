scanner.addListener('scan', function (qr_data) {
    if (qr_data != null) {
        $("#emp-id").val(qr_data);
        $("#emplog-form").submit();
    }
});