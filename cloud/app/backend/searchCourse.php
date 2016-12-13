<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$cname = $request->key;

$validator="success";

$sql="SELECT `id`,`name` , `year` ,`semester` , `time` , `location` , `enrollmentkey` FROM `products` WHERE `name` LIKE '%".$cname."%' ";

$result = $connection->query($sql);



if ($result->num_rows > 0) 
		{
		    // output data of each row
		    			
			 while($row = $result->fetch_assoc()) 
			 {
				   $validator = 'success' ;
		    		$returndata[] = array(
		    				'id' => $row['id'],					      	
					      	'name' => $row['name'],
					      	'year' => $row['year'],
					      	'semester' => $row['semester'],
					      	'time' =>$row['time'],
					      	'location' =>$row['location'],
					      	'enrollmentkey' =>$row['enrollmentkey']
					         
					        
					    				 
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
