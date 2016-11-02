<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$lid=$requst->lecId;

$sql="DELETE * FROM `lecturers` WHERE `lecId`='".$lid."' ";

// $result = $connection->query($sql);

if ($connection->query($sql) === TRUE) {
    echo "New record created successfully";
    $validator="success";
} else {
    echo "Error: " . $sql . "<br>" . $connection->error;
    $validator="fail";
}


echo json_encode($validator);

?>
