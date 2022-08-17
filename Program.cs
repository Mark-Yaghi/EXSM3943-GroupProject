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
var items = new List<string>();
do
{
    Console.WriteLine("1) Enter \"1\" Make Purchase\n2) Enter \"2\" For Admin Login\n3) Enter \"0\" to Quit");
    Console.Write("Please select option: ");
    userChoice = Console.ReadLine().Trim();
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
                        foreach (Product product in context.Products.ToList())
                        {
                            Console.WriteLine($"{product.ProductID} {product.ProductName} {product.SalePrice}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sorry, couldn't find {userFirstName} {userLastName}");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Sorry, couldn't find. {ex.Message}");
                    bool something = true;
                    if (something)
                    {
                        phoneNumber = getValidation("Please enter your Phone Number: ", @"^[2-9][\d]{9}$");
                        address = getValidation("Please enter your Address: ", @"^[A-Za-z\d#][\w\s.,-]{1,50}$");
                        Console.WriteLine($"Customer First Name: {userFirstName} \nCustomer Last Name: {userLastName} \nCustomer Address: {address} \nCustomer phone: {phoneNumber}");
                        context.Customers.Add(new Customer(userFirstName, userLastName, address, phoneNumber) { });
                        //context.SaveChanges();
                    }
               
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
                        bool dis = false;

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
                            int sup = 0;
                        using (DatabaseContext context = new DatabaseContext())
                        {
                            try
                            {
                                context.Products.Add(new Product(sup, prodName, description, qis, dis, productPrice)
                                {
                                    // For now the SupplierID is added manually 
                                    SupplierID = -2,
                                    ProductName = prodName,
                                    Description = description,
                                    QuantityInStock = qis,
                                    Discontinued = dis,
                                    SalePrice = productPrice,
                                
                                });
                            }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("ERROR: " + ex.Message);
                                }

                                context.SaveChanges();

                            };
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
        else
        {
                Console.WriteLine("Invalid Password");
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
        if ((int.TryParse(inputValue, out intValue) && intValue > 0))
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

public class ItemCart
{
    public string ItemName { get; set; }
    public int ItemNum { get; set; }

    public ItemCart(string itemName, int itemNum)
    {
        ItemName = itemName;
        ItemNum = itemNum;
    }
}