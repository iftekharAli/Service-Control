var gridBL;
function Search() {
    gridBL.reload({ searchString: $("#txtSearchBL").val() });

}

function AutoReloadBL() {

    gridBL.reload();
}

window.setInterval(AutoReloadBL, 7000);

$(document).ready(function () {
    gridBL = $("#gvBanglalink").grid({
        dataKey: "RowID",
        uiLibrary: "bootstrap",
        columns: [
            { field: "Processing", sortable: true },
            { field: "Sent", sortable: true },
            { field: "Service", sortable: true }
        ],
        pager: { enable: false, limit: 5, sizes: [2, 5, 10, 20] }

    });
    $("#btnSearchBL").on("click", Search);
});