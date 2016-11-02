<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$cid = $request->cID;
$cname=$request->cName;
$dept=$request->dept;
$lec=$request->lec;


		$validator = "success";

		$sql="INSERT INTO `courses` VALUES('".$cid."','".$cname."','".$dept."','".$lec."') ";

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