var gridRobi;

function Search() {
    grid.reload({ searchString: $("#txtSearchRobi").val() });

}

function AutoReload() {
    grid.reload();
    grid.parent().find("div[data-role='loading-cover']").remove();
    grid.parent().find("div[data-role='loading-text']").remove();

    alert("HEllo");
}

window.setInterval(AutoReload, 5000);

$(document).ready(function () {
    gridRobi = $("#gvRobi").grid({
        dataKey: "RowID",
        uiLibrary: "bootstrap",
        columns: [
            { field: "Processing", sortable: true },
            { field: "Port", sortable: true },
            { field: "Status", sortable: true }
        ],
        pager: { enable: true, limit: 5, sizes: [2, 5, 10, 20] }

    });
    $("#btnSearchRobi").on("click", Search);    
});