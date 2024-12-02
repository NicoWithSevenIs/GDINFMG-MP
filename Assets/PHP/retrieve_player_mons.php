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

$num_rows = mysqli_num_rows($moncheck);
if ($num_rows != 3) {
    echo "You have missing mons!";
    exit();
}

while ($existing_info = mysqli_fetch_assoc($moncheck)) {
     echo $existing_info['pokemonID'] . "\n"; // Output each pokemonID on a new line
}

?>