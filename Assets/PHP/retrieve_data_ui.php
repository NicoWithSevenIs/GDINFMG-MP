<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$pokemon_ui_query = "SELECT pokemonID, pokemonName, pokemonType1, pokemonType2, spriteID FROM pokemon;";
$pokemon_ui = mysqli_query($con, $pokemon_ui_query) or die("2: RETRIEVAL OF ALL MONS FAILED.");

echo "Success!";
echo "\t";

while ($pokemon_ui_info = mysqli_fetch_assoc($pokemon_ui)) {
    $pokemonID = $pokemon_ui_info["pokemonID"];
    $pokemonName = $pokemon_ui_info["pokemonName"];
    $pokemonType1 = $pokemon_ui_info["pokemonType1"];
    $pokemonType2 = $pokemon_ui_info["pokemonType2"];
    $spriteID = $pokemon_ui_info["spriteID"];
    
    echo $pokemonID;
    echo "\t";
    echo $pokemonName;
    echo "\t";
    echo $pokemonType1;
    echo "\t";
    echo $pokemonType2;
    echo "\t";
    echo $spriteID;    
}

$pokemon_ui_query = "SELECT (*) FROM move;";
$pokemon_ui = mysqli_query($con, $pokemon_ui_query) or die("2: RETRIEVAL OF ALL MONS FAILED.");



?>