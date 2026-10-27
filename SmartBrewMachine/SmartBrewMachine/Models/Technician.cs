using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBrewMachine.Models
{
    public class Technician : User
    {
        public bool IsMachineOnline { get; private set; } = true;


        // Description: Displays the technician menu and its options.
        // Input: List of ingredients.
        // Output: Does not return anything (void).
        public void TechnicianMenu(List<Ingredient> ingredients)
        {
            while (true)
            {
                Console.WriteLine("\n---------------------------------------------\n");
                Console.WriteLine("=== Technician Menu ===");
                Console.WriteLine("1. Check Ingredient Level");
                Console.WriteLine("2. Manage Machine");
                Console.WriteLine("0. Log Out");

                Console.Write("\nChoice: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        CheckIngredientLevels(ingredients);
                        break;

                    case "2":
                        ManageMachine(ingredients);
                        break;

                    case "0":
                        Console.WriteLine("Logged out successfully!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please enter 0, 1, or 2.");
                        break;
                }
            }
        }


        // Description: Displays the machine status and ingredient levels.
        // Input: List of ingredients.
        // Output: Does not return anything (void).
        public void CheckIngredientLevels(List<Ingredient> ingredients)
        {
            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("\n=== Machine Status ===");
            Console.WriteLine($"Machine: {(IsMachineOnline ? "Online" : "Offline")}\n");

            foreach (Ingredient ingredient in ingredients)
            {
                string status = ingredient.Level == 0 ? "EMPTY" : ingredient.Level < 20 ? "LOW STOCK" : "OK";

                Console.WriteLine($"{ingredient.Name}: {ingredient.Level}% - {status}");
            }

            Console.WriteLine("\nPress Enter to go back.");
            Console.ReadLine();
        }


        // Description: Allows the technician to manage the machine and refill ingredients.
        // Input: List of ingredients.
        // Output: Does not return anything (void).
        public void ManageMachine(List<Ingredient> ingredients)
        {
            while (true)
            {
                Console.WriteLine("\n---------------------------------------------\n");
                Console.WriteLine("=== Manage Machine ===");
                Console.WriteLine($"Status: {(IsMachineOnline ? "Online" : "Offline")}");

                Console.WriteLine("\n1. Turn Machine On");
                Console.WriteLine("2. Turn Machine Off");
                Console.WriteLine("3. Restart Machine");
                Console.WriteLine("4. Refill Ingredients");
                Console.WriteLine("0. Go Back");

                while (true)
                {
                    Console.Write("\nChoice: ");
                    string choice = Console.ReadLine() ?? "";

                    switch (choice)
                    {
                        case "1":
                            if (IsMachineOnline)
                                Console.WriteLine("Machine is already online.");
                            else
                            {
                                IsMachineOnline = true;
                                Console.WriteLine("Machine turned on successfully!");
                            }
                            break;

                        case "2":
                            if (!IsMachineOnline)
                                Console.WriteLine("Machine is already offline.");
                            else
                            {
                                IsMachineOnline = false;
                                Console.WriteLine("Machine turned off successfully!");
                            }
                            break;

                        case "3":
                            if (!IsMachineOnline)
                            {
                                Console.WriteLine("Machine is offline. Turn it on first.");
                            }
                            else
                            {
                                IsMachineOnline = false;
                                Console.WriteLine("Restarting machine...");
                                IsMachineOnline = true;
                                Console.WriteLine("Machine restarted successfully!");
                            }
                            break;

                        case "4":
                            RefillIngredients(ingredients);
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please enter 0, 1, 2, or 3.");
                            continue;
                    }

                    break;
                }
            }
        }


        // Description: Allows the technician to refill one or all ingredients.
        // Input: List of ingredients.
        // Output: Does not return anything (void).
        public void RefillIngredients(List<Ingredient> ingredients)
        {
            while (true)
            {
                Console.WriteLine("\n---------------------------------------------\n");
                Console.WriteLine("=== Refill Ingredients ===");

                for (int i = 0; i < ingredients.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ingredients[i].Name} - {ingredients[i].Level}%");
                }

                Console.WriteLine("5. Refill All Ingredients");
                Console.WriteLine("0. Go Back");

                int choice;

                while (true)
                {
                    Console.Write("\nSelect ingredient: ");
                    string input = Console.ReadLine() ?? "";

                    if (input == "0")
                        return;

                    if (int.TryParse(input, out choice) &&
                        choice >= 1 && choice <= 5)
                        break;

                    Console.WriteLine("Invalid option. Try again.");
                }

                // Rfill all
                if (choice == 5)
                {
                    Console.WriteLine("\n1. Confirm Refill All");
                    Console.WriteLine("0. Cancel");

                    while (true)
                    {
                        Console.Write("\nChoice: ");
                        string option = Console.ReadLine() ?? "";

                        if (option == "0")
                            break;

                        if (option == "1")
                        {
                            foreach (Ingredient ingredient in ingredients)
                            {
                                ingredient.Level = 100;
                            }

                            Console.WriteLine("\nAll ingredients refilled successfully!");
                            break;
                        }

                        Console.WriteLine("Invalid option. Please enter 0 or 1.");
                    }

                    continue; // Returns to ingredient list
                }

                // Refill one ingredient
                Ingredient selected = ingredients[choice - 1];

                Console.WriteLine($"\nSelected: {selected.Name}");
                Console.WriteLine($"Current level: {selected.Level}%");

                if (selected.Level == 100)
                {
                    Console.WriteLine("Ingredient is already full.");
                    continue;
                }

                Console.WriteLine("\n1. Confirm Refill");
                Console.WriteLine("0. Cancel");

                while (true)
                {
                    Console.Write("\nChoice: ");
                    string option = Console.ReadLine() ?? "";

                    if (option == "0")
                        break;

                    if (option == "1")
                    {
                        selected.Level = 100;

                        Console.WriteLine($"\n{selected.Name} refilled successfully!");
                        Console.WriteLine("New level: 100%");
                        break;
                    }

                    Console.WriteLine("Invalid option. Please enter 0 or 1.");
                }
            }
        }
    }
}
