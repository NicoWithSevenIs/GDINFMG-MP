<?php

$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

$pokemonName = $_POST["pokemonName"]; 

$namecheckquery = "SELECT pokemonName FROM pokemon WHERE pokemonName = '$pokemonName'; ";

$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name Check Query Failed."); //error code 2 = namecheck query failed

if (mysqli_num_rows($namecheck) != 1) {
   echo "found more than 1 mon or no mon";
   exit();
}

 $deletemonquery = "DELETE FROM pokemon WHERE pokemonName = '$pokemonName';";
 $deletemon = mysqli_query($con, $deletemonquery) or die("3: Delete Mon query failed.");

echo "Success!";

?>