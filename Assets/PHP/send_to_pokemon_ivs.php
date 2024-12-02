<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$pokemonID = $_POST["pokemonID"];
$hpIV = $_POST["hpIV"];
$atkIV = $_POST["atkIV"];
$sp_atkIV = $_POST["sp_atkIV"];
$defIV = $_POST["defIV"];
$sp_defIV = $_POST["sp_defIV"];
$speedIV = $_POST["speedIV"];    

$moncheckquery = "SELECT pokemonID FROM pokemonivdetails; ";
$moncheck = mysqli_query($con, $moncheckquery) or die("2: Mon Check Query Failed."); //error code 2 = namecheck query failed

$num_rows = mysqli_num_rows($moncheck);

if ($num_rows != 3) {
    //query to insert //
    $send_data_query = "INSERT INTO pokemonivdetails (pokemonID, hpIV, atkIV, sp_atkIV, defIV, sp_defIV, speedIV) VALUES ('$pokemonID', '$hpIV', '$atkIV', '$sp_atkIV', '$defIV', '$sp_defIV', '$speedIV');";
    mysqli_query($con, $send_data_query) or die("4: Insert user details query failed.");
    echo "Success send to pokemonivdetails!";
}
else {
    // $updatequery = "UPDATE pokemonivdetails SET hpIV = '$hpIV', atkIV = '$atkIV', sp_atkIV = '$sp_atkIV',  defIV = '$defIV', sp_defIV = '$sp_defIV', speedIV = '$speedIV', pokemonID = '$pokemonID', playerID = '$playerID' WHERE partyMemberNum = '$partyMemberNum';";
    // mysqli_query($con, $updatequery) or die("7: Save query failed");
    // echo("Update Success!");
 
}

?>