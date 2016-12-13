<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$name = $request->name;
$email = $request->email;
$dept = $request->dept;
$pw = $request->password;



//$pass = $request->password;
$validator = "success" ;

 

$sql="INSERT INTO `lecturers` (`lecName`, `lecEmail`, `lecPassword`) VALUES ('".$name."','".$email."','".$pw."') ";



if ($connection->query($sql) === TRUE) {
    echo "New record created successfully";
} else {
    echo "Error: " . $sql . "<br>" . $connection->error;
    $validator="fail";
}


echo json_encode($validator);

?>