# ASP.NET Core App for Bloggers
A full-featured ASP.NET Core application for bloggers where users can post different articles. The application consists of 3 parts: API back-end, DTO library to share data between dependent components, and a web front-end application.

## Setup
1. In Visual Studio, select the Tools -> NuGet Package Manager -> Package Manager Console
2. For each project run the following commands in the Package Manager Console

Note: After "Add Migration Initial" command go to "Migrations\initial.cs" file in back-end project and change "onDelete: ReferentialAction.Cascade" to "onDelete: ReferentialAction.NoAction" in Comment table for "FK_Comments_Bloggers_BloggerId" to resolve a constraint error. 

```console
   Add-Migration Initial
   Update-Database
   ```

### Application Architecture
![Architecture Diagram](/docs/awesomeblog-architecture-diagram.jpg)

### Database Schema
![Database Schema Diagram](/docs/awesomeblog-db-diagram.jpg)
