# Drivers Vehicles & Licenses Department (DVLD)

## Overview
Drivers Vehicles & Licenses Department (DVLD) is a desktop application that simulates a traffic department system for managing driver licensing workflows.  
It helps replace manual, paper-based procedures with a centralized and automated data system.

## Objective
- Organize traffic department operations
- Replace paper-based workflows with automation
- Provide a centralized database for managing licensing data

## Target Users
- Traffic departments
- Licensing authorities
- Any organization that provides driver licensing services

## Features
- Apply for a local driving license and select the required category
- Issue an international driving license
- Suspend a local license in case of violations
- Reinstate a suspended Local license
- Renew an expired local license
- Issue a replacement for a damaged local license

## Tech Stack
- **UI:** WinForms
- **Data Access:** ADO.NET
- **Database:** Microsoft SQL Server
- **Architecture:** 3-Tier Architecture

## Requirements
- Visual Studio 2015 or later
- .NET Framework 4.6
- Microsoft SQL Server 2019 or later

> If you have an Older SQL Server installed, contact me to get the database as a script.

## Setup & Installation

1. Restore the database:
   - Open Microsoft SQL Server Management Studio
   - Restore `DVLD.bak`
   - If restore does not work, use the database script version

2. Extract the project files:
   - Unzip `DVLD Project.rar`

3. Open the solution:
   - Run `My DVLD Project.sln`
   - Or open it through Microsoft Visual Studio

4. Build the project:
   - Do not open any file before building the project
   - Perform a build first to avoid errors

5. Update global file paths:
   - Open `Presentation Layer > Global Classes > clsGlobal.cs`
   - Update:
     - `FolderPath` Used for storing user/person images
     - `FilePath` Used for storing user login credentials
   - Adjust both paths according to your machine

6. Update the database connection string:
   - Open `DataAccessLayer > clsDataAccessSettings.cs`
   - Edit `connectionString` according to your SQL Server credentials

7. Build and run the project

## Default Login
- **Username:** `AKD`
- **Password:** `0000`

> Note: In Case You Need Help With The Project, You Can Contact me.
