<?php


$con = mysqli_connect("localhost", "root", "root", "gdinfmg_mp");

// check if connection happened //
if (mysqli_connect_errno()) {
    echo "PHP ERROR 1: Connection Failed."; // 1 = connection failed
    exit();
}

//$playerID = $_POST["playerID"];

// $instancecheckquery = "SELECT instanceID1, instanceID2, instanceID3 FROM player WHERE playerID = '$playerID'; ";
// $instanceCheck = mysqli_query($con, $instancecheckquery) or die("2: Name Check Query Failed.");

// $existing_info = mysqli_fetch_assoc($instanceCheck);
// $id1 = $existing_info["instanceID1"];
// $id2 = $existing_info["instanceID2"];
// $id3 = $existing_info["instanceID3"];

echo "Success!";
echo "\t";
// echo "Player Party Instance: " . $id1;
// echo "\t";
// echo "Player Party Instance: " . $id2;
// echo "\t";
// echo "Player Party Instance: " . $id3;
// echo "\t";

$getpokemondetailsquery = "SELECT instanceID FROM pokemondetails;";
$getpokemondetails = mysqli_query($con, $getpokemondetailsquery) or die("3: get instance from pokemondetails failed.");

while ($pokemondetais_result = mysqli_fetch_assoc($getpokemondetails)) {
    $pd_instanceID = $pokemondetais_result["instanceID"];

    echo "Pokemon Details Instance: " . $pd_instanceID;
    echo "\t";
}

?>