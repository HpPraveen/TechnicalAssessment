# TechnicalAssessment

Create a new ASP.NET Core project using the Visual Studio IDE.

Add the necessary dependencies for ASP.NET Core and Cosmos DB to the project using the NuGet Package Manager.

Create the data models for Invoice and InvoiceLine. 
The Invoice model should include the following properties: Date, Description, TotalAmount, and a collection of InvoiceLines. 
The InvoiceLine model should include the following properties: Amount, Quantity, UnitPrice, and LineAmount.

Create generic repository class and unit of work 

Create DbContext class to interact with Cosmos DB.

Implement the Dependency Injection design pattern by adding the repository class to the project's Startup class and registering it as a service.

Create the InvoiceService class with methods for getting, creating and editing invoices.

Add routes to the InvoiceController methods in the Startup class, so that they can be accessed via HTTP requests.

Test the API using a tool like Swagger or Postman to verify that invoices can be created and edited as expected.

--------------------------------------------------------------------------------------

Test Data

1---------------

{
 
  "date": "2023-02-12T12:37:58.299Z",
  "description": "Test1",
  "totalAmount": 10,
  "invoiceLines": [
    {
      "amount": 10,
      "quantity": 2,
      "unitPrice": 5,
      "lineAmount": 10
    }
  ]
}

2------------

{
  "id": "",
  "date": "2023-02-12T12:37:58.299Z",
  "description": "Test1",
  "totalAmount": 100,
  "invoiceLines": [
    {
      "amount": 50,
      "quantity": 5,
      "unitPrice": 10,
      "lineAmount": 50
    }, {
      "amount": 20,
      "quantity": 1,
      "unitPrice": 20,
      "lineAmount": 20
    }, {
      "amount": 30,
      "quantity": 2,
      "unitPrice": 15,
      "lineAmount": 30
    }
  ]
}