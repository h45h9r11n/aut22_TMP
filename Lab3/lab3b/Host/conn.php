<?php 
    $mysql = mysqli_connect("localhost", "root", "developer", "ControlPanel");
    if (mysqli_connect_errno()){
        echo "Failed to connect database: " . mysqli_connect_errno();
        exit();
    }

?>