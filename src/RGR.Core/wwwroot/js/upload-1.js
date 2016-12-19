$(document).ready(function () {
    var dropZone = $('#add-photo'),
        maxFileSize = 1048576; // максимальный размер файла - 5 мб.
    var maxFiles = 20; // максимальное количество - 20 шт.
    var dataArray = [];


    //Проверка поддержки браузером Drag and Drop
    if (typeof (window.FileReader) == 'undefined') {
        dropZone.text('Не поддерживается браузером!');
        dropZone.addClass('error');
    }

    //Анимация эффекта перетаскивания файла
    dropZone[0].ondragover = function () {
        dropZone.addClass('hover');
        return false;
    };

    dropZone[0].ondragleave = function () {
        dropZone.removeClass('hover');
        return false;
    };

    //Обработчик события drop
    dropZone[0].ondrop = function (event) {
        dropZone.removeClass('hover');
        dropZone.addClass('drop');
        var files = event.dataTransfer.files;
        event.preventDefault();

        if (files.length <= maxFiles) {
            // Передаем массив с файлами в функцию загрузки на предпросмотр
            var file = event.dataTransfer.files[0];

            if (file.size > maxFileSize) {
                dropZone.text('Файл слишком большой!');
                dropZone.addClass('error');
                return false;
            }
            else {
                // Передаем массив с файлами в функцию загрузки на предпросмотр
                addImage(files);
                // И отправляем их на сервер
                send(files);
            }           

        } else {
            alert('Вы не можете загружать больше ' + maxFiles + ' изображений!');
            files.length = 0;
        }
        //Проверка размера
        
    };

    function addImage (files) {
        for (i = files.length - 1; i >= 0; i--) {        
            var src = URL.createObjectURL(files[i]);
            $('#photo-row').append('<div id="img-' + files[i] + '" class="col-lg-3" style="background:url(' + src + ') no-repeat; background-size:cover;"></div>');        
            
        }
    }

    //отправка файлов на сервер
    function send(files) {
        var data = new FormData();
        for (var i = 0; i < files.length; i++) {
            data.append(files[i].name, files[i])
        }

        $.ajax({
            type: "POST",
            url: "/Storage/UploadMedia",
            contentType: false,
            processData: false,
            success: function (message) {
                alert(message);
            },
            error: function () {
                alert("Ошибка загрузки файла!");
            }
        })
    }
});