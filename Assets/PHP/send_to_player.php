<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$playerID = $_POST["playerID"];

$instanceID = $_POST["instanceID"];
$idnum = $_POST["idnum"];

$send_data_query = "SELECT playerID FROM player;";
$send_data = mysqli_query($con, $send_data_query) or die("4: Num check query failed.");

if (mysqli_num_rows($send_data) == 0) {
    $send_data_query = "INSERT INTO player (playerID, currentFloor, instanceID1, instanceID2, instanceID3) VALUES ('$playerID', '$currentFloor', '$instanceID1', '$instanceID2', '$instanceID3');";
    mysqli_query($con, $send_data_query) or die("5: Insert user details query failed.");
    echo "Success send to player!";
}
else {
    if ($idnum == 0) {
        $send_data_query = "UPDATE player SET instanceID1 = '$instanceID' WHERE playerID = '$playerID'; ";
        mysqli_query($con, $send_data_query) or die("5: Insert user details query failed.");
        echo "Success update to player!";
    }
    elseif ($idnum == 1) {
        $send_data_query = "UPDATE player SET instanceID2 = '$instanceID' WHERE playerID = '$playerID'; ";
        mysqli_query($con, $send_data_query) or die("5: Insert user details query failed.");
        echo "Success update to player!";
    }
    elseif ($idnum == 2) {
        $send_data_query = "UPDATE player SET instanceID3 = '$instanceID' WHERE playerID = '$playerID'; ";
        mysqli_query($con, $send_data_query) or die("5: Insert user details query failed.");
        echo "Success update to player!";
    }
}


?>