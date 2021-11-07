<?php

$message = $_POST['message']; // извлекаем присланные данные в переменные
$cloudiness = $_POST['cloud_value'];
$timestamp = $_POST['timestamp'];

$combined = $message." clodiness=".$cloudiness." time =".$timestamp."\n";

$filename = "data.txt"; // определяем имя файла, в который будет выполняться запись
file_put_contents($filename, $combined, FILE_APPEND | LOCK_EX); // записываем файл

echo "Logged";

?>