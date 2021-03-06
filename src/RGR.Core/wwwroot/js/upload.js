﻿$(document).ready(function () {
    var dropZone = $('#add-photo');
    var maxFileSize = 1048576; // максимальный размер файла - 1 мб.
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
        event.preventDefault();
        dropZone.removeClass('hover');
        dropZone.addClass('drop');

        // Проверка на максимальное количество файлов
        var files = event.dataTransfer.files;
    
        if (files.length <= maxFiles) {
            // Передаем массив с файлами в функцию загрузки на предпросмотр
            loadInView(files);
        } else {
            alert('Вы не можете загружать больше ' + maxFiles + ' изображений!');
            files.length = 0; return;
        }

        //Проверка размера
        var file = event.dataTransfer.files[0];
        
        if (file.size > maxFileSize) {
            dropZone.text('Файл слишком большой!');
            dropZone.addClass('error');
            return false;
        }
        else {
            // Передаем массив с файлами в функцию загрузки на предпросмотр
            loadInView(files);
        }
    };
    // При нажатии на кнопку выбора файлов
    defaultUploadBtn.on('add-but', function () {
        // Заполняем массив выбранными изображениями
        var files = $(this)[0].files;
        // Проверяем на максимальное количество файлов
        if (files.length <= maxFiles) {
            // Передаем массив с файлами в функцию загрузки на предпросмотр
            loadInView(files);        

        } else {
            alert('Вы не можете загружать больше ' + maxFiles + ' изображений!');
            files.length = 0;
        }
        //Проверка размера
        var file = event.dataTransfer.files[0];

        if (file.size > maxFileSize) {
            dropZone.text('Файл слишком большой!');
            dropZone.addClass('error');
            return false;
        }
        else {
            // Передаем массив с файлами в функцию загрузки на предпросмотр
            loadInView(files);            
        }
    });
    
    // Функция загрузки изображений на предпросмотр
    function loadInView(files) {
        if (files.length <= maxFiles) {
            $('#photo-row').css({ 'display': 'block' });
        }
        else { alert('Вы не можете загружать больше ' + maxFiles + ' изображений!'); return; }

        // Для каждого файла
        $.each(files, function(index,file) { 
            // Создаем новый экземпляра FileReader
            var fileReader = new FileReader();
            // Инициируем функцию FileReader
            fileReader.onload = (function(file) {
               
                return function(e) {
                    // Помещаем URI изображения в массив
                    dataArray.push({ name: file.name, value: this.result });                    
                    addImage((dataArray.length-1));
                };
                 
            })(files[index]);            
            // Производим чтение картинки по URI
            fileReader.readAsDataURL(file);
        })
    };

    // Процедура добавления эскизов на страницу
    function addImage(ind) {
        // Если индекс отрицательный значит выводим весь массив изображений
        //if (ind < 0) {
        //    start = 0; end = dataArray.length;
        //} else {
        //    // иначе только определенное изображение
        //    start = ind; end = ind + 1;
        //}
        ////if (dataArray.leinght <= maxFiles) {
        //    // Цикл для каждого элемента массива
        //for (i = start; i < end; ++i) {        
        //        // размещаем загруженные изображения
        //        //if ($('#dropped-files > .image').length <= maxFiles) {
        //        //    $('#dropped-files').append('<div id="img-' + i + '" class="image" style="background: url(' + dataArray[i].value + '); background-size: cover;"> <a href="#" id="drop-' + i + '" class="drop-button">Удалить изображение</a></div>');
        //        //}                
        //}
        //for (i = 1; i < dataArray.lenght; ++i) {
        $('#photo-row').append('<div id="img-' + ind + '" class="col-lg-3" style="background:url(' + dataArray[ind].value + ') no-repeat; background-size:cover;"></div>');

        //return false;     
        
            
    }
});