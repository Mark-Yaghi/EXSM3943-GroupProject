using System;
using System.Security.Principal;
using ClassroomStart.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;


const string passCode = "password";
string admin = "";
//const string USER_DATA = "userData.txt";
string userChoice = "";
string userFirstName = "";
string userLastName = "";
string phoneNumber;
string address;
var productIDList = new List<string>();
var cartList = new List<string>();
do
{
    Console.WriteLine("1) Enter \"1\" to Make Purchase \n2) Enter \"2\" For Admin Login \n3) Enter \"0\" to Quit");
    Console.Write("Please select option: ");
    userChoice = Console.ReadLine().ToUpper().Trim();
    switch (userChoice)
    {
        case "1":
            Console.Write("Enter your First Name: ");
            userFirstName = Console.ReadLine().Trim();
            Console.Write("Enter your Last Name: ");
            userLastName = Console.ReadLine().Trim();
            using (DatabaseContext context = new DatabaseContext())
            {

                try
                {
                    var userID = context.Customers.Where(x => x.FirstName == userFirstName && x.LastName == userLastName).SingleOrDefault().CustomerID;
                    if (userID != null)
                    {
                        bool breakLoop = false;
                        do
                        {
                            getProductListFromDatabase();

                            Console.Write("Select 'a' to add items to the cart. Select 'b' to Exit: ");
                            userChoice = Console.ReadLine().ToUpper().Trim();
                            switch (userChoice)
                            {
                                case "A":
                                    Console.WriteLine("Shopping cart");
                                    do
                                    {
                                        Console.Write("Add Item to Cart: ");
                                        string addItem = Console.ReadLine().ToUpper().Trim();

                                        if (productIDList.Contains(addItem))
                                        {
                                            string quantity = getValidation("Quantity (min 0 and max 99 per item) : ", @"^[\d]{0,2}$");
                                            int itemIntValue = getIntValue(addItem);
                                            int quantityIntValue = getIntValue(quantity);
                                            var selectedProd = context.Products.Where(x => x.ProductID == itemIntValue).Single();
                                            if (quantityIntValue <= selectedProd.QuantityInStock)
                                            {
                                                var itemName = selectedProd.ProductName;
                                                var productPrice = selectedProd.SalePrice;
                                                var newQIH = selectedProd.SellProduct(quantityIntValue);
                                                context.SaveChanges();
                                                getProductListFromDatabase();
                                                cartList.Add($"ItemName: {itemName} | Quantity: {quantity} | Price: {(productPrice * quantityIntValue).ToString()}");
                                                foreach (var prodList in cartList) Console.WriteLine(prodList);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Sorry, insufficient Quantity.");
                                            }


                                            Console.WriteLine("Press 'Enter' to add another item or 'q' to Quit: ");
                                            if (Console.ReadLine().ToUpper().Trim() != "Q")
                                            {
                                                breakLoop = false;
                                            }
                                            else
                                            {
                                                foreach (var prodList in cartList) Console.WriteLine(prodList);
                                                //foreach (var prodList in cartList)
                                                //{
                                                //    string price = (prodList.Split("|")[2].Split(":")[1]);
                                                //    Console.WriteLine("{ 0:C2}", price);
                                                //}
                                                Console.WriteLine("Thanks for shopping!!!");
                                                cartList.Clear();
                                                breakLoop = true;
                                                userChoice = "0";
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Item is not in the list.");
                                        }
                                    } while (!breakLoop);
                                    break;
                                case "B":
                                    breakLoop = true;
                                    break;
                                default:
                                    Console.WriteLine("Worng selection! Please choose 'a' for shopping cart, 'b' to exit.");
                                    break;
                            }
                        } while (!breakLoop);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Sorry, couldn't find {userFirstName} {userLastName} in the database. Would you like to be added in database? {ex.Message}");

                    do
                    {
                        Console.Write("Select '1' for Yes / '2' for No: ");
                        userChoice = Console.ReadLine().Trim();
                        switch (userChoice)
                        {
                            case "1":
                                bool something = true;
                                if (something)
                                {
                                    phoneNumber = getValidation("Please enter your Phone Number: ", @"^[2-9][\d]{9}$");
                                    address = getValidation("Please enter your Address: ", @"^[A-Za-z\d#][\w\s.,-]{1,50}$");
                                    context.Customers.Add(new Customer(userFirstName, userLastName, address, phoneNumber) { });
                                    context.SaveChanges();
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid selection");
                                break;
                        }
                    } while (userChoice != "2");

                }
            }

            break;
        case "2":

            Console.WriteLine("Please Enter Admin Password: ");
            admin = Console.ReadLine().Trim();
            if (admin == passCode)
            {

                do
                {
                    Console.WriteLine("1) Add product \"A\" 2) Add Inventory\"B\" 3) Discontinue the Product \"C\" 4) Admin Logout\"Q\" ");
                    userChoice = Console.ReadLine().Trim();
                    switch (userChoice)
                    {
                        case "A":
                            string prodName, description;
                            int qis;
                            decimal productPrice;
                            Console.WriteLine("Add Product: ");
                            Console.WriteLine("Product Name: ");
                            prodName = Console.ReadLine().Trim();
                            Console.WriteLine("Desciption of Product: ");
                            description = Console.ReadLine().Trim();
                            Console.WriteLine("Quantity: ");
                            qis = getIntValue(Console.ReadLine().Trim());
                            Console.WriteLine("Price of Product: ");
                            String prodPrice = getValidation("Price of Product: ", @"^[1-9][\d]{0,4}\.?([\d]?){0,2}( )?$");
                            productPrice = getDecimalValue(prodPrice);
                            //using (DatabaseContext context = new DatabaseContext())
                            //{
                            //    context.Products.Add(new Product())
                            //};
                            break;
                        case "B":
                            Console.WriteLine("Add Inven.");
                            break;
                        case "C":
                            Console.WriteLine("Disc Prod.");
                            break;
                        case "Q":
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again !!!");
                            break;
                    }

                } while (userChoice != "Q");

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


int getIntValue(string inputValue)
{
    bool isValid = false;
    int intValue;
    int output = 0;
    do
    {
        if (int.TryParse(inputValue, out intValue))
        {
            output = intValue;
            isValid = true;
        }
        else
        {
            Console.WriteLine($"You have entered invalid number.");
        };
    } while (!isValid);

    return output;
}

decimal getDecimalValue(string inputValue)
{
    bool isValid = false;
    decimal deciVal;
    decimal output = 0m;
    do
    {
        try
        {
            if (Decimal.TryParse(inputValue, out deciVal))
            {
                output = deciVal;
                isValid = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("You have entered invalid decimal value.");
        }
    } while (!isValid);
    return output;
}

void getProductListFromDatabase()
{
    using (DatabaseContext context = new DatabaseContext())
    {
        Console.WriteLine("{0, 10} {1, 30} {2, 10:C2} {3, 10}\n", "ProductID", "Name", "Price", "QIS");
        foreach (Product product in context.Products.ToList())
        {
            productIDList.Add(product.ProductID.ToString());
            Console.WriteLine("{0, 10} {1, 30} {2, 10:C2} {3, 10}", product.ProductID, product.ProductName, product.SalePrice, product.QuantityInStock);
        }

    }
}


