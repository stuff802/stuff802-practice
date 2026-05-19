$(function () {

    var captionText = $(".table-caption").text();

    var $allTables = $("table");

    $allTables.each(function (index, element) {

        var $table = $(element);

        var $tbody = $table.find("tbody");

        var $thead = $table.find("thead");

        //if there is no thead element, add one.
        //if there is one no need to go further

        if ($thead.length === 0) {

            var caption = "<caption class='c-table_caption u-sr-only'>" + captionText + "</caption>";

            var tableid = 'table' + index;

            $table.attr('id', tableid);

            var colCount = columnCount(tableid);

            //alert(colCount);

            thead = $("<thead></thead>").insertBefore($tbody);

            $table.attr('class', tableid + ' c-table responsive');

            $table = $("#" + tableid);

            $tbody.attr('class', 'c-table_body responsive');

            var $rows = $tbody.find("tr");
            $rows.attr('class', 'c-table_row responsive');

            var $cells = $tbody.find("td");
            $cells.attr('class', 'c-table_cell responsive');

            // in case when we change first row of table as header
            var $firstRowWhenHeader = $tbody.find("tr:first-child th");
            $firstRowWhenHeader.each(function () {
                $(this).replaceWith("<th scope= 'col' class='c-table_header u-w-1/5' >" + $(this).text() + '</th>');
            });
            //end revie it later
            var $firstRow = $tbody.find("tr:first-child td");

            $firstRow.each(function () {
                $(this).replaceWith("<th scope= 'col' class='c-table_header u-w-1/5' >" + $(this).text() + '</th>');
            });

            $thead = $table.find("thead");

            $thead.attr('class', 'c-table_head responsive');
            //$thead.attr('scope', 'col');
            $thead.append($rows[0]);

           //console.log($thead.innerHTML);

            $(caption).insertBefore($thead);

            //https://medium.com/allenhwkim/mobile-friendly-table-b0cb066dbc0e

            const tableEl = document.querySelector("." + tableid.toString());
            const thEls = tableEl.querySelectorAll('thead th');
            const tdLabels = Array.from(thEls).map(el => el.innerText);

            tableEl.querySelectorAll('tbody tr').forEach(tr => {
                Array.from(tr.children).forEach(
                    (td, ndx) => td.setAttribute('label', tdLabels[ndx]));
            });

            if (window.innerWidth < 421) {
                $table.removeClass("c-table");
                $table.removeAttr("style");
                $tbody.removeClass("c-table_body");
                $tbody.removeAttr("style");
                $rows.removeClass("c-table_row");
                $rows.removeAttr("style");
                $cells.removeClass("c-table_cell");
                $cells.removeAttr("style");
                $thead.removeClass("c-table_head");
            }

        }

    });

});

function columnCount(table_id) {
    var cellCount = $("#" + table_id + " tr td").length;
    var rowCount = $("#" + table_id + " tr").length;
    var colCount = cellCount / rowCount;
    return colCount;
}