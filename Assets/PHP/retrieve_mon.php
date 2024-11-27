<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$pokemonID = $_POST["pokemonID"];

// sql query to retrieve mon //
$retrieve_mon_sql = "SELECT pokemonID, pokemonName, pokemonType1, pokemonType2, pokemonWeight, pokemonHeight, spriteID FROM pokemon WHERE pokemonID = '$pokemonID';";

$retrieve_mon = mysqli_query($con,$retrieve_mon_sql) or die("PHP ERROR 2: Retrieve Mon Failed.");

$existing_info = mysqli_fetch_assoc($retrieve_mon);

$db_pokemonID = $existing_info["pokemonID"];
$db_pokemonName = $existing_info["pokemonName"];
$db_pokemonType1 = $existing_info["pokemonType1"];
$db_pokemonType2 = $existing_info["pokemonType2"];
$db_pokemonWeight = $existing_info["pokemonWeight"];
$db_pokemonHeight = $existing_info["pokemonHeight"];
$spriteID = $existing_info["spriteID"];

echo "Success!";
echo "\t";
echo $db_pokemonID;
echo "\t";
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