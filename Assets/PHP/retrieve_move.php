<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$moveID = $_POST["moveID"];

// sql query to retrieve mon //
$retrieve_move_sql = "SELECT moveName, moveDescription, moveType, moveGroup, movePower FROM move WHERE moveID = '$moveID';";

$retrieve_move = mysqli_query($con,$retrieve_mon_sql) or die("PHP ERROR 2: Retrieve Move Data Failed.");

$existing_info = mysqli_fetch_assoc($retrieve_move);
$db_moveName = $existing_info["moveName"];
$db_moveDescription = $existing_info["moveDescription"];
$db_moveType = $existing_info["moveType"];
$db_moveGroup = $existing_info["moveGroup"];
$db_movePower = $existing_info["movePower"];

echo "Success!";
echo "\t";
echo $db_moveName;
echo "\t";
echo $db_moveDescription;
echo "\t";
echo $db_moveType;
echo "\t";
echo $db_moveGroup;
echo "\t";
echo $db_movePower;

?>