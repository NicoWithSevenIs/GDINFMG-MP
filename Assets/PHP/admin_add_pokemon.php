<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$pokemonID = $_POST["pokemonID"]; 
$pokemonName = $_POST["pokemonName"]; 
$pokemonType1 = $_POST["pokemonType1"]; 
$pokemonType2 = $_POST["pokemonType2"];
$pokemonWeight = $_POST["pokemonWeight"]; 
$pokemonHeight = $_POST["pokemonHeight"]; 
$spriteID = $_POST["spriteID"]; 

$hp = $_POST["hp"];
$attack = $_POST["attack"];
$special_attack = $_POST["special_attack"];
$defense = $_POST["defense"];
$special_defense = $_POST["special_defense"];
$speed = $_POST["speed"];

$moveID1 = $_POST["moveID1"];
$moveID2 = $_POST["moveID2"];
$moveID3 = $_POST["moveID3"];
$moveID4 = $_POST["moveID4"];
$moveID5 = $_POST["moveID5"];
$moveID6 = $_POST["moveID6"];

// check if name exists //
$namecheckquery = "SELECT pokemonID FROM pokemon WHERE pokemonID = '$pokemonID'; ";

$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name Check Query Failed."); //error code 2 = namecheck query failed

// check namecheck return //
if (mysqli_num_rows($namecheck) > 0) {
    echo "Pokemon ID Already Exists in Pokemon!";
    exit();
}

$namecheckquery2 = "SELECT pokemonID FROM pokestats WHERE pokemonID = '$pokemonID'; ";

$namecheck2 = mysqli_query($con, $namecheckquery2) or die("2: Name Check Query Failed."); //error code 2 = namecheck query failed

// check namecheck return //
if (mysqli_num_rows($namecheck2) > 0) {
    echo "Pokemon ID Already Exists in PokeStats!";
    exit();
}

$move_pool_check_query = "SELECT moveID FROM move WHERE moveID = '$moveID1';";
$move_pool_check = mysqli_query($con, $move_pool_check_query);
if (mysqli_num_rows($move_pool_check) == 0) {
    echo "Move ID 1 Doesn't Exist in Move!";
    exit();
}


$move_pool_check_query2 = "SELECT moveID FROM move WHERE moveID = '$moveID2';";
$move_pool_check2 = mysqli_query($con, $move_pool_check_query2);
if (mysqli_num_rows($move_pool_check2) == 0) {
    echo "Move ID 2 Doesn't Exist in Move!";
    exit();
}

$move_pool_check_query3 = "SELECT moveID FROM move WHERE moveID = '$moveID3';";
$move_pool_check3 = mysqli_query($con, $move_pool_check_query3);
if (mysqli_num_rows($move_pool_check3) == 0) {
    echo "Move ID 3 Doesn't Exist in Move!";
    exit();
}

$move_pool_check_query4 = "SELECT moveID FROM move WHERE moveID = '$moveID4';";
$move_pool_check4 = mysqli_query($con, $move_pool_check_query4);
if (mysqli_num_rows($move_pool_check4) == 0) {
    echo "Move ID 4 Doesn't Exist in Move!";
    exit();
}

$move_pool_check_query5 = "SELECT moveID FROM move WHERE moveID = '$moveID5';";
$move_pool_check5 = mysqli_query($con, $move_pool_check_query5);
if (mysqli_num_rows($move_pool_check5) == 0) {
    echo "Move ID 5 Doesn't Exist in Move!";
    exit();
}

$move_pool_check_query6 = "SELECT moveID FROM move WHERE moveID = '$moveID6';";
$move_pool_check6 = mysqli_query($con, $move_pool_check_query6);
if (mysqli_num_rows($move_pool_check6) == 0) {
    echo "Move ID 6 Doesn't Exist in Move!";
    exit();
}

// $send_data_query = "INSERT INTO pokemon (pokemonID, pokemonName, pokemonType1, pokemonType2, pokemonWeight, pokemonHeight, spriteID) VALUES ('$pokemonID', '$pokemonName', '$pokemonType1', '$pokemonType2', '$pokemonWeight', '$pokemonHeight', '$spriteID');";   
// mysqli_query($con, $send_data_query) or die("4: Insert user details query failed.");

// $send_data_query2 = "INSERT INTO pokestats (hp, attack, special_attack, defense, special_defense, speed) VALUES ('$hp', '$attack', '$special_attack', '$defense', '$special_defense', '$speed');";   
// mysqli_query($con, $send_data_query2) or die("4: Insert user details query failed.");

// $send_data_query3 =  "INSERT INTO movepool (pokemonID, moveID1, moveID2, moveID3, moveID4, moveID5, moveID6) VALUES ('$pokemonID', '$moveID1', '$moveID2', '$moveID3', '$moveID4', '$moveID5', '$moveID6');";   
// mysqli_query($con, $send_data_query3) or die("4: Insert user details query failed.");

echo "Success!";

?>