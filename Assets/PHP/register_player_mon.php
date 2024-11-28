<?php

$con = mysqli_connect("localhost", "root", "root", "unity_tutorial");

// check if connection happened //

if (mysqli_connect_errno()) {
    echo "1: Connection Failed."; // 1 = connection failed
    exit();
}

$playerID = $_POST["playerID"];
$pokemonID = $_POST["pokemonID"];
$nature = $_POST["nature"];
$gender = $_POST["gender"];

$retrieve_mon_sql = "SELECT pokemonID, pokemonName, pokemonType1, pokemonType2, pokemonWeight, pokemonHeight, spriteID FROM pokemon WHERE pokemonID = '$pokemonID';";

$retrieve_mon = mysqli_query($con,$retrieve_mon_sql) or die("PHP ERROR 2: Retrieve Mon Failed.");

$existing_info = mysqli_fetch_assoc($retrieve_mon);



?>

$namecheckquery = "SELECT Username FROM players WHERE Username='$username'; ";

$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name Check Query Failed."); //error code 2 = namecheck query failed

// check namecheck return //
if (mysqli_num_rows($namecheck) > 0) {
    echo "3: Name already exists. Cannot Register.";
    exit();
}

// encryption //
$salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$" ; // randomized encryption
$hash = $password . "bomba" . $salt;
//inserting into table//

//$insertuserquery = "INSERT INTO players (Username, hash, salt) VALUES(' " . $username . " ', ' " . $hash . " ', ' " . $salt . " ');";
$insertuserquery = "INSERT INTO players (Username, hash, salt) VALUES('$username', '$hash', '$salt');";

mysqli_query($con, $insertuserquery) or die("4: Insert user details query failed.");

echo("0")