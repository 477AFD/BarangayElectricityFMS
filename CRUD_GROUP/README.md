# Barangay Electricity File Manager

- This repository was made for use in our Computer Programming 2 course.
- Its main purpose is to record the electricity bills of individuals in the barangay.

### Installation
- This does not need to be installed.

### Compiling
1. Clone this repository by using git:<br><a href="https://github.com/477AFD/BarangayElectricityFMS.git">https://github.com/477AFD/BarangayElectricityFMS.git</a><br>or using `gh repo clone 477AFD/BarangayElectricityFMS`.
2. Install the following dependency:<br>- Microsoft Visual Studio 2026 with `.NET desktop development` and `Data storage and processing` workloads
3. In the repository folder, double-click the `CRUD_GROUP.sln` file.

> [!WARNING]
> You may need to unblock all the files inside the `CRUD_GROUP` folder. See [this article from MajorGeeks](https://www.majorgeeks.com/content/page/unblock_blocked_files.html) for more information.

4. Included in the repository is the `template-records.mdf` file located beside the `CRUD_GROUP.sln`. Move this file to the `CRUD_GROUP` folder and rename it to `Records.mdf`.
5. Open the `Server Manager` window in Visual Studio by going to View > Server Explorer, click "Connect to Database" (the cylinder with a plug icon).
6. In the `Choose data source` window, click `Microsoft SQL Server Database File` in the Data Sources list.
7. In the `Add Connection` window, in the Database File Name section, click Browse and open the `Records.mdf` file you just renamed earlier.
8. Click OK.
9. In the `Server Manager` section, right-click `Records.mdf` then click Properties.
10. Look for the `ConnectionString` property. Copy the contents inside. It should be similar to this one:
```
Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=<source folder>\CRUD_GROUP\Records.mdf;Integrated Security=True;Encrypt=True
```
11. Open the `DatabaseWorker.cs` class file and at line 22, replace the contents of the `conStr` variable with the connection string you just copied earlier. Be sure to remove `;Encrypt=True` at the end of the connection string!

> [!WARNING]
> Be sure to have double quotation marks around the entire connection string and there should be a semicolon at the end of the statement `;`!<br><br>Otherwise, the program will not compile!

12. Now save the file and press F5 on the keyboard.
13. Try logging it with the default admin account:

```
Username: admin
Password: TLoZ_B0TW32
```

14. Click on `Add/Edit`, then type in the details.
15. If the record is added, that means the connection string works. Now close the window.
16. In line 21 on the `MainForm.cs` (you may need to press F7 to show the code designer), change the value of the line associated with the administrator account with your username and password:
```
21 | KeyValuePair<string, string> adminAccount = new KeyValuePair<string, string>("your_username", "your_password");
```
17. Now press F6 to build the solution (or F5 to test the program).