<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$getcountquery = "SELECT COUNT(*) AS row_count FROM pokemon;";

$getcountsql = mysqli_query($con, $getcountquery) or die("2: Name Check Query Failed."); //error code 2 = namecheck query failed


$existing_info = mysqli_fetch_assoc($getcountsql);
$count = $existing_info["row_count"];

echo "Success Count Retrieve!";
echo "\t";
echo $count;

?>