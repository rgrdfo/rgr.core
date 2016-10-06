/*ymaps.ready(function () {
    var myMap = new ymaps.Map('map', {
        center: [59.22, 39.89],
        zoom: 12,
        controls: []
    }),
    // Создаем экземпляр класса ymaps.control.SearchControl
        mySearchControl = new ymaps.control.SearchControl({
            options: {
                noPlacemark: true
            }
        }),
    // Результаты поиска будем помещать в коллекцию.
        mySearchResults = new ymaps.GeoObjectCollection(null, {
            hintContentLayout: ymaps.templateLayoutFactory.createClass('$[properties.name]')
        });
    myMap.controls.add(mySearchControl);
    myMap.geoObjects.add(mySearchResults);
    // При клике по найденному объекту метка становится красной.
    mySearchResults.events.add('click', function (e) {
        e.get('target').options.set('preset', 'islands#redIcon');
    });
    // Выбранный результат помещаем в коллекцию.
    mySearchControl.events.add('resultselect', function (e) {
        var index = e.get('index');
        mySearchControl.getResult(index).then(function (res) {
            mySearchResults.add(res);
        });
    }).add('submit', function () {
        mySearchResults.removeAll();
    })
});*/

ymaps.ready(init);
var myMap,
    myPlacemark;

function init() {
    myMap = new ymaps.Map("map", {
        center: [48.48888324, 135.08322577],
        zoom: 10
    });

    myPlacemark = new ymaps.Placemark([48.48888324, 135.08322577], {
        hintContent: 'РГР ДФО',
        balloonContent: 'ул. Карла Маркса, 65-102А'
    });

    myMap.geoObjects.add(myPlacemark);
}