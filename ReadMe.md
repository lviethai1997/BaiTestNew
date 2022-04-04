<h2>Asp.net MVC 5 project</h2>

1.	Install Tools :</br>
•	.net framework 4.7.2</br>
•	.net 5.0</br>
•	MS 2022</br>
•	SQL Server 2019</br>

2.	How to run project:</br>
•	Clone code from Github : git clone https://github.com/lviethai1997/BaiTestNew</br>
•	Open BaiTestNew.sln in Visual Studio </br>
•	Set startup project Data</br>
•	Change connection string in Appsetting.json and SystemConstants.SQLcnn in Data project</br>
•	Open Tools --> Nuget Package Manager --> Package Manager Console in Visual Studio</br>
•	Run "add-migration CreateDatabase" press enter then run "Update-database and Enter".</br>
•	Set multiple run project: Right click to Solution and choose Properties and set Multiple Project, choose Start for 2 Projects: AdminApplication, WebClientApplication.</br>
•	Choose profile to run or press F5</br>

3.	If you want up project to ISS:</br>
•	Change SystemConstants.baseURL to your URL of AdminApplication in Data project</br>
•	You also have to install .net hosting bundle 5.0 and .net framework 4.7.2 runtime</br>

User and password for testing: </br>
Username : admin </br>
Password: Admin@123</br>

Username : user</br>
Password: Admin@123