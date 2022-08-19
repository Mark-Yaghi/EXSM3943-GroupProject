using System;
using System.Security.Principal;
using ClassroomStart.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;




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
                        //Console.WriteLine("1) Yes \n2) No");
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
            ///
            Console.WriteLine("Please Enter Admin Password: ");
            const string passCode = "password";
            var pass = string.Empty;
            ConsoleKey key;

            do
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
                //

            {
                do
                {
                    Console.WriteLine("1) Add product \"A\" 2) Add Inventory\"B\" 3) Discontinue the Product \"C\" 4) Admin Logout\"Q\" ");
                    userChoice = Console.ReadLine().Trim();
                    switch (userChoice)
                    {
                        case "A":
                            bool validator = false;
                            string prodName, description;
                            int quantity;
                            decimal productPrice;
                            bool discontinued = false;
                            int suppliersID = 0;
                            string suppliersName = "";

                            Console.WriteLine("Add Product: ");

                            Console.WriteLine("Product Name: ");

                            prodName = Console.ReadLine().Trim();

                            Console.WriteLine("Description of Product: ");

                            description = (Console.ReadLine().Trim());
                      
                            quantity = InputNumberFn("Quantity: ");

                       
                            productPrice = DecimalInputNumberFn("Price of Product: ");

                         

                            using (DatabaseContext context = new DatabaseContext())
                            {

                                Console.WriteLine("\nSuppliers: ");
                                foreach (Supplier supplier in context.Supplier.ToList())
                                {
                                    Console.WriteLine(supplier.CompanyName + " " + supplier.SupplierID + " ID");
                                }
                                do
                                {
                                    suppliersID = InputNumberFn("\nSelect the supplier ID: ");
                                    if (context.Supplier.Any(x => x.SupplierID == suppliersID))
                                    {
                                        validator = true;                              
                                    }
                                    else
                                    {
                                        validator = false;
                                        Console.WriteLine("Product ID NOT FOUND!!");
                                    }

                                } while (!validator);
                               

                                try
                                {
                                    suppliersName = context.Supplier.Where(x => x.SupplierID == suppliersID).Select(x => x.CompanyName).FirstOrDefault();
                                    context.Products.Add(new Product(suppliersID, prodName, description, quantity, discontinued, productPrice)
                                    {
                                        SupplierID = suppliersID,
                                        ProductName = prodName,
                                        Description = description,
                                        QuantityInStock = quantity,
                                        Discontinued = discontinued,
                                        SalePrice = productPrice,

                                    });

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("ERROR: " + ex.Message);
                                }

                                string addConfrimation = "";
                                Console.WriteLine("\n" + "Product: " + prodName + "\n" + "Description: " + description + "\n" + "Quantity: " + quantity + "\n" + "$" + productPrice + "\n" + "Supplier: " + suppliersName + "\n");
                           
                                do
                                {
                                    validator = false;
                                    Console.WriteLine("Please confirm you would like add this Product: Yes || No  ");
                                    addConfrimation = Console.ReadLine().Trim();
                                    switch (addConfrimation)
                                    {
                                        case "Yes":
                                            validator = true;
                                            context.SaveChanges();
                                            Console.WriteLine(prodName + "Has been added to the Inventory\n");
                                            break;

                                        case "No":
                                            break;

                                        default:
                                            Console.WriteLine("Invalid entry");
                                            break;
                                    }

                                } while (!validator && addConfrimation != "No");

                            };
                            break;

                        case "B":
          
                                break;
                        case "C":
                            Console.WriteLine("Products to Discontinue.");

                            bool itemsToDiscontinue = true;
                            int itemToSelect ;
                            string confirm = "";
                             validator = false;
                            string enterInput = "";
                            using (DatabaseContext context = new DatabaseContext())
                            {
                                foreach (Product product in context.Products.ToList().Where(x => x.Discontinued == false))
                                {
                                    Console.WriteLine("Product ID# "+product.ProductID + " Product Name: " + product.ProductName + " Quantity In Stock: " + product.QuantityInStock);
                                }

                                do
                                {
                                    
                                    itemToSelect = InputNumberFn("\nPlease enter the Product you would like to discontinue by the ID #: \n");
                                    var checkingID = (context.Products.Any(x => x.ProductID == itemToSelect));
                                    if (checkingID) 
                                    {
                                       validator = true;
                                   }
                                   else
                                   {
                                        Console.WriteLine("Product ID NOT FOUND!!");
                                   }

                                } while (!validator);
                                do
                                {
                                    Console.WriteLine("Would you like to discontinue a product: Yes || No ");
                                    confirm = Console.ReadLine().Trim();
                                    switch (confirm)
                                    {

                                        case "Yes":
                                            validator = true;
                                            try
                                            {
                                                context.Products.Where(x => x.ProductID == itemToSelect).Single().Discontinued = true;

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("ERROR: " + ex.Message);
                                            }


                                            context.SaveChanges();
                                            string itemsToDiscontinuedName = context.Products.Where(x => x.ProductID == itemToSelect).Select(x => x.ProductName).FirstOrDefault();
                                            Console.WriteLine(itemsToDiscontinuedName + " has now been discontinued");
                                            break;

                                        case "No":

                                            break;
                                        default:
                                            Console.WriteLine("Invalid entry");
                                            break;

                                    }

                                } while (!validator && confirm != "No");


                            }

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

/////
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


decimal DecimalInputNumberFn(string consoleMessage)
{

    bool validator = false;
    decimal NumberOuput = 0;

    do
    {
        Console.Write(consoleMessage);

        if (decimal.TryParse(Console.ReadLine().Trim(), out NumberOuput) && NumberOuput > 0) validator = true;

        else Console.WriteLine("Invalid entry!! Please enter a whole and positive number!");

    } while (!validator);

    return NumberOuput;
}

//////

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
        Console.WriteLine("{0, 10} {1, 30} {2, 10:C2} {3, 10}\n", "ProductID", "Name", "Price", "QIS");
        foreach (Product product in context.Products.ToList())
        {
            productIDList.Add(product.ProductID.ToString());
            Console.WriteLine("{0, 10} {1, 30} {2, 10:C2} {3, 10}", product.ProductID, product.ProductName, product.SalePrice, product.QuantityInStock);
        }

    }
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
