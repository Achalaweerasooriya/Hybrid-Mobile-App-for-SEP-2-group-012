<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$cid = $request->cId;
$cname=$request->ncName;
$lic=$request->lic;
$dept=$request->ndept;


$sql="UPDATE `courses` SET `courseName`='".$cname."',`lecInCharge`='".$lic."',`courseDept`='".$dept."' WHERE `courseId`='".$cid."'  ";

if ($connection->query($sql) === TRUE) {
    echo "New record created successfully";
    $validator="success";
} else {
    echo "Error: " . $sql . "<br>" . $connection->error;
    $validator="fail";
}


echo json_encode($validator);



?>