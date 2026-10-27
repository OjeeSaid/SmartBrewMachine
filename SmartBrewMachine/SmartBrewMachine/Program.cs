using SmartBrewMachine.Models;

class Program
{
    static List<Customer> customers = new List<Customer>
    { 
        new Customer { Username = "test", Password = "test" }
    };

    static Technician technician = new Technician
    {
        Username = "admin",
        Password = "admin"
    };

    static List<Ingredient> ingredients = new List<Ingredient>
    {
        new Ingredient { Name = "Coffee Beans", Level = 80 },
        new Ingredient { Name = "Milk", Level = 65 },
        new Ingredient { Name = "Sugar", Level = 40 },
        new Ingredient { Name = "Chocolate", Level = 15 }
    };

    static List<Recipe> drinks = new List<Recipe>
    {
        new Recipe { Name = "Espresso", Price = 25 },
        new Recipe { Name = "Cappuccino", Price = 35 },
        new Recipe { Name = "Latte", Price = 40 },
        new Recipe { Name = "Americano", Price = 30 },
        new Recipe { Name = "Mocha", Price = 45 }
    };

    static List<Order> orders = new List<Order>();
    static int nextOrderId = 1;

    static void Main()
    {
        Console.WriteLine("\n=== Welcome to SmartBrew Coffee! ===");
        while (true)
        {
            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("\n=== SmartBrew Coffee - Main Menu ===");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Log In");
            Console.WriteLine("3. Browse Drinks");
            Console.WriteLine("0. Exit");

            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n---------------------------------------------");
                    Register();
                    break;

                case "2":
                    Console.WriteLine("\n---------------------------------------------");
                    Login();
                    break;

                case "3":
                    BrowseDrinks();
                    break;

                case "0":
                    Console.WriteLine("\n---------------------------------------------\n");
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
            
        }
    }


    // Description: Allows the user to register a new account.
    // Input: None.
    // Output: Does not return anything (void).
    static void Register()
    {
        Console.WriteLine("\n=== Register ===");
        Console.WriteLine("Enter 0 to go back.\n");

        string username;

        while (true)
        {
            Console.Write("Username: ");
            username = Console.ReadLine() ?? "";

            if (username == "0")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username cannot be empty.");
                continue;
            }

            if (username.Equals(technician.Username, StringComparison.OrdinalIgnoreCase) ||
                customers.Any(c => c.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Username already exists. Try another.");
                continue;
            }

            break;
        }

        string password;

        while (true)
        {
            Console.Write("Password: ");
            password = Console.ReadLine() ?? "";

            if (password == "0")
                return;

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty.");
                continue;
            }

            break;
        }

        Customer newCustomer = new Customer
        {
            Username = username,
            Password = password
        };

        customers.Add(newCustomer);

        Console.WriteLine("\nAccount created successfully!");
    }

 
    // Description: Allows customers and technicians to log in.
    // Input: None.
    // Output: Does not return anything (void).
    static void Login()
    {
        Console.WriteLine("=== Log In ===");
        Console.WriteLine("Enter 0 to go back.\n");

        while (true)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine() ?? "";

            if (username == "0")
                return;

            // Check technician account
            bool isTechnician = username.Equals(technician.Username, StringComparison.OrdinalIgnoreCase);

            // Check customer accounts
            Customer? customer = customers.Find(c => c.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (!isTechnician && customer == null)
            {
                Console.WriteLine("Username not found. Try again.");
                continue;
            }

            while (true)
            {
                Console.Write("Password: ");
                string password = Console.ReadLine() ?? "";

                if (password == "0")
                    return;

                if (isTechnician && password == technician.Password)
                {
                    Console.WriteLine("\nLogged in successfully!");
                    technician.TechnicianMenu(ingredients);
                    return;
                }

                if (customer != null && password == customer.Password)
                {
                    Console.WriteLine($"\nLogged in successfully! Welcome, {customer.Username}!");
                    CustomerMenu(customer);
                    return;
                }

                Console.WriteLine("Incorrect password. Try again.");
            }
        }
    }


    // Description: Displays the customer menu and its options.
    // Input: Customer object.
    // Output: Does not return anything (void).
    static void CustomerMenu(Customer customer)
    {
        while (true)
        {
            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine($"\n=== Customer Menu - {customer.Username} ===");
            Console.WriteLine("1. Browse Drinks");
            Console.WriteLine("2. View Saved Recipes");
            Console.WriteLine("3. Manage Profile");
            Console.WriteLine("0. Log Out");

            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    BrowseDrinks(customer);
                    break;

                case "2":
                    ViewSavedRecipes(customer);
                    break;

                case "3":
                    ManageProfile(customer);
                    break;

                case "0":
                    Console.WriteLine("Logged out successfully!");
                    return;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }


    // Description: Displays the available drinks and lets the user select one.
    // Input: Customer object (optional, null for guests).
    // Output: Does not return anything (void).
    static void BrowseDrinks(Customer? customer = null)
    {
        while (true)
        {
            Console.WriteLine("\n---------------------------------------------\n");
            Console.WriteLine("=== Browse Drinks ===");

            for (int i = 0; i < drinks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {drinks[i].Name} - {drinks[i].Price} SEK");
            }

            Console.WriteLine("0. Go Back");

            Console.Write("\nSelect a drink: ");
            string input = Console.ReadLine() ?? "";

            if (input == "0")
            {
                return;
            }
                
            if (!int.TryParse(input, out int choice) || choice < 1 || choice > drinks.Count)
            {
                Console.WriteLine("Invalid option. Try again.");
                continue;
            }

            Recipe standardDrink = drinks[choice - 1];

            Recipe selectedDrink = new Recipe
            {
                Name = standardDrink.Name,
                Price = standardDrink.Price
            };

            SelectedDrinkMenu(customer, selectedDrink);
        }
    }


    // Description: Lets the user choose either the standard or to customize the drink before moving to next step which is placing a order.
    // Input: Customer object (null for guests) and Recipe object.
    // Output: Does not return anything (void).
    static void SelectedDrinkMenu(Customer? customer, Recipe drink)
    {
        Console.WriteLine("\n---------------------------------------------\n");
        Console.WriteLine($"=== {drink.Name} ===");
        Console.WriteLine($"Price: {drink.Price} SEK");

        Console.WriteLine("\n1. Order Standard Drink");
        Console.WriteLine("2. Customize Drink");
        Console.WriteLine("0. Go Back");

        while (true)
        {
            Console.Write("\nChoice: ");
            string option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    DrinkOptions(customer, drink);
                    return;

                case "2":
                    Recipe customizedDrink = CustomizeDrink(drink);
                    DrinkOptions(customer, customizedDrink);
                    return;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please enter 0, 1, or 2.");
                    break;
            }
        }
    }


    // Description: Lets the user order or save a selected drink.
    // Input: Customer object (null for guests) and Recipe/drink object.
    // Output: Does not return anything (void).
    static void DrinkOptions(Customer? customer, Recipe drink)
    {
        Console.WriteLine("\n---------------------------------------------\n");
        Console.WriteLine("=== Your Drink ===");
        Console.WriteLine($"Drink: {drink.Name}");
        Console.WriteLine($"Size: {drink.Size}");
        Console.WriteLine($"Extra Sugar: {drink.ExtraSugar}");
        Console.WriteLine($"Extra Milk: {(drink.ExtraMilk ? "Yes" : "No")}");
        Console.WriteLine($"Total: {drink.Price} SEK");

        Console.WriteLine("\n1. Place Order");
        Console.WriteLine("2. Save Recipe");
        Console.WriteLine("0. Go Back");

        while (true)
        {
            Console.Write("\nChoice: ");
            string option = Console.ReadLine() ?? "";

            switch (option)
            {
                case "1":
                    if (!technician.IsMachineOnline)
                    {
                        Console.WriteLine("Machine is currently offline. Cannot place order.");
                        break;
                    }
                    Order order = PlaceOrder(customer, drink);

                    string? paymentMethod = Payment(order);

                    if (paymentMethod != null)
                    {
                        order.PaymentMethod = paymentMethod;
                        GenerateReceipt(order);
                    }
                    else
                    {
                        orders.Remove(order);
                        Console.WriteLine("Order cancelled.");
                    }
                    return;

                case "2":
                    if (customer == null)
                    {
                        Console.WriteLine("Please log in to save recipes.");
                    }
                    else
                    {
                        SaveRecipe(customer, drink);
                    }
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please enter 0, 1, or 2.");
                    break;
            }
        }
    }


    // Description: Allows the user to customize a given drink.
    // Input: Recipe/drink object.
    // Output: Returns the customized Recipe/drink object.
    static Recipe CustomizeDrink(Recipe drink)
    {
        Console.WriteLine("\n---------------------------------------------\n");
        Console.WriteLine("=== Customize Drink ===");
        Console.WriteLine($"Drink: {drink.Name}");

        // SIZE
        Console.WriteLine("\nChoose size:");
        Console.WriteLine("1. Small (-5 SEK)");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3. Large (+5 SEK)");

        while (true)
        {
            Console.Write("\nChoice: ");
            string size = Console.ReadLine() ?? "";

            if (size == "1")
            {
                drink.Size = "Small";
                drink.Price -= 5;
                break;
            }
            else if (size == "2")
            {
                drink.Size = "Medium";
                break;
            }
            else if (size == "3")
            {
                drink.Size = "Large";
                drink.Price += 5;
                break;
            }

            Console.WriteLine("Invalid option. Please enter 1, 2, or 3.");
        }

        // EXTRA SUGAR
        Console.WriteLine("\nExtra sugar portions (0-3) (2 SEK each):");

        while (true)
        {
            Console.Write("Choice: ");
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int sugar) &&
                sugar >= 0 && sugar <= 3)
            {
                drink.ExtraSugar = sugar;
           
                drink.Price += sugar * 2;
                break;
            }

            Console.WriteLine("Invalid input. Please enter a number between 0 and 3.");
        }

        // EXTRA MILK
        Console.WriteLine("\nExtra milk? (y/n) +5 SEK if y");

        while (true)
        {
            Console.Write("Choice: ");
            string milk = (Console.ReadLine() ?? "").Trim().ToLower();

            if (milk == "y")
            {
                drink.ExtraMilk = true;
                drink.Price += 5;
                break;
            }
            else if (milk == "n")
            {
                drink.ExtraMilk = false;
                break;
            }

            Console.WriteLine("Invalid input. Please enter y or n.");
        }

        return drink;
    }


    // Description: Creates an order for the selected drink.
    // Input: Customer object (null for guests) and Recipe object.
    // Output: Returns the created Order object.
    static Order PlaceOrder(Customer? customer, Recipe drink)
    {
        Order order = new Order
        {
            Id = nextOrderId++,
            CustomerName = customer?.Username ?? "Guest",
            Drink = drink
        };
        
        orders.Add(order);

        Console.WriteLine("\n---------------------------------------------\n");

        Console.WriteLine("=== Order Created ===");
        Console.WriteLine($"Order ID: {order.Id}");
        Console.WriteLine($"Customer: {order.CustomerName}");
        Console.WriteLine($"Drink: {order.Drink.Name}");
        Console.WriteLine($"Total: {order.Drink.Price} SEK");

        return order;
    }


    // Description: Allows the user to pay for an order.
    // Input: Order object.
    // Output: Returns the payment method as a string or null if cancelled.
    static String? Payment(Order order)
    {
        Console.WriteLine("\n---------------------------------------------\n");
        Console.WriteLine("=== Payment ===");
        Console.WriteLine($"Order ID: {order.Id}");
        Console.WriteLine($"Amount: {order.Drink.Price} SEK");

        Console.WriteLine("\n1. Pay by Card");
        Console.WriteLine("2. Pay by Swish");
        Console.WriteLine("0. Cancel Order");

        string method = "";

        while (true)
        {
            Console.Write("\nChoice: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "0")
                return null;

            if (choice == "1")
            {
                method = "Card";
                break;
            }

            if (choice == "2")
            {
                method = "Swish";
                break;
            }

            Console.WriteLine("Invalid option. Please enter 0, 1, or 2.");
        }

        Console.WriteLine("\n---------------------------------------------\n");
        Console.WriteLine($"Payment method: {method}");
        Console.WriteLine($"Total: {order.Drink.Price} SEK");

        Console.WriteLine("\n1. Confirm Payment");
        Console.WriteLine("0. Cancel");

        while (true)
        {
            Console.Write("\nChoice: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "0")
                return null;

            if (choice == "1")
            {
                order.IsPaid = true;

                Console.WriteLine("\nPayment successful!");
                Console.WriteLine($"Order #{order.Id} has been confirmed.");

                return method;
            }

            Console.WriteLine("Invalid option. Please enter 0 or 1.");
        }
    }


    // Description: Saves a recipe to the customer's saved recipes if it is not already saved.
    // Input: Customer object and Recipe object.
    // Output: Returns true if saved, false if already exists.
    static void SaveRecipe(Customer customer, Recipe drink)
    {
        bool alreadyExists = customer.SavedRecipes.Any(r =>
            r.Name == drink.Name &&
            r.Size == drink.Size &&
            r.ExtraSugar == drink.ExtraSugar &&
            r.ExtraMilk == drink.ExtraMilk &&
            r.Price == drink.Price);

        if (alreadyExists)
        {
            Console.WriteLine("\nYou already have this recipe saved! Try another option.");
            return;
        }

        customer.SavedRecipes.Add(new Recipe
        {
            Name = drink.Name,
            Price = drink.Price,
            Size = drink.Size,
            ExtraSugar = drink.ExtraSugar,
            ExtraMilk = drink.ExtraMilk
        });

        Console.WriteLine("\nRecipe saved successfully!");
    }

    
    // Description: Displays the customer's saved recipes and lets them select one.
    // Input: Customer object.
    // Output: Does not return anything (void).
    static void ViewSavedRecipes(Customer customer)
    {
        while (true)
        {
            Console.WriteLine("\n---------------------------------------------\n");
            Console.WriteLine("=== Saved Recipes ===");

            if (customer.SavedRecipes.Count == 0)
            {
                Console.WriteLine("You have no saved recipes.");
                return;
            }

            for (int i = 0; i < customer.SavedRecipes.Count; i++)
            {
                Recipe recipe = customer.SavedRecipes[i];

                Console.WriteLine($"{i + 1}. {recipe.Name} ({recipe.Size}) - {recipe.Price} SEK");
            }

            Console.WriteLine("0. Go Back");
            Console.Write("\nSelect a recipe: ");

            string input = Console.ReadLine() ?? "";

            if (input == "0")
                return;

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > customer.SavedRecipes.Count)
            {
                Console.WriteLine("Invalid option. Try again.");
                continue;
            }

            Recipe selectedRecipe = customer.SavedRecipes[choice - 1];

            DrinkOptions(customer, selectedRecipe);
        }
    }


    // Description: Allows the customer to view and manage their profile.
    // Input: Customer object.
    // Output: Does not return anything (void).
    static void ManageProfile(Customer customer)
    {
        while (true)
        {
            Console.WriteLine("\n---------------------------------------------\n");

            Console.WriteLine("=== Manage Profile ===");
            Console.WriteLine($"Username: {customer.Username}");

            Console.WriteLine("\n1. Change Username");
            Console.WriteLine("2. Change Password");
            Console.WriteLine("0. Go Back");

            Console.Write("\nChoice: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    ChangeUsername(customer);
                    break;

                case "2":
                    ChangePassword(customer);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please enter 0, 1, or 2.");
                    break;
            }
        }
    }


    // Description: Allows the customer to change their username.
    // Input: Customer object.
    // Output: Does not return anything (void).
    static void ChangeUsername(Customer customer)
    {
        Console.WriteLine("\n---------------------------------------------\n");
        Console.WriteLine("=== Change Username ===");
        Console.WriteLine("Enter 0 to go back.");

        while (true)
        {
            Console.Write("\nNew Username: ");
            string username = Console.ReadLine() ?? "";

            if (username == "0")
                return;

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username cannot be empty.");
                continue;
            }

            if (customers.Any(c => c.Username.Equals(
                username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Username already exists. Try another.");
                continue;
            }

            string oldUsername = customer.Username;
            customer.Username = username;

            // Update the username on the customer's existing orders.
            foreach (Order order in orders)
            {
                if (order.CustomerName == oldUsername)
                    order.CustomerName = username;
            }

            Console.WriteLine("Username changed successfully!");
            return;
        }
    }


    // Description: Allows the customer to change their password.
    // Input: Customer object.
    // Output: Does not return anything (void).
    static void ChangePassword(Customer customer)
    {
        Console.WriteLine("\n---------------------------------------------\n");
        Console.WriteLine("=== Change Password ===");
        Console.WriteLine("Enter 0 to go back.");

        while (true)
        {
            Console.Write("\nCurrent Password: ");
            string currentPassword = Console.ReadLine() ?? "";

            if (currentPassword == "0")
                return;

            if (currentPassword != customer.Password)
            {
                Console.WriteLine("Incorrect password. Try again.");
                continue;
            }

            break;
        }

        while (true)
        {
            Console.Write("\nNew Password: ");
            string newPassword = Console.ReadLine() ?? "";

            if (newPassword == "0")
                return;

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                Console.WriteLine("Password cannot be empty.");
                continue;
            }

            if (newPassword == customer.Password)
            {
                Console.WriteLine("New password must be different.");
                continue;
            }

            customer.Password = newPassword;

            Console.WriteLine("Password changed successfully!");
            return;
        }
    }


    // Description: Displays a receipt for a paid order.
    // Input: Order object.
    // Output: Does not return anything (void).
    static void GenerateReceipt(Order order)
    {
        if (!order.IsPaid)
            return;

        Console.WriteLine("\n---------------------------------------------\n");
        Console.WriteLine("=== SmartBrew Receipt ===");

        Console.WriteLine($"Order ID: {order.Id}");
        Console.WriteLine($"Date: {order.Date}");
        Console.WriteLine($"Customer: {order.CustomerName}");
        Console.WriteLine($"Drink: {order.Drink.Name}");
        Console.WriteLine($"Size: {order.Drink.Size}");
        Console.WriteLine($"Extra Sugar: {order.Drink.ExtraSugar}");
        Console.WriteLine($"Extra Milk: {(order.Drink.ExtraMilk ? "Yes" : "No")}");
        Console.WriteLine($"Payment Method: {order.PaymentMethod}");
        Console.WriteLine($"Total: {order.Drink.Price} SEK");

        Console.WriteLine("\nThank you for your purchase!");
    }


}