using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using static GreenList.Actions;
using GreenList;


class Driver
{
    static int Main()
    {
        string[] sections = { "Produce", "Meat", "Dairy", "Bakery", "Frozen", "Other" };

        string saveOption = "n";
        string exitOption = "n";
        bool isCreated = false;

        //testing purposes
        if (File.Exists(@"C:\Users\Peter\GreenList\Grocery.db"))
        {
            isCreated = true;
        }

        string cs = @"URI=file:C:\Users\Peter\GreenList\Grocery.db";
        using var con = new SQLiteConnection(cs);
        con.Open();
        using var cmd = new SQLiteCommand(con);
        Actions myActions = new Actions();

        if (!isCreated)
        {
            myActions.createDB(cmd);
        }

        Console.WriteLine("Welcome to Green List!");

        while (exitOption == "n")
        {
            Console.WriteLine("Would you like to create a NEW grocery list or OPEN an existing one?");

            string listOption = Console.ReadLine();

            if (listOption == "NEW")
            {
                Console.WriteLine("Enter a title for this list:");
                string listTitle = Console.ReadLine();

                while (saveOption == "n")
                {
                    string ItemChoice = "y";
                    string mealChoice = "y";

                    Console.WriteLine("Would you like to enter a MEAL or ITEMS?");
                    string mealItemOption = Console.ReadLine();

                    if (mealItemOption != "MEAL")
                    {
                        while (ItemChoice == "y")
                        {

                            Console.WriteLine("Enter an Item:");
                            string itemName = Console.ReadLine();
                            Console.WriteLine("Enter Quantity:");
                            int quantity = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Secton: Produce(1), Meat(2), Dairy(3) Bakery(4), Frozen(5), Other(6):");
                            int section = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Price");
                            float price = float.Parse(Console.ReadLine());

                            myActions.addItems(cmd, itemName, quantity, null, listTitle, sections[section - 1], price);

                            Console.WriteLine("Would you like to ADD another item (y/n)?");
                            ItemChoice = Console.ReadLine();
                        }
                    }
                    else
                    {

                        Console.WriteLine("Enter the title of this meal:");
                        string mealTitle = Console.ReadLine();
                        while (mealChoice == "y")
                        {
                            Console.WriteLine("Enter an Item:");
                            string itemName = Console.ReadLine();
                            Console.WriteLine("Enter Quantity:");
                            int quantity = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Secton: Produce(1), Meat(2), Dairy(3) Bakery(4), Frozen(5), Other(6):");
                            int section = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Price");
                            float price = float.Parse(Console.ReadLine());

                            myActions.addItems(cmd, itemName, quantity, mealTitle, listTitle, sections[section - 1], price);
                            myActions.addMeals(cmd, mealTitle, listTitle, 0);

                            Console.WriteLine("Would you like to ADD another item to this meal (y/n)?");
                            mealChoice = Console.ReadLine();
                        }
                    }


                    Console.WriteLine("Are you ready to save and exit list? (y/n)");
                    saveOption = (Console.ReadLine());
                }


                Console.WriteLine("Enter any notes for this list:");
                string notes = Console.ReadLine();
                Console.WriteLine("List has been saved.");
                string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
                myActions.addList(cmd, listTitle, notes, date, 0);

            }
            else
            {
                Console.WriteLine("Please choose one of the following saved lists:");
                myActions.readLists(cmd);
                string listTitleOp = Console.ReadLine();
                myActions.readListItems(cmd, listTitleOp);

            }
            Console.WriteLine("Would you like to exit? y/n");
            exitOption = Console.ReadLine();
        }

        myActions.readItems(cmd);

        return 0;
    }

}


