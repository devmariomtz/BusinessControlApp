function filterTable() {
    $('#items').DataTable().draw();
}

$(document).ready(function () {

    $('#items').DataTable();
    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) { //'data' contiene los datos de la fila
            //En la columna 4 estamos mostrando la categoria
            let categoryColumnData = data[4] || 0;
            if (!filterByCategory(categoryColumnData)) {
                return false;
            }
            return true;
        }
    );
});

function filterByCategory(categoryColumnData) {
    let categorySelected = $('#categoryFilter').val();
    //Si la opción seleccionada es 'TODOS', devolvemos 'true' para que pinte la fila
    if (categorySelected === "all") {
        return true;
    }
    // obtener el texto del select y no el valor
    categorySelected = $('#categoryFilter option:selected').text();
    //La fila sólo se va a pintar si el valor de la columna coincide con el del filtro seleccionado
    return categoryColumnData === categorySelected;
}





