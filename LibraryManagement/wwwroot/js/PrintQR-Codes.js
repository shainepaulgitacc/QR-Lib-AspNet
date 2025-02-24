$(function () {
    $('#btn-export-pdf').on('click', function () {
        // Temporarily remove elements with class 'download-one'
        $(".download-one").hide();

        // Call the function to generate PDF
        downloadAsPDF();

        // Restore the elements after PDF generation
        //$(".download-one").show();
    });
});
function downloadAsPDF() {
    // Get the content of the 'qr-codes' div
    var content = document.getElementById('qr-codes');

    // Use html2pdf library to generate PDF
    html2pdf(content, {
        margin: 0,
        filename: 'downloads.pdf',
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
    });
}