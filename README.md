# Store_Manager_MVVM

This is a group project I worked on with 3 other members in my team during a period where we simulated working in a development team using the Agile method.
We made use of Jira abd planned our sprints trough this tool. Everyone did an equal amount of work on this project but I want to make it clear I did not write all this myself.

The original requirements for this project were something along the line of making a tool (In the MVVM method) for a hardware store to manage data about things like products and clients.
The requirements for this project got changed throughout the development as to simulate a client wanting different features troughout the development of the app to make us make changes to already finished modules.

For ease of use and being able to be tested by different people the database is a local one made by EntityFramework. To get this set up please follow the following steps.

After downloading the files and launching **Type2.sln**.

- you will have to go to the **package manager console**. If this is not visible at the bottom left then you can use the search bar at the top to look for this.
- In the package manager console make sure for default project **dal** is selected and NOT **wpf**.
- Now pate this command in the console to make the database and run the migrations: "Update-Database".
- Now the database is created and you can go find it in the SQL server object explorer. If this is not visible at the left side of the screan please search for it in the search bar.
- in this window you sould see something like **(localdb)\mssqllocaldb**. Under here will be a folder with databases and in this folder will be a database named "Type2". If you do not see this you might have to press the refresh button.
- Right click this database and click "new query".
- In the main folder I added a file called "SQLScript". Copy paste the contents into the new query window and run it.
- Your database should now be ready with some dummy data to test the application.
  
