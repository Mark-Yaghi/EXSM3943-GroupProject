using System.Text.RegularExpressions;
using ClassroomStart.Models;
using Microsoft.EntityFrameworkCore;
using System.Security;
using Microsoft.Data.SqlClient.Server;


const string passCode = "password";
string admin = "";
const string USER_DATA = "userData.txt";
string userChoice = "";
string userName = "";
string phoneNumber;
string address;

using (DatabaseContext context = new DatabaseContext())
{

    var check = context.Customers.Where(x => x.FirstName == "Tony").Single();
    context.Entry(check).Collection(x => x.Orders).Load();
    int checkingID = check.CustomerID;
    //Console.WriteLine(checkingID);


    /**
      Customer findme = context.Customers.Where(x => x.FirstName == "Tony").Single();
    context.Entry(findme).Reference(x => x.FirstName).Load();
    if (findme.ToString() = "Tony")
    {
        Console.WriteLine("COOL");
    }
   
    context.Entry(findme).Reference(x => x.FirstName).Load();
    Console.WriteLine(findme);
     */
    foreach (Customer customer in context.Customers.ToList())
    {
       
      
    }
    foreach (Order order in context.Orders.ToList())
    {
        context.Entry(order).Reference(x => x.Customer).Load();
       // Console.WriteLine(order.Customer.FirstName);
    }
}

    do
{
    Console.WriteLine("\n1) Enter \"1\" Make Purchase\n2) Enter \"2\" For Admin Login\n3) Enter \"0\" to Quit");
    Console.Write("Please select option: ");
    userChoice = Console.ReadLine().Trim();
    switch (userChoice)
    {
        case "1":

            Console.WriteLine("Enter your Name: ");
            userName = Console.ReadLine().Trim();
            bool success = false;
            try
            {
                using (StreamReader reader = File.OpenText(USER_DATA))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null && (success = success || line.Split("|")[0] == userName)) ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sorry, couldn't find you. {ex.Message}");
            }

            Console.WriteLine(success ? "Successfull found you in database" : "We need further details");

            if (!success)
            {
                phoneNumber = getValidation("Enter your Phone Number (should be 10 digit long): ", @"^[2-9][\d]{9}$");
                address = getValidation("Enter your Address (should be maximum 50 characters long): ", @"^[A-Za-z\d][\w\s.]{1,50}$");
                using (StreamWriter writer = File.AppendText(USER_DATA))
                {
                    writer.WriteLine($"{userName}|{phoneNumber}|{address}");
                }
            }


            break;
        case "2":

            Console.WriteLine("\nPlease Enter Admin Password: ");
            //admin = Console.ReadLine().Trim();
            var pass = string.Empty;
            ConsoleKey key;

            do         // the code below deals with converting the user's input to a password format (asterisks instead of letters.)
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key; // Console.ReadKey(true);

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    // pass.AppendChar(keyInfo.KeyChar);
                    Console.Write("\b\b");
                    pass = pass[0..^1];

                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    //pass.RemoveAt(pass.Length - 1);
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }

            } while (key != ConsoleKey.Enter);

           
            if (pass == passCode)
            {
                do
                {
                    Console.WriteLine("\n \n 1) Add product \"A\" \n 2) Add Inventory\"B\" \n 3) Discontinue the Product \"C\" \n 4) Admin Logout\"Q\" ");
                    userChoice = Console.ReadLine().Trim();
                    switch (userChoice.ToUpper())
                    {
                        case "A":
                          
                                Console.WriteLine("Add Product");





                            break;
                        case "B":
                            Console.WriteLine("\nYou are in the Add Inventory Section.");        // ------The code below deals with updating product inventory.

                           using (DatabaseContext context = new DatabaseContext())
                           {

                                int updateQuantity = 0;
                                int tempProductID = 0;
                                int tempQuantityInStock = 0;
                                string tempProductName = "";
                                int updatedQuantityOnHand = 0;                               
                               

                                Console.WriteLine("The following is a list of products in stock.");


                                foreach (Product product in context.Products.ToList())
                                {

                                    context.Entry(product).Reference(x => x.Supplier).Load();   
                                
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;                    //Change the color of the product id and name to make them stand out.

                                    Console.WriteLine("\n\t Product ID Number: " + product.ProductID + "\n\t Product Name: " + product.ProductName); 

                                    Console.ResetColor();                                       //Reset the color and print the rest of the info in standard font color.

                                    Console.WriteLine("\t Description: " + product.Description + "\n\t Sale Price, each: $" + product.SalePrice + "\n\t Quantity Currently in Stock: " + product.QuantityInStock + "\n\t Supplier ID Number & Name: " + product.Supplier.SupplierID + " / " + product.Supplier.CompanyName);


                                }

                                Console.WriteLine("\n Please select a Product ID number from the list above to update: ");

                                try
                                {
                                    tempProductID = int.Parse(Console.ReadLine().Trim());
                                    tempQuantityInStock = context.Products.Where(x => x.ProductID == tempProductID).Single().QuantityInStock;
                                    tempProductName = context.Products.Where(x => x.ProductID == tempProductID).Single().ProductName;

                                    Console.WriteLine("You entered " + tempProductID);

                                    Console.WriteLine("How many units would you like to add to the " + tempQuantityInStock + " units of " + tempProductName + " you currently have in stock?");

                                    updateQuantity = InputNumberFn("\nPlease enter only numbers.");      //Ensure the user enters only numbers.

                                    bool upDateBool = false;                                   
                                    string verifyUpdate = "" ;

                                    do
                                    {
                                        
                                        if (updateQuantity < -50 || updateQuantity > 100)         // Ensure the user cannot delete more than 50 units or add more than 100 units.
                                        {
                                            Console.WriteLine("Please enter a number between -50 and + 100.");

                                            updateQuantity = InputNumberFn("\nPlease enter only numbers.");
                                        }

                                        else
                                        {
                                           bool confirmUpdate ;
                                            do
                                            {

                                                confirmUpdate = false;
                                                Console.WriteLine("Please confirm you would like to update this product's inventory: Yes || No ");
                                                verifyUpdate = Console.ReadLine().Trim();
                                                switch (verifyUpdate.ToUpper())
                                                {                                                    //Verify the client wants to  update, and didn't get here by mistake.

                                                    case "YES":
                                                        updateQuantity += tempQuantityInStock;

                                                        context.Products.Where(x => x.ProductID == tempProductID).Single().QuantityInStock = updateQuantity;
                                                        context.SaveChanges();

                                                        updatedQuantityOnHand = context.Products.Where(x => x.ProductID == tempProductID).Single().QuantityInStock;

                                                        Console.WriteLine("The database has been successfully updated. There are now " + updatedQuantityOnHand + " " + tempProductName + " units in inventory.");
                                                        upDateBool = true;
                                                        confirmUpdate = true;
                                                        break;

                                                    case "NO":
                                                        //breakout of the switch/do while
                                                        break;

                                                    default:
                                                        Console.WriteLine("Please enter either a 'YES' or a 'NO' only.");
                                                        break;

                                                }
                                            } while (!confirmUpdate && verifyUpdate!= "NO");                                            
                                        }

                                    } while (!upDateBool);
                                }

                                catch (Exception ex)                                            //Tailor the message to the user based on the specific error. 
                                {
                                    if (ex.Message == "Sequence contains no elements")
                                        Console.WriteLine("\nSorry, you entered a Product ID number doesn't exist in the database. Please try another number. " + ex.Message);

                                    else if (ex.Message == "Input string was not in a correct format.")
                                        Console.WriteLine("\nSorry, you entered letters or other characters. Please try entering a Product ID number. " + ex.Message);
                                    else Console.WriteLine("\nSorry, an error occurred updating the database. " + ex.Message);
                                }
                           }

                            break;
                        case "C":
                            Console.WriteLine("Disc Prod.");




                            break;
                       
                        
                        case "Q":
                            break;
                       
                        
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                } while (userChoice.ToUpper() != "Q");

            }
        
            break;
        case "0":
            break;
        default:
            Console.WriteLine("Invalid input. Please try again !!!");
            break;
    }
} while (userChoice != "0");


string getValidation(string prompt, string regEx)
{
    string output = "";
    do
    {
        Console.Write(prompt);
        output = Console.ReadLine().ToUpper().Trim();
    } while (!new Regex(regEx).IsMatch(output));
    return output;
}

//public class User
//{
//    public User(string userName, int phoneNumber)
//    {
//        UserName = userName;
//        phoneNumber = phoneNumber;
//    }

//    public string UserName { get; set; }
//    public int PhoneNumber { get; set; }
//}

int InputNumberFn(string consoleMessage)
{

    bool validator = false;
    int NumberOuput = 0;

    do
    {
        Console.Write(consoleMessage);

        if (int.TryParse(Console.ReadLine().Trim(), out NumberOuput)) validator = true;

        else Console.WriteLine("Invalid entry!!");

    } while (!validator);

    return NumberOuput;
}
