<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$pokemonID = $_POST["pokemonID"];

// sql query to retrieve mon //
$retrieve_stats_sql = "SELECT hp, attack, special_attack, defense, special_defense, speed FROM pokestats WHERE pokemonID = '$pokemonID';";

$retrieve_stats = mysqli_query($con,$retrieve_stats_sql) or die("PHP ERROR 2: Retrieve Mon Failed.");

$existing_info = mysqli_fetch_assoc($retrieve_stats);

$db_hp = $existing_info["hp"];
$db_attack = $existing_info["attack"];
$db_special_attack = $existing_info["special_attack"];
$db_defense = $existing_info["defense"];
$db_special_defense = $existing_info["special_defense"];
$db_speed = $existing_info["speed"];

echo "Success!";
echo "\t";
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

?>