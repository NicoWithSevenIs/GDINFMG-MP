<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$playerID = $_POST["playerID"];
$currentFloor = $_POST["currentFloor"];

$instanceID1 = $_POST["instanceID1"];
$instanceID2 = $_POST["instanceID2"];
$instanceID3 = $_POST["instanceID3"];

$send_data_query = "INSERT INTO player (playerID, currentFloor, instanceID1, instanceID3) VALUES ('$playerID', '$currentFloor', '$instanceID1', '$instanceID2', '$instanceID3');";
mysqli_query($con, $send_data_query) or die("4: Insert user details query failed.");
echo "Success send to player!";

?>