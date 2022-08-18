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
do
{
    Console.WriteLine("1) Enter \"1\" to Make Purchase \n2) Enter \"2\" For Admin Login \n3) Enter \"0\" to Quit");
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
                        bool breakLoop = false;
                        do
                        {
                            Console.WriteLine("{0, 10} {1, 30} {2, 10:C2} {3, 10}\n", "ProductID", "Name", "Price", "QIS");
                            foreach (Product product in context.Products.ToList())
                            {
                                productIDList.Add(product.ProductID.ToString());
                                //Console.WriteLine($"{product.ProductID} {product.ProductName} {product.SalePrice} {product.QuantityInStock}");
                                Console.WriteLine("{0, 10} {1, 30} {2, 10:C2} {3, 10}", product.ProductID, product.ProductName, product.SalePrice, product.QuantityInStock);
                            }

                            Console.Write("Select 'a' add items to the cart. Select 'b' to Exit: ");
                            userChoice = Console.ReadLine().ToUpper().Trim();
                            switch (userChoice)
                            {
                                case "A":
                                    Console.WriteLine("Shopping cart");
                                    Console.Write("Add Item: ");
                                    string addItem = Console.ReadLine().ToUpper().Trim();

                                    if (productIDList.Contains(addItem))
                                    {
                                        string quantity = getValidation("Quantity (min 1 max 99 per item) : ", @"^[\d]{1,2}$");
                                        int itemIntValue = getIntValue(addItem);
                                        int quantityIntValue = getIntValue(quantity);
                                        var itemName = context.Products.Where(x => x.ProductID == itemIntValue).Single().ProductName;
                                        var newQIH = context.Products.Where(x => x.ProductID == itemIntValue).Single().SellProduct(quantityIntValue);
                                        Console.WriteLine($"{itemName} : {newQIH}");
                                        //try
                                        //{
                                        //    foreach (Product product in context.Products.ToList())
                                        //    {
                                        //        Console.WriteLine(product);
                                        //        //context.SaveChanges();
                                        //    }
                                        //    //var itemName = context.Products.Where(x => x.ProductID == itemIntValue).Single().ProductName;
                                        //    //var newQIH = context.Products.Where(x => x.ProductID == itemIntValue).Single().SellProduct(quantityIntValue);
                                        //    //Console.WriteLine(itemName + "; ");
                                        //}
                                        //catch (Exception ex)
                                        //{
                                        //    Console.WriteLine(ex.Message);
                                        //}

                                        //foreach (Product product in context.Products.ToList())
                                        //{

                                        //    context.SaveChanges();
                                        //}

                                    }
                                    else
                                    {
                                        Console.WriteLine("Item is notin the list.");
                                    }
                                    break;
                                case "B":
                                    breakLoop = true;
                                    break;
                                default:
                                    Console.WriteLine("Worng selection! Please choose 'a' for shopping cart, 'b' to exit.");
                                    break;
                            }


                            ////int selectedProductID = getIntValue(addItem);
                            ////Console.Write("Quantity: ");
                            //if (productIDList.Contains(addItem)) // || addItem != "EXIT"
                            //{
                            //    Console.WriteLine("Yes!");
                            //}
                            //else
                            //{
                            //    Console.WriteLine("The product is not in the list");
                            //}
                            ////Console.WriteLine(selectedProductID);

                            //string quantity = getValidation("Quantity: ", @"^[\d]{1,2}$");

                            ////var item = context.Products.Where(x => x.ProductID == selectedProductID).Single().ProductName;

                            ////new ItemCart()
                        } while (!breakLoop);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Sorry, couldn't find {userFirstName} {userLastName} in the database. Would you like to be added in database? {ex.Message}");

                    do
                    {
                        Console.WriteLine("1) Yes \n2) No");
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
                                    //Console.WriteLine($"Customer First Name: {userFirstName} \nCustomer Last Name: {userLastName} \nCustomer Address: {address} \nCustomer phone: {phoneNumber}");
                                    context.Customers.Add(new Customer(userFirstName, userLastName, address, phoneNumber) { });
                                    //context.SaveChanges();
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

//public class ItemCart
//{
//    public string ItemName { get; set; }
//    public int ItemNum { get; set; }

//    public ItemCart(string itemName, string itemNum)
//    {
//        ItemName = itemName;
//        ItemNum = itemNum;
//    }
//}

