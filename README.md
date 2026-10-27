## System Overview

SmartBrew is a coffee vending machine application developed in C# using .NET 8. The project is a console-based prototype where users can create accounts, log in, browse available drinks, customize their orders, and make payments.

The system also allows customers to save their favorite recipes for future orders. Technicians have a separate menu where they can manage ingredients, refill stock, and control the machine.

The goal of the project is to simulate how a smart coffee vending machine works, including both customer features and basic maintenance functions.

Please note: Restarting the program result or deleting orders and customers accounts.

For technician account enter
username: admin
password: admin
Technician account is static

## Environment & Prerequisites

SmartBrew is developed in C# using **.NET 8**. The application runs in the console and does not require any additional software or external services.

To build and run the project, you need:

- **.NET 8 SDK** – Required to build and run the application.
- **Visual Studio 2022** – Recommended for opening, editing, and running the project.
- **Operating System** – Windows is used for development and testing.
- **Libraries** – The project uses standard .NET libraries and does not require any additional NuGet packages.

The project includes a Visual Studio solution file (`SmartBrewMachine.sln`), which can be opened directly in Visual Studio.

Visual Studio is not required to only run it. You can run in the console by opening cmd and going to the project folder.

When you are in the project folder do "cd .\SmartBrewMachine\" --> "dotnet build" --> "dotnet run". 
This should run the SmartBrew coffee vending mechine without donwnloading visual studio.

## Testing the 10 Functionalities

Expected the program is running and you are in the Main Menu

1. Register Account

Select Register  menu. Enter a username and password to create a new account.

Keypresses: 1 → username → password

2. Login

Select Login and enter the username and password of an existing account.

Keypresses: 2 → username → password

For technician account enter
username: admin
password: admin

3. Browse Drinks

Browse Drinks to see the available drinks and their prices.

Keypresses: 3 (Not logged in)

Log in as a customer and select Browse Drinks to see the available drinks and their prices.

Keypresses: 1 (Logged in)

4. Customize Drink

Select a drink and customize it by choosing options such as size, sugar, and milk.

Keypresses: 3 → 1-5 → 2 → 1-3 → 0-3 → y/n (Not logged in)

Keypresses: 1 → 1-5 → 2 → 1-3 → 0-3 → y/n (Logged in)

5. Place Order / Record Order

Select a drink and place an order. The system creates an order with the selected drink and total price.

Keypresses: 3 → 1-5 → 1 → 1 (Not logged in)

Keypresses: 1 → 1-5 → 1 → 1 (Logged in)

6. Payment Processing

After placing an order, select a payment method and complete the simulated payment.

Keypresses: 3 → 1-5 → 1 → 1 → 1-2 → 1 (Not logged in)

Keypresses: 1 → 1-5 → 1 → 1 → 1-2 → 1 (Logged in)

Payment methods:

1 = Card

2 = Swish

7. Save Recipes

Select a drink and save its recipe. The saved recipe can be accessed later from the customer menu.

Keypresses: 1 → 1-5 → 1 → 2 (Logged in only)

8. Generate Receipt

After completing a payment, the system generates a receipt showing the order details and total price.

Keypresses: 3 → 1-5 → 1 → 1 → 1-2 → 1 (Not logged in)

Keypresses: 1 → 1-5 → 1 → 1 → 1-2 → 1 (Logged in)

9. Ingredient Level Monitoring

Log in as a technician and select Check Ingredient Level to view the available ingredients and their stock levels.

Keypresses: 2 → admin → admin

10. Ingredient Refill

Log in as a technician, open Manage Machine, and select Refill Ingredients to update the ingredient stock levels.

Keypresses: 2 → admin → admin → 2 → 4 → 1-5 → 1
