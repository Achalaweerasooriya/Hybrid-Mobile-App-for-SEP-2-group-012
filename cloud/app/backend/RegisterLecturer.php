<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$name = $request->name;
$email = $request->email;
$dept = $request->dept;
$pw = "password1";

echo "backend";


//$pass = $request->password;
$validator = "success" ;

 

$sql="INSERT INTO `lecturers` (`lecName`, `lecEmail`, `lecPassword`, `lecDept`) VALUES ('".$name."','".$email."','".$pw."','".$dept."') ";

echo " backend".$email."recived";

if ($connection->query($sql) === TRUE) {
    echo "New record created successfully";
} else {
    echo "Error: " . $sql . "<br>" . $connection->error;
    $validator="fail";
}


echo json_encode($validator);

?>