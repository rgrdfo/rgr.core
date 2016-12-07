
ymaps.ready(showDistrictSelectionDialog);

//@* Диалог выбора районов *@
function showDistrictSelectionDialog() {
    var districtsYandexMap = new ymaps.Map("select-districts-dialog", {
        center: [48.48888324, 135.08322577],
        zoom: 10
    });
    var yandexMap = districtsYandexMap;
    yandexMap.geoObjects.each(function (obj) {
        yandexMap.geoObjects.remove(obj);
    });
    var selectedDistricts = parseIdsStr($("#districtIds-hidden").val());
    $.ajax({
        type: 'POST',
        url: '/search/districts',
        data: {
            cityId: $("#city-id-field").val()
        },
        success: function (data) {
            $.each(data.districts, function (index, item) {
                // Создаем объект геометрии
                var geometry = {
                    s,
                        type: 'Polygon',
                        coordinates: item.coordinates
                    };
                    var options = {
                        strokeWidth: 3,
                        strokeColor: '#0000FF',
                        fillColor: '#FFFF00',
                        fillOpacity: 0.3,
                        draggable: false,
                        hasHint: true
                    };
                    var polygon = new ymaps.GeoObject({geometry: geometry}, options);
                polygon.name = item.name;
                polygon.districtName = item.name;
                polygon.districtId = item.id;
                polygon.selected = false;
                for (var i = 0; i < selectedDistricts.length; i++) {
                    var selId = selectedDistricts[i];
                    if (selId == item.id) {
                        polygon.selected = true;
                        break;
                    }
                }
                polygon.events.add("click", function (event) {
                    if (!event.originalEvent.target.selected) {
                        event.originalEvent.target.options.set("fillColor", "#FF0000");
                        event.originalEvent.target.selected = true;
                    } else {
                        event.originalEvent.target.options.set("fillColor", "#0275BE");
                        event.originalEvent.target.selected = false;
                    }
                    updateSelectedDistricts();
                });
                yandexMap.geoObjects.add(polygon);
                updateSelectedDistricts();
            });
        },
        error: function () {

        },
        dataType: 'json'
    });
    var dialog = $("#select-districts-dialog").dialog({
        autoOpen: true,
        resizable: false,
        modal: true,
        width: 625,
        buttons: {
            "Выбрать": function () {
                var idsStr = "";
                var idsArray = [];
                yandexMap.geoObjects.each(function (obj) {
                    if (obj.selected) {
                        idsStr += obj.districtId + ",";
                        idsArray.push(obj.districtId);
                    }
                });
                $("#districtIds-hidden").val(idsStr);
                $("#district-names-field").val(idsArray).change().multiselect("refresh");
                dialog.dialog("close");
            },
            "Отмена": function () {
                dialog.dialog("close");
            }
        },
        open: function () {
            setTimeout(function () {
                yandexMap.container.fitToViewport();
                yandexMap.setBounds(yandexMap.geoObjects.getBounds());
            }, 1000);

        }
    });
}
    //@* Обновляет выбранные районы *@
    function updateSelectedDistricts() {
        var selectedNames = "";
        districtsYandexMap.geoObjects.each(function(obj) {
            if (obj.selected) {
                selectedNames += obj.districtName + ", ";
                obj.options.set("fillColor", "#FF0000");
            }
        });
        $("#selected-districts").text(selectedNames);
    }

    //@* Обновляет выбранные жил массивы *@
    function updateSelectedAreas() {
        var selectedNames = "";
        areasYandexMap.geoObjects.each(function(obj) {
            if (obj.selected) {
                selectedNames += obj.areaName + ", ";
                obj.options.set("fillColor", "#FF0000");
            }
        });
        $("#selected-areas").text(selectedNames);
    }

//@* Парсит строку идентификаторов и возвращает ее в виде массива *@
function parseIdsStr(str) {
    var parts = str.split(',');
    var result = [];
    $.each(parts, function (index, item) {
        if (item != "") {
            result.push(parseInt(item));
        }
    });
    return result;
}

//@* Байндим изменение города *@
$("#city-id-field").change(function () {
    var val = $(this).val();
    $("#areas-fields-wrapper").hide();
    if (val == -1) {
        $("#district-fields-wrapper").hide();
        $("#street-fields-wrapper").hide();
    } else {
        $("#district-fields-wrapper").show().find("#district-names-field").remove();
        $("#district-fields-wrapper").find(".ui-multiselect").remove();
        $.ajax({
            type: 'GET',
            url: '/search/districts-selector',
            data: {
                cityId: val
            },
            dataType: 'html',
            success: function (responseText) {
                $("#districtIds-hidden").val("").after(responseText);
                $("#district-names-field").multiselect({
                    noneSelectedText: 'Выберите значения из списка',
                    selectedList: 4,
                    maxWidth: 317
                });
            },
            error: function () {
                alert("Ошибка на сервере. Обновите страницу");
            }
        });
    }
});
//@* Байндим изменение района *@
$("#district-names-field").live("change", function () {
    var val = $(this).val();
    var ids = "";
    if (val == null) {
        val = [];
    }
    $.each(val, function (index, item) {
        ids += item + ",";
    });
    $("#districtIds-hidden").val(ids);
    $("#area-ids-hidden").val("");
    if (val != null && val.length == 1) {
        $("#areas-fields-wrapper").show();
        $("#area-names-field").remove();
        $("#areas-fields-wrapper").find(".ui-multiselect").remove();
        $.ajax({
            type: 'GET',
            url: '/search/areas-selector',
            data: {
                districtId: val[0]
            },
            dataType: 'html',
            success: function (responseText) {
                $("#area-ids-hidden").val("").after(responseText);
                $("#area-names-field").multiselect({
                    noneSelectedText: 'Выберите значения из списка',
                    selectedList: 4,
                    maxWidth: 317
                });
            },
            error: function () {
                alert("Ошибка на сервере. Обновите страницу");
            }
        });
    } else {
        $("#areas-fields-wrapper").hide();
        $("#street-fields-wrapper").hide();
    }
});
//@* Байндинг селектора массивов *@
$("#area-names-field").live("change", function () {
    var val = $(this).val();
    var ids = "";
    if (val == null) {
        val = [];
    }
    $.each(val, function (index, item) {
        ids += item + ",";
    });
    $("#area-ids-hidden").val(ids);
    $("#street-ids-hidden").val("");
    if (val != null && val.length > 0) {
        $("#street-fields-wrapper").show();
        $("#street-names-field").remove();
        $("#street-fields-wrapper").find(".ui-multiselect").remove();
        $.ajax({
            type: 'GET',
            url: '/search/streets-selector',
            data: {
                areaIds: ids
            },
            dataType: 'html',
            success: function (responseText) {
                $("#street-ids-hidden").val("").after(responseText);
                $("#street-names-field").multiselect({
                    noneSelectedText: 'Выберите значения из списка',
                    selectedList: 12,
                    maxWidth: 700
                });
            },
            error: function () {
                alert("Ошибка на сервере. Обновите страницу");
            }
        });
    } else {
        $("#street-fields-wrapper").hide();
    }
});
//@* Байндинг селектора массивов *@
$("#street-names-field").live("change", function () {
    var val = $(this).val();
    var ids = "";
    $.each(val, function (index, item) {
        ids += item + ",";
    });
    $("#street-ids-hidden").val(ids);
});

//@* Обработка добавления *@
$("#add-criteria-action").click(function () {
    showAddCriteriaDialog();
    return false;
});

//@* Обработка удаления критериев *@
$("#delete-criterias-action").click(function () {
    var ids = getSelectedTableItems();
    $.each(ids, function (index, item) {
        var selector = "#criterias-table tbody tr[name='" + item + "']";
        $(selector).remove();
    });
    updateCriteriasTable();
    return false;
});

//@* Сохранение поиска *@
    $("#save-search-action").click(function() {
        showSaveSearchDialog();
        return false;
    });
    
    //@* Восстановлене поиска  *@
    $("#load-search-action").click(function() {
        showLoadSearchDialog();
        return false;
    });
    
    //@* Редактирование районов *@
    $("#district-ids-field").live("click", function() {
        showDistrictSelectionDialog();
    });
    
    //@* Редактирование массивов *@
    $("#area-ids-field").live("click", function() {
        showAreaSelectionDialog();
    });