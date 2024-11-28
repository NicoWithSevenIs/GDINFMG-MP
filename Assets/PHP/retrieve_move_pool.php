<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$pokemonID = $_POST["pokemonID"];

// sql query to retrieve mon //
$retrieve_pool_sql = "SELECT moveID1, moveID2, moveID3, moveID4, moveID5, moveID6 FROM movepool WHERE pokemonID = '$pokemonID';";

$retrieve_pool = mysqli_query($con,$retrieve_pool_sql) or die("PHP ERROR 2: Retrieve Move Pool Failed.");

$existing_info = mysqli_fetch_assoc($retrieve_pool);

$db_moveID1 = $existing_info["moveID1"];
$db_moveID2 = $existing_info["moveID2"];
$db_moveID3 = $existing_info["moveID3"];
$db_moveID4 = $existing_info["moveID4"];
$db_moveID5 = $existing_info["moveID5"];
$db_moveID6 = $existing_info["moveID6"];

echo "Success!";
echo "\t";
echo $db_moveID1;
echo "\t";
echo $db_moveID2;
echo "\t";
echo $db_moveID3;
echo "\t";
echo $db_moveID4;
echo "\t";
echo $db_moveID5;
echo "\t";
echo $db_moveID6;

?>