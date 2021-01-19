# **LibraryManagementApi**

A simple administrative tool for a book library using JWT, notifications about adding/removing/ordering a book using SignalR, CRUD operations with books, authors, genres, orders are implemented with the help of ASP.NET Core Web API.

You can view endpoints via *Swagger*.

You must specify a valid auth token for each request, which returns 401, in Authorization tab, type - Bearer token if Postman is used.

***Note:*** some endpoints are accessible only for users with a specific role (*Reader/Librarian*).

## **Basic project description**
There are 5 main entities: ***Books, Authors, Genres, Orders, Users***, who are assigned a role of a *Reader and Librarian*.

A *Reader* can book a book for a specified date and it becomes unavailable.

A *Librarian* can confirm the order (when the reader takes the book from the library). Then the book becomes borrowed.

The *Librarian* closes the order when the Reader returns the book. The book becomes available again.

***Note:***

* When the book is unavailable, it cannot be modified, deleted, booked by somebody else.

* The availability book status is a book property, the borrow book status is an order property.

#### **If you wish to use the app, follow these steps:**

1. Clone this repo.

2. Run **Update-Database** in Package Manager Console. 
***Note:*** Database will be seeded with users, books (with authors and genres) when you run the app for the first time. 

#### **The app consists of the following modules:**

## **Authorization**

1. To log in use **Auth/Login** endpoint. *You will need to be logged in to use all endpoints regarding books (and the related entities – authors, genres) and orders unless stated otherwise.*

## **Books (CRUD)**
1. To view list of all books with the ability to *filter, sort, and group by author, genre, year*, use **Books/Get all** endpoint. To view only *available* books, specify the respective parameter. You do not have to be authorized for this.
2. To view one book at a time, use **Books/Get** endpoint. You do not have to be authorized for this.
3. To add a new book, use **Books/Post** endpoint. You have to be authorized as *librarian*. You receive a *real-time notification* about this action.
4. To update an existing book, use **Books/Put** endpoint. You have to be authorized as *librarian*.
5. To delete an existing book, use **Books/Delete** endpoint. You have to be authorized as *librarian*. You receive a *real-time notification* about this action.

***Note:*** you cannot edit or delete a book if it is booked.

## **Authors (CRUD)**

***Note:*** You have to be authorized as *librarian* to use all below mentioned endpoints.
1. To view list of all authors, use **Authors/Get all** endpoint. 
2. To view one author at a time, use **Authors/Get** endpoint. 
3. To add a new author, use **Authors/Post** endpoint. 
4. To update an existing author, use **Authors/Put** endpoint. 
5. To delete an existing author, use **Authors/Delete** endpoint. 

***Note:*** you cannot edit or delete an author if there are books related to them.

## **Genres (CRUD)**

***Note:*** You have to be authorized as *librarian* to use all below mentioned endpoints.
1. To view list of all genres, use **Genres/Get all** endpoint. 
2. To view one genre at a time, use **Genres/Get** endpoint. 
3. To add a new genre, use **Genres/Post** endpoint. 
4. To update an existing genre, use **Genres/Put** endpoint. 
5. To delete an existing genre, use **Genres/Delete** endpoint. 

***Note:*** you cannot edit or delete a genre if there are books related to it.

## **Orders (CRUD)**
1. To view list of all orders with the ability to *filter, sort, and group by reader and librarian*, use **Orders/Get all** endpoint. To view only *borrowed* books, specify the respective parameter. 
2. To view one order at a time, use **Orders/Get** endpoint. 
3. To add a new order, use **Orders/Post** endpoint. You have to be authorized as *reader*. You receive a *real-time notification* about this action.
4. To delete an existing order, use **Orders/Delete** endpoint. You have to be authorized as *reader*.

***Note:*** 

* You cannot delete an order since it has been marked as borrowed.
* If a reader has not taken their book on time, it becomes available again and somebody else can borrow it. The outdated order is deleted when somebody else books the book.

5. To confirm an order, use **Orders/Take order** endpoint. You have to be authorized as *librarian*.
6. To close an order, use **Orders/Return order** endpoint. You have to be authorized as *librarian*.

## **Notifications (Using SignalR)**
You can connect to *NotificationsHub* by **{baseUrl}/notifications**. A JS file to test notifications service is available on request.

