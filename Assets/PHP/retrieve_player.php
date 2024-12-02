<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$playerID = $_POST["playerID"];

$player_req_query = "SELECT playerID, currentFloor, instanceID1, instanceID2, instanceID3 FROM player; ";
$player_req = mysqli_query($con, $moncheckquery) or die("2: Mon Check Query Failed."); //error code 2 = namecheck query failed

$db_playerID = $existing_info["playerID"];
$db_currentFloor = $existing_info["currentFloor"];
$db_instanceID1 = $existing_info["instanceID1"];
$db_instanceID2 = $existing_info["instanceID2"];
$db_instanceID3 = $existing_info["instanceID3"];

echo "Success!";
echo "\t";
echo $db_playerID;
echo "\t";
echo $db_currentFloor;
echo "\t";
echo $db_instanceID1;
echo "\t";
echo $db_instanceID2;
echo "\t";
echo $db_instanceID3;

?>