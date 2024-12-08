<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$randomInstanceID = $_POST["randomInstanceID"];
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

echo "Success"; // 0
echo "\t";
echo $playerID; //1
echo "\t";
echo $pokemonID; //2
echo "\t";
echo $pokemonGender; //3
echo "\t";
echo $pokemonNature; //4
echo "\t";
echo $moveID1; //5
echo "\t";
echo $moveID2; //6
echo "\t";
echo $moveID3; //7
echo "\t";
echo $moveID4; //8
echo "\t";

// //query to get Pokemon IV Fields //
$retrieve_pokemon_iv_query = "SELECT hpIV, atkIV, sp_atkIV, defIV, sp_defIV, speedIV FROM pokemonivdetails JOIN pokemondetails ON pokemonivdetails.instanceID = pokemondetails.instanceID WHERE pokemondetails.instanceID = '$randomInstanceID';";
$retrieve_pokemon_iv = mysqli_query($con, $retrieve_pokemon_iv_query) or die("ERROR WITH RETIEVEING POKEMON IV FIELDS GRA");
$iv_info = mysqli_fetch_assoc($retrieve_pokemon_iv);

$hpIV = $iv_info["hpIV"];
$atkIV = $iv_info["atkIV"];
$sp_atkIV = $iv_info["sp_atkIV"];
$defIV = $iv_info["defIV"];
$sp_defIV = $iv_info["sp_defIV"];
$speedIV = $iv_info["speedIV"];

echo $hpIV; //9
echo "\t";
echo $atkIV; //10
echo "\t";
echo $sp_atkIV; //11
echo "\t";
echo $defIV; //12
echo "\t";
echo $sp_defIV; //13
echo "\t";
echo $speedIV; //14
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

echo $db_hp; //15
echo "\t";
echo $db_attack; //16
echo "\t";
echo $db_special_attack; //17
echo "\t";
echo $db_defense; //18
echo "\t";
echo $db_special_defense; //19
echo "\t";
echo $db_speed; //20
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

echo $db_pokemonName; //21
echo "\t";
echo $db_pokemonType1; //22
echo "\t";
echo $db_pokemonType2; //23
echo "\t";
echo $db_pokemonWeight; //24
echo "\t";
echo $db_pokemonHeight; //25
echo "\t";
echo $spriteID; //26
echo "\t";
echo $randomInstanceID; //27
echo "\t";

?>