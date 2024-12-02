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

$retrieve_pokemon_query = "SELECT pokemonID, playerID, pokemonGender, pokemonNature, moveID1, moveID2, moveID3, moveID4, instanceID FROM pokemondetails WHERE instanceID = '$randomInstanceID';";
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
echo $playerID;
echo "\t";
echo $pokemonID;
echo "\t";
echo $pokemonGender;
echo "\t";
echo $pokemonNature;
echo "\t";
echo $playerID;
echo "\t";
echo $moveID1;
echo "\t";
echo $moveID2;
echo "\t";
echo $moveID3;
echo "\t";
echo $moveID4;
echo "\t";

//query to get Pokemon IV Fields //
$retrieve_pokemon_iv_query = "SELECT hpIV, atkIV, sp_atkIV, defIV, sp_defIV, speedIV FROM pokemonivdetails JOIN pokemondetails ON pokemonivdetails.instanceID = pokemondetails.instanceID WHERE pokemondetails.instanceID = '$randomInstanceID';";
$retrieve_pokemon_iv = mysqli_query($con, $retrieve_pokemon_iv_query) or die("ERROR WITH RETIEVEING POKEMON IV FIELDS GRA");
$iv_info = mysqli_fetch_assoc($retrieve_pokemon_iv);

$hpIV = $iv_info["hpIV"];
$atkIV = $iv_info["atkIV"];
$sp_atkIV = $iv_info["sp_atkIV"];
$defIV = $iv_info["defIV"];
$sp_defIV = $iv_info["sp_defIV"];
$speedIV = $iv_info["speedIV"];

echo $hpIV;
echo "\t";
echo $atkIV;
echo "\t";
echo $sp_atkIV;
echo "\t";
echo $defIV;
echo "\t";
echo $sp_defIV;
echo "\t";
echo $speedIV;
echo "\t";

//query to get Pokemon Stats//
$retrieve_stats_sql = "SELECT hp, attack, special_attack, defense, special_defense, speed FROM pokestats WHERE pokemonID = '$pokemonID';";

$retrieve_stats = mysqli_query($con,$retrieve_stats_sql) or die("PHP ERROR 2: Retrieve Mon Failed.");

$stats_req_result = mysqli_fetch_assoc($retrieve_stats);

$db_hp = $stats_req_result["hp"];
$db_attack = $stats_req_result["attack"];
$db_special_attack = $stats_req_result["special_attack"];
$db_defense = $stats_req_result["defense"];
$db_special_defense = $stats_req_result["special_defense"];
$db_speed = $stats_req_result["speed"];

echo $db_hp;
echo "\t";
echo $db_attack;
echo "\t";
echo $db_special_attack;
echo "\t";
echo $db_defense;
echo "\t";
echo $db_special_defense;
echo "\t";
echo $db_speed;
echo "\t";

//query to get Pokemon General Data//
$retrieve_mon_sql = "SELECT pokemonID, pokemonName, pokemonType1, pokemonType2, pokemonWeight, pokemonHeight, spriteID FROM pokemon WHERE pokemonID = '$pokemonID';";

$retrieve_mon = mysqli_query($con,$retrieve_mon_sql) or die("PHP ERROR 2: Retrieve Mon Failed.");

$mon_req_result = mysqli_fetch_assoc($retrieve_mon);

$db_pokemonName = $mon_req_result["pokemonName"];
$db_pokemonType1 = $mon_req_result["pokemonType1"];
$db_pokemonType2 = $mon_req_result["pokemonType2"];
$db_pokemonWeight = $mon_req_result["pokemonWeight"];
$db_pokemonHeight = $mon_req_result["pokemonHeight"];
$spriteID = $mon_req_result["spriteID"];

echo $db_pokemonName;
echo "\t";
echo $db_pokemonType1;
echo "\t";
echo $db_pokemonType2;
echo "\t";
echo $db_pokemonWeight;
echo "\t";
echo $db_pokemonHeight;
echo "\t";
echo $spriteID;


?>