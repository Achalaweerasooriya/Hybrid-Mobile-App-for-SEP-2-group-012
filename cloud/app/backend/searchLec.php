<?php 
require("../dbConnection.php");
$postdata = file_get_contents("php://input");
$request = json_decode($postdata);
$key = $request->key;
		$validator = "success";

		$sql = "SELECT * FROM `lecturers` WHERE `lecName` LIKE '%".$key."%' ";

		
		$result = $connection->query($sql);

		if ($result->num_rows > 0) 
		{
		    // output data of each row
		    			
			 while($row = $result->fetch_assoc()) 
			 {
				   $validator = 'success' ;
		    		$returndata[] = array(
					      	'lecId' => $row['lecId'],
					      	'lecName' => $row['lecName'],
					         'email' => $row['lecEmail'],
					        
					    				 
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