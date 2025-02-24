$("#btn-export-excel").on("click", function () {
    var table2excel = new Table2Excel();

    // Make a copy of the table for exporting
    var exportTableCopy = document.querySelector("table.table").cloneNode(true);

    table2excel.export([exportTableCopy]);
});
