var express = require('express');
var router = express.Router();

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Kiss my Ass '});
});

module.exports = router;



/* Create Employee Service. */
router.post('/add/v1/createEmployee', function(req,res,next){
    try{
        var reqObj = req.body;        
        console.log(reqObj);
        req.getConnection(function(err, conn){
    if(err)
    {
        console.error('SQL Connection error: ', err);
        return next(err);
    }
    else
    {
        var insertSql = "INSERT INTO employee SET ?";
        var insertValues = {
        "Emp_Name" : reqObj.empName
    };
        var query = conn.query(insertSql, insertValues, function (err, result){
        if(err){
            console.error('SQL error: ', err);
            return next(err);
    }
        var Employee_Id = result.insertId;
        res.json({"Emp_Name":Emp_Name});
    });
    }
    });
    }
    catch(ex){
    console.error("Internal error:"+ex);
    return next(ex);
    }
});

/* Get Employee Service. */
router.get('/get/v1/getEmployeeDetails', function(req, res, next) {
    try {
            var roleId = req.param('roleId');
            var deptId = req.param('deptId');
                  /*var query = url.parse(req.url,true).query;
                  console.log(query);
        var roleId = query.roleId;
        var deptId = query.deptId;*/
        console.log(roleId);
        console.log(deptId);
        req.getConnection(function(err, conn) {
            if (err) {
                console.error('SQL Connection error: ', err);
                return next(err);
            } else {
                conn.query('select E.Emp_Name, Date_Format(E.Doj,"%d-%m-%Y") AS DOJ, D.Dept_Name, R.Role_Name from employee E, role R, department D where E.Role_Id = R.Role_Id and E.Dept_Id = D.Dept_Id and E.Role_Id = ? and E.Dept_Id = ? order by DOJ', [roleId,deptId], function(err, rows, fields) {
                    if (err) {
                        console.error('SQL error: ', err);
                        return next(err);
                    }
                    var resEmp = [];
                    for (var empIndex in rows) {
                        var empObj = rows[empIndex ];
                        resEmp .push(empObj);
                    }
                    res.json(resEmp);
                });
            }
        });
    } catch (ex) {
        console.error("Internal error:" + ex);
        return next(ex);
    }
});

/*Get available lectures*/
router.get('/get/AvailableLec', function(req, res, next) {
    try {
            
                  /*var query = url.parse(req.url,true).query;
                  console.log(query);
        var roleId = query.roleId;
        var deptId = query.deptId;*/
        //console.log(roleId);
        //console.log(deptId);
        req.getConnection(function(err, conn) {
            if (err) {
                console.error('SQL Connection error: ', err);
                return next(err);
            } else {
                conn.query('select li.ilid from lecture_instance li', function(err, rows, fields) {
                    if (err) {
                        console.error('SQL error: ', err);
                        return next(err);
                    }
                    var resEmp = [];
                    for (var empIndex in rows) {
                        var empObj = rows[empIndex ];
                        resEmp .push(empObj);
                    }
                    res.json(resEmp);
                });
            }
        });
    } catch (ex) {
        console.error("Internal error:" + ex);
        return next(ex);
    }
});


/*Get enrollment key*/
router.get('/get/EnrollKey', function(req, res, next) {
    try {
            
            var lecId = req.param('lecId');

        req.getConnection(function(err, conn) {
            if (err) {
                console.error('SQL Connection error: ', err);
                return next(err);
            } else {
                conn.query('select li.enroll from lecture_instance li where ilid = ? ', [lecId], function(err, rows, fields) {
                    if (err) {
                        console.error('SQL error: ', err);
                        return next(err);
                    }
                    var resEmp = [];
                    for (var empIndex in rows) {
                        var empObj = rows[empIndex ];
                        resEmp .push(empObj);
                    }
                    res.json(resEmp);
                });
            }
        });
    } catch (ex) {
        console.error("Internal error:" + ex);
        return next(ex);
    }
});

/* Create Student Question. */
router.post('/add/addquestion', function(req,res,next){
    try{
        var reqObj = req.body;        
        console.log(reqObj);      
        req.getConnection(function(err, conn){
    if(err)
    {
        console.error('SQL Connection error: ', err);
        return next(err);
    }
    else
    {
        var insertSql = "INSERT INTO s_question SET ?";
        var insertValues = {
        "question" : reqObj.question,
        "sid" : reqObj.sid
    };
        var query = conn.query(insertSql, insertValues, function (err, result){
        if(err){
            console.error('SQL error: ', err);
            return next(err);
        }
        res.true;
    });
    }
    });
    }
    catch(ex){
    console.error("Internal error:"+ex);
    return next(ex);
    }
});

/*get subjects*/
router.get('/get/getsubjects', function(req, res, next) {
    try {
            
            var year = req.param('year');
            var sem = req.param('sem');

        req.getConnection(function(err, conn) {
            if (err) {
                console.error('SQL Connection error: ', err);
                return next(err);
            } else {
                conn.query('select s.sname,s.location,s.time,s.sid,s.enrollk,l.lname from subjects s,lecturers l where s.lid=l.lid and s.year = ? and s.semester = ? ', [year,sem], function(err, rows, fields) {
                    if (err) {
                        console.error('SQL error: ', err);
                        return next(err);
                    }
                    var resEmp = [];
                    for (var empIndex in rows) {
                        var empObj = rows[empIndex ];
                        resEmp .push(empObj);
                    }
                    res.json(resEmp);
                });
            }
        });
    } catch (ex) {
        console.error("Internal error:" + ex);
        return next(ex);
    }
});


