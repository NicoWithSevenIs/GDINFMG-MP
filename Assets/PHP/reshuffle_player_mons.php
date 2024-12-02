<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$playerID = $_POST["playerID"];


$get_instance_query = "SELECT instanceID FROM pokemondetails;";
$get_instance = mysqli_query($con,$get_instance_query);
$list_instanceid = [];

if (mysqli_num_rows($get_instance) < 3) {
    echo "inadequate party!";
    exit();
}

while ($row = mysqli_fetch_array($get_instance)) {
    $list_instanceid[] = $row["instanceID"];
}


$instancecheckquery = "SELECT instanceID1, instanceID2, instanceID3 FROM player WHERE playerID = '$playerID'; ";
$instanceCheck = mysqli_query($con, $instancecheckquery) or die("2: Name Check Query Failed.");

$existing_info = mysqli_fetch_assoc($instanceCheck);
$id1 = $existing_info["instanceID1"];
$id2 = $existing_info["instanceID2"];
$id3 = $existing_info["instanceID3"];

$FoundInParty = true;
$id_num = 0;

while ($FoundInParty) {
    $randomIndex = rand(0, count($list_instanceid) - 1);
    $randomInstanceID = $list_instanceid[$randomIndex]; 

    if ($randomInstanceID != $id1) {
         $FoundInParty = false;
        $id_num = 1;
    }

    if ($randomInstanceID != $id2) {
        $FoundInParty = false;
        $id_num = 2;
    }

    if ($randomInstanceID != $id3) {
        $FoundInParty = false;
        $id_num = 3;
    }
}

// query to get Pokemon Fields //

$retrieve_pokemon_query = "SELECT pokemonID, playerID, pokemonGender, pokemonNature, moveID1, moveID2, moveID3, moveID4, instanceID FROM pokemonDetails WHERE instanceID = '$randomInstanceID';";
$retrieve_pokemon = mysqli_query($con, $retrieve_pokemon_query) or die("ERROR WITH RETIEVEING POKEMON FIELDS GRA");
$pokemon_info = mysqli_fetch_assoc($retrieve_pokemon);

$playerID = $pokemon_info["playerID"];
$pokemonID = $pokemon_info["pokemonID"];
$pokemonGender = $pokemon_info["pokemonGender"];
$pokemonNature = $pokemon_info["pokemonNature"];
$moveID1 = $pokemon_info["moveID1"];
$moveID2 = $pokemon_info["moveID2"];    
$moveID3 = $pokemon_info["moveID3"];
$moveID4 = $pokemon_info["moveID4"]; 

echo "Success";
echo "\t";
echo $randomInstanceID;
echo "\t";
echo "$playerID";
echo "\t";
echo $pokemonID;
echo "\t";
echo "$pokemonGender";
echo "\t";
echo $pokemonNature;
echo "\t";
echo "$playerID";
echo "\t";
echo $moveID1;
echo "\t";
echo $moveID2;
echo "\t";
echo $moveID3;
echo "\t";
echo "$moveID4";
echo "\t";

?>