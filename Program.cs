using System.Text.RegularExpressions;
using ClassroomStart.Models;
using Microsoft.EntityFrameworkCore;


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
    Console.WriteLine(checkingID);


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
            admin = Console.ReadLine().Trim();
            if (admin == passCode)
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
                            Console.WriteLine("\nYou are in the Add Inventory Section.");

                           using (DatabaseContext context = new DatabaseContext())
                           {

                                int updateQuantity = 0;
                                int tempProductID = 0;
                                int tempQuantityInStock = 0;
                                string tempProductName = "";
                                int updatedQuantityOnHand = 0;

                                Console.WriteLine("The following is a list of products in stock.");
                                
                                foreach(Product product in context.Products.ToList())
                                {
                                   Console.WriteLine("\t\n Product ID Number: " + product.ProductID + "\t Product Name: " + product.ProductName + "\t Quantity Currently in Stock: "+ product.QuantityInStock);
                                 //Console.WriteLine("{0, 10} {1, 30} {2, 10:C2} {3, 10}", product.ProductID, product.ProductName, product.SalePrice, product.QuantityInStock);

                                }

                                Console.WriteLine("\n Please select a Product ID number from the list above to update: ");

                                try 
                                {          
                                
                                    tempProductID = int.Parse(Console.ReadLine().Trim());
                                    tempQuantityInStock = context.Products.Where(x => x.ProductID == tempProductID).Single().QuantityInStock;
                                    tempProductName = context.Products.Where(x => x.ProductID == tempProductID).Single().ProductName;
                               
                                    Console.WriteLine("You entered "+ tempProductID);

                                    Console.WriteLine("How many units would you like to add to the " +tempQuantityInStock + " of the " + tempProductName + " you currently have in stock?");

                                    updateQuantity = int.Parse(Console.ReadLine());
                                    
                                    updateQuantity += tempQuantityInStock;

                                    context.Products.Where(x => x.ProductID == tempProductID).Single().QuantityInStock = updateQuantity;
                                    context.SaveChanges();

                                     updatedQuantityOnHand = context.Products.Where(x => x.ProductID == tempProductID).Single().QuantityInStock;

                                    Console.WriteLine("The database has been successfully updated. There are now " + updatedQuantityOnHand + " " + tempProductName + " units in inventory.");
                                }

                                catch( Exception ex)
                                {
                                    Console.WriteLine("Sorry, an error occurred updating the database. " + ex.Message);
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


