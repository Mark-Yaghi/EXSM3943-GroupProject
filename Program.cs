
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
var customerList = new List<string>();
var productIDList = new List<string>();
var cartList = new List<string>();
string customerListFirstName = "";
string customerListLastName = "";
string customerListID = "";
do
{
    Console.WriteLine("1) Enter \"1\" to Make Purchase \n2) Enter \"2\" For Admin Login \n3) Enter \"0\" to Quit");
    Console.Write("Please select option: ");
    userChoice = Console.ReadLine().Trim();
    switch (userChoice)
    {
        case "1":
            Console.WriteLine("\nWelcome!");
            Console.Write("Enter your First Name: ");
            userFirstName = Console.ReadLine().Trim();
            Console.Write("Enter your Last Name: ");
            userLastName = Console.ReadLine().Trim();
            Console.WriteLine("");
            using (DatabaseContext context = new DatabaseContext())
            {
                updateCustLocalListVar();
                try
                {
                    shoppingCart(customerListFirstName, customerListLastName);
                    if (userFirstName != customerListFirstName && userLastName != customerListLastName)
                    {
                        Console.WriteLine($"Sorry, couldn't find {userFirstName} {userLastName} in the database. Would you like to be added in database?");
                        Console.Write("To create New User Select 'Y' for Yes / 'N' for No: ");
                        userChoice = Console.ReadLine().ToUpper().Trim();
                        switch (userChoice)
                        {
                            case "Y":
                                bool something = true;
                                if (something)
                                {
                                    phoneNumber = getValidation("Please enter your Phone Number: ", @"^[2-9][\d]{9}$");
                                    address = getValidation("Please enter your Address: ", @"^[A-Za-z\d#][\w\s.,-]{1,50}$");
                                    context.Customers.Add(new Customer(userFirstName, userLastName, address, phoneNumber) { });
                                    context.SaveChanges();
                                    Console.WriteLine($"\nWelcome, New user ({userFirstName} {userLastName}) is created...");
                                    updateCustLocalListVar();
                                    shoppingCart(customerListFirstName, customerListLastName);
                                }
                                break;
                            case "N":
                                break;
                            default:
                                Console.WriteLine("Invalid selection");
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
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
                Console.Clear();
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
        if (Decimal.TryParse(inputValue, out deciVal))
        {
            output = deciVal;
            isValid = true;
        }
        else
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
        Console.WriteLine("{0, 10} {1, 30} {2, 10:C2} {3, 15}\n", "ProductID", "Name", "Price", "Qty Avail.");
        foreach (Product product in context.Products.ToList().Where(x => !(x.Discontinued == true && x.QuantityInStock == 0)))
        {
            productIDList.Add(product.ProductID.ToString());
            Console.WriteLine("{0, 10} {1, 30} {2, 10:C2} {3, 15}", product.ProductID, product.ProductName, product.SalePrice, product.QuantityInStock);
        }

    }
}

void shoppingCart(string custListFN, string custListLN)
{
    using (DatabaseContext context = new DatabaseContext())
    {
        if (userFirstName == custListFN && userLastName == custListLN)
        {
            bool breakLoop = false;
            do
            {
                getProductListFromDatabase();

                Console.Write("\nSelect 'A' to add items to the cart. Select 'B' to Exit: ");
                userChoice = Console.ReadLine().ToUpper().Trim();
                switch (userChoice)
                {
                    case "A":
                        Console.WriteLine("Shopping cart");
                        do
                        {
                            Console.Write("Please type productID to Add Item in Cart: ");
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
                                    Console.WriteLine("");
                                    foreach (var prodList in cartList) Console.WriteLine(prodList);
                                }
                                else
                                {
                                    Console.WriteLine("Sorry, insufficient Quantity.");
                                }


                                Console.Write("Press 'Enter' to add another item or 'Q' to Quit: ");
                                if (Console.ReadLine().ToUpper().Trim() != "Q")
                                {
                                    breakLoop = false;
                                }
                                else
                                {
                                    Console.WriteLine("{0, 30} {1, 15} {2, 15:C2}\n", "Name", "Qty", "Price");
                                    double sum = 0;
                                    foreach (var prodList in cartList)
                                    {
                                        sum += Convert.ToDouble(prodList.Split("|")[2].Split(":")[1]);
                                        Console.WriteLine("{0, 30} {1, 15} {2, 15:C2}", prodList.Split("|")[0].Split(":")[1], prodList.Split("|")[1].Split(":")[1], Convert.ToDouble(prodList.Split("|")[2].Split(":")[1]));
                                    }
                                    decimal total = (decimal)sum;
                                    Console.WriteLine("{0, 30} {1, 15} {2, 15:C2}", "", "Total:", sum);
                                    Console.WriteLine("\nThanks for shopping!!!");
                                    cartList.Clear();
                                    breakLoop = true;
                                    //userChoice = "0";
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
                        Console.WriteLine("Worng selection! Please choose 'A' for shopping cart, 'B' to exit.");
                        break;
                }
            } while (!breakLoop);
            //Console.Clear();
        }
    }
}

void updateCustLocalListVar()
{
    using (DatabaseContext context = new DatabaseContext())
    {
        foreach (Customer customer in context.Customers.ToList()) customerList.Add($"{customer.FirstName}|{customer.LastName}|{customer.CustomerID}");
        foreach (var item in customerList)
        {
            if (item.Split("|")[0].Contains(userFirstName) && item.Split("|")[1].Contains(userLastName))
            {
                customerListFirstName = item.Split("|")[0];
                customerListLastName = item.Split("|")[1];
                customerListID = item.Split("|")[2];
            }
        }
    }
}