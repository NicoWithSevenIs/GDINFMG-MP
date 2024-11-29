<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$playerID = $_POST["playerID"];
$pokemonID = $_POST["pokemonID"];
$pokemonGender = $_POST["pokemonGender"];
$pokemonNature = $_POST["pokemonNature"];
$moveID1 = $_POST["moveID1"];
$moveID2 = $_POST["moveID2"];    
$moveID3 = $_POST["moveID3"];
$moveID4 = $_POST["moveID4"]; 

//query to insert //
$send_data_query = "INSERT INTO pokemondetails (playerID, pokemonID, pokemonGender, pokemonNature, moveID1, moveID2, moveID3, moveID4) VALUES ('$playerID', '$pokemonID', '$pokemonGender', '$pokemonNature', '$moveID1', '$moveID2', '$moveID3', '$moveID4');";
mysqli_query($con, $send_data_query) or die("4: Insert user details query failed.");
echo("Success!")

?>