<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$hpIV = $_POST["hpIV"];
$atkIV = $_POST["atkIV"];
$sp_atkIV = $_POST["sp_atkIV"];
$defIV = $_POST["defIV"];
$sp_defIV = $_POST["sp_defIV"];
$speedIV = $_POST["speedIV"];    

//query to insert //
$send_data_query = "INSERT INTO pokemonivdetails (hpIV, atkIV, sp_atkIV, defIV, sp_defIV, speedIV) VALUES ('$hpIV', '$atkIV', '$sp_atkIV', '$defIV', '$sp_defIV', '$speedIV');";
mysqli_query($con, $send_data_query) or die("4: Insert user details query failed.");
echo "Success send to poke  monivdetails!";


?>