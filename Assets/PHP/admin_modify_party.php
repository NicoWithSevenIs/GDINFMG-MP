<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}


$playerID = $_POST["playerID"]; 
$instanceID1 = $_POST["instanceID1"]; 
$instanceID2 = $_POST["instanceID2"]; 
$instanceID3 = $_POST["instanceID3"];

// check instanceid  //
$instanceidquery = "SELECT instanceID FROM pokemondetails WHERE instanceID = '$instanceID1'; ";

$instancecheck = mysqli_query($con, $instanceidquery) or die("2: Name Check Query Failed."); //error code 2 = namecheck query failed

// check namecheck return //
if (mysqli_num_rows($instancecheck) > 0) {
    echo "Pokemon ID Already Exists in Pokemon!";
    exit();
}

$instanceidquery2 = "SELECT instanceID FROM pokemondetails WHERE instanceID = '$instanceID2'; ";

$instancecheck2 = mysqli_query($con, $instanceidquery2) or die("2: Name Check Query Failed."); //error code 2 = namecheck query failed

// check namecheck return //
if (mysqli_num_rows($instancecheck2) > 0) {
    echo "Pokemon ID Already Exists in Pokemon!";
    exit();
}

$instanceidquery3 = "SELECT instanceID FROM pokemondetails WHERE instanceID = '$instanceID3'; ";

$instancecheck3 = mysqli_query($con, $instanceidquery3) or die("2: Name Check Query Failed."); //error code 2 = namecheck query failed

// check namecheck return //
if (mysqli_num_rows($instancecheck3) > 0) {
    echo "Pokemon ID Already Exists in Pokemon!";
    exit();
}


$updatcheckequery = "UPDATE player SET instanceID1 = '$instanceID1', instanceID2 = '$instanceID2', instanceID3 = '$instanceID3' WHERE playerID = '$playerID';";
$updatecheck = mysqli_query($con, $updatcheckequery) or die("UPDATE PLAYER INSTANCE FAILED HUHU");




echo "Success";


?>