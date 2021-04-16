function AddTeacher(){
	
	var URL = "http://localhost:51654/api/TeacherData/AddTeacher";
	
	var rq = new XMLHttpRequest();
	
	var FirstName = document.getElementById('firstname').value;
	var LastName = document.getElementById('lastname').value;
	var Salary = document.getElementById('salary').value;
	
	var TeacherData = {
		"TeacherFname": FirstName,
		"TeacherLname": LastName,
		"Salary": Salary
	
	};
	
	
	if(!FirstName){
	alert('All form fields are required!');
			return false;
			
		}
		
	if(!LastName){
	alert('All form fields are required!');		
			return false;
		}
		
	if(!Number.parseInt(Salary)){	
	alert('All form fields are required! Please add a number to Salary field!');
			return false;
		}
		
	
		
	console.log (TeacherData.TeacherFname);
	console.log (TeacherData.TeacherLname);
	console.log (TeacherData.Salary);
	
	document.getElementById('FirstName').innerHTML = TeacherData.TeacherFname;
	document.getElementById('LastName').innerHTML = TeacherData.TeacherLname;
	document.getElementById('Salary').innerHTML = TeacherData.Salary;
	
	
	
	
	
	document.getElementById('confirmation').style.display = "block";
	
	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		
		if (rq.readyState === 4 && rq.status === 200) {
			
		}

	}
	
	rq.send(JSON.stringify(TeacherData));

}