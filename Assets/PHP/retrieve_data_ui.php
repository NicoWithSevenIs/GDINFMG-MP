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
    
    echo "PokemonID: " . $pokemonID;
    echo "\t";
    echo "PokemonName: " . $pokemonName;
    echo "\t";
    echo "PokemonType1: " . $pokemonType1;
    echo "\t";
    echo "PokemonType2: " . $pokemonType2;
    echo "\t";
    echo "SpriteID: " . $spriteID;    
    echo "\t";
}

$move_ui_query = "SELECT * FROM move;";
$move_ui = mysqli_query($con, $move_ui_query) or die("2: RETRIEVAL OF ALL MONS FAILED.");

while ($move_ui_info = mysqli_fetch_assoc($move_ui)) {
    $moveID = $move_ui_info["moveID"];
    $moveName = $move_ui_info["moveName"];
    $moveDescription = $move_ui_info["moveDescription"];
    $moveType = $move_ui_info["moveType"];
    $moveGroup = $move_ui_info["moveGroup"];
    $movePower = $move_ui_info["movePower"];

    echo "MoveID: " . $moveID;
    echo "\t";
    echo "MoveName: " . $moveName;
    echo "\t";
    echo "MoveDescription: " . $moveDescription;
    echo "\t";
    echo "MoveType: " . $moveType;
    echo "\t";
    echo "MoveGroup: " . $moveGroup;    
    echo "\t";
    echo "MovePower: " . $movePower;
    echo "\t";
}


?>