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
$partyMemberNum = $_POST["partyMemberNum"];

$moncheckquery = "SELECT pokemonID FROM pokemondetails; ";
$moncheck = mysqli_query($con, $moncheckquery) or die("2: Name Check Query Failed."); //error code 2 = namecheck query failed

$num_rows = mysqli_num_rows($moncheck);

//echo "num_rows: " . $num_rows;

if ($num_rows != 3) {
    //query to insert //
    $send_data_query = "INSERT INTO pokemondetails (playerID, pokemonID, pokemonGender, pokemonNature, moveID1, moveID2, moveID3, moveID4, partyMemberNum) VALUES ('$playerID', '$pokemonID', '$pokemonGender', '$pokemonNature', '$moveID1', '$moveID2', '$moveID3', '$moveID4', '$partyMemberNum');";
    mysqli_query($con, $send_data_query) or die("4: Insert user details query failed.");
    echo("Success!");
}
else {
    $updatequery = "UPDATE pokemondetails SET pokemonID = '$pokemonID', playerID = '$playerID', pokemonGender = '$pokemonGender',  pokemonNature = '$pokemonNature', moveID1 = '$moveID1', moveID2 = '$moveID2', moveID3 = '$moveID3', moveID4 = '$moveID4' WHERE partyMemberNum = '$partyMemberNum';";
    mysqli_query($con, $updatequery) or die("7: Save query failed");
    echo("Update Success!");
 
}

// , pokemonID = '$pokemonID', pokemonGender = '$pokemonGender', pokemonNature = '$pokemonNature', moveID1 = '$moveID1', moveID2 = '$moveID2', moveID3 = '$moveID3', moveID4 = '$moveID4' 
?>