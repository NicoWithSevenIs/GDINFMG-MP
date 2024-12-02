<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$playerID = $_POST["playerID"];

$moncheckquery = "SELECT pokemonID FROM pokemondetails; ";
$moncheck = mysqli_query($con, $moncheckquery) or die("2: Mon Check Query Failed."); //error code 2 = namecheck query failed

if ($num_rows != 3) {
    echo "You have missing mons!";
    exit();
}

$db_pokemonID = $existing_info["pokemonID"];

echo"Success!";
echo "\t";
echo $db_pokemonID;

?>