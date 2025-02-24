/*$("#btn-export-excel").on("click", function () {
    var table2excel = new Table2Excel();

    // Make a copy of the table for exporting
    var exportTableCopy = document.querySelector("table.table").cloneNode(true);

    // Exclude last column (action column) in the copied table
    var excludedColumnIndex = exportTableCopy.rows[0].cells.length - 1;
    for (var i = 0; i < exportTableCopy.rows.length; i++) {
        exportTableCopy.rows[i].deleteCell(excludedColumnIndex);
    }

    table2excel.export([exportTableCopy]);
});

*/

$("#btn-export-excel").on("click", function () {
    var table2excel = new Table2Excel({
        filename: "exported_data",
        sheet: {
            name: "Sheet 1",
            autoSize: true // Set autoSize to true for automatic column width adjustment
        }
    });

    // Make a copy of the table for exporting
    var exportTableCopy = document.querySelector("table.table").cloneNode(true);

    // Exclude last column (action column) in the copied table
    var excludedColumnIndex = exportTableCopy.rows[0].cells.length - 1;
    for (var i = 0; i < exportTableCopy.rows.length; i++) {
        exportTableCopy.rows[i].deleteCell(excludedColumnIndex);
    }

    table2excel.export([exportTableCopy]);
});
