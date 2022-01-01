<h1 align="center">WPF Library Database Simulator</h1>
<p align="center">This project is a C# WPF demo for a library database simulator. Some of the features added into this demo include full functionality with an SQL database which stores user information (Email Address, Passwords, Books rented etc.) and an account system which allows a variety of users to interact with the application to record book rentings and returns. This applications model is based on existing systems commonly found within libraries.

The purpose of this application is to serve as an example project utilizing WPF and C# to deliver a typical Software Engineering client-requested application. It also serves as an example of an effective way to use an SQL database to store data for a WPF application. This application is supported on Windows OS.</p>

## Screenshots
<p align="center">
  <img alt ="IMAGE OR VIDEO OF APPLICATION" src = "https://user-images.githubusercontent.com/74617187/147849728-3f88ab9d-e3c9-4f7d-8694-4e7e2934f236.png">
</p>
  
## How To Use
The application makes use of a single Microsoft WPF window containing various WPF pages to display different UI states which users can interact with. Users are able to create an account in the application using their Email Address and password; this account will allow users to access many features of the system such as renting a book from the book database, returning one of the books that they are currently renting and extending the duration of a rented book.

All account information including the list of books rented and their due dates are stored within an SQL database, the application retrieves this information appropriately <b>only</b> when connected to the database. For security reasons, on database operations, parameters are always used to diminish the chances of an SQL injection and sensitive information such as Email Addresses and Passwords for accounts are encrypted before being stored on the database.

Since this application is not supported on an external server and is using localhost via Apache, for the application to functionally run on other machines, <b>an exact database structure and naming convention would be required</b> (the structure of the applications database is given at the end of this section).

The information for the database credentials is encrypted and stored on <i>line 4 of the app.config</i> file. For custom database connection strings, it is recommended you obtain the encrypted version of the connection string using the <i>EncryptTextToCipher</i> function contained in <i>DatabaseConnection.cs</i>, then copy in the encrypted string to <i>line 4 of app.config</i>. When starting the application, ensure that MySQL servers are running otherwise the connection will not be made possible.

<b>Application Database Structuring and Naming</b>
- Database Name: librarydatabase
  - Account Table Name: accounts (<b>PK:</b> accID, email, password, name, age)
  - Book Table Name: bookcollection (<b>PK:</b> bookID, bookName, bookAuthor, bookStock, bookCategory)
  - Book Orders Table Name: rentedbookorders (<b>PK:</b> orderID, accID, bookName, rentDate, returnDate)

A video demonstration is available here: https://youtu.be/XH9S7J1GkrA

## Built With
- C#
- Microsoft WPF
- MahApps.Metro

## Contributors

**Adam Howard**
- [Profile] (https://github.com/AdamHoward99)
