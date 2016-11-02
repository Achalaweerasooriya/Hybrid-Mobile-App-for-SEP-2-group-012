<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$lid=$requst->lecId;
$name = $request->nlecName;
$email = $request->nemail;
$dept = $request->ndept;


$sql="UPDATE `lecturers` SET `lecName`='".$name."',`lecEmail`='".$email."',`lecDept`='".$dept."' WHERE `lecId`='".$lid."' ";



if ($connection->query($sql) === TRUE) {
    echo "New record created successfully";
    $validator="success";
} else {
    echo "Error: " . $sql . "<br>" . $connection->error;
    $validator="fail";
}


echo json_encode($validator);

?>