<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$cname = $request->key;

$validator="success";

$sql="SELECT * FROM `courses` WHERE `courseId` LIKE '%".$cname."%' ";

$result = $connection->query($sql);



if ($result->num_rows > 0) 
		{
		    // output data of each row
		    			
			 while($row = $result->fetch_assoc()) 
			 {
				   $validator = 'success' ;
		    		$returndata[] = array(
					      	'cId' => $row['courseId'],
					      	'cName' => $row['courseName'],
					         'lic' => $row['lecInCharge'],
					        'dept' => $row['courseDept']
					    				 
					    );

		    	}
    	}  

	    else
	    {

			$validator = 'fail' ;
			$returndata = array(					      					         
						        'validator' => $validator
						    );


	     }


echo json_encode($returndata);


?>
