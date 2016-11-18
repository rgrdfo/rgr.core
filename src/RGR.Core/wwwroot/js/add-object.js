//Yandex map
    ymaps.ready(init);
    function init(){
        var map = new ymaps.Map('map-block', {
            center: [48.48272, 135.08379],
            zoom: 10
        });

        myPlacemark = new ymaps.Placemark([48.48888324, 135.08322577], {
            hintContent: 'РГР ДФО',
            balloonContent: 'ул. Карла Маркса, 65-102А'
        });

        myMap.geoObjects.add(myPlacemark);
    }
   

//Change color in textbox (id add-adress)    
        //function change() {
        //    document.getElementById("add-adress").style.color = "#559abd";
        //}

