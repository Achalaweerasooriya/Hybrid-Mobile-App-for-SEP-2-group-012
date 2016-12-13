<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$sname=$request->sname;
$year=$request->year;
$semester=$request->semester;
$enrollmentkey=$request->enrollmentkey;
$time=$request->time;
$location = $request->location;



		$validator = "success";

		$sql="INSERT INTO `products`(`name`,`year`,`semester`,`time`,`location`,`enrollmentkey`) VALUES('".$sname."','".$year."','".$semester."' ,  '".$time."' , '".$location."' ,'".$enrollmentkey."') ";

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