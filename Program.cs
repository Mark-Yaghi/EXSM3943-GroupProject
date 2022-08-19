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
                            // Console.WriteLine("Quantity: ");
                            quantity = InputNumberFn("Quantity: ");

                            // String prodPrice = getValidation("Price of Product: \n", @"^[1-9][\d]{0,4}\.?([\d]?){0,2}( )?$" );
                            productPrice = DecimalInputNumberFn("Price of Product: ");
                     
                            using (DatabaseContext context = new DatabaseContext())
                            {
                                Console.WriteLine("\nSuppliers: ");
                                foreach (Supplier supplier in context.Supplier.ToList())
                                {
                                    Console.WriteLine(supplier.CompanyName + " " + supplier.SupplierID + " ID");
                                }
                                // Console.WriteLine("Select the supplier ID:");
                                suppliersID = InputNumberFn("Select the supplier ID: ");
                               
                                suppliersName = context.Supplier.Where(x => x.SupplierID == suppliersID).Select(x => x.CompanyName).FirstOrDefault();

                                try
                                {
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
                                do
                                {

                                    Console.WriteLine("\n" + "Product: " + prodName + "\n" + "Description: " + description + "\n" + "Quantity: " + quantity + "\n" + "$" + productPrice + "\n" + "Supplier: " + suppliersName + "\n");
                                    Console.WriteLine("Please confirm you would like add this Product: Yes || No  ");
                                    addConfrimation = Console.ReadLine().Trim();
                                    switch (addConfrimation)
                                    {
                                        case "Yes":
                                            context.SaveChanges();
                                            Console.WriteLine(prodName + "Has been added to the Inventory\n");
                                            break;

                                        case "No":
                                            break;

                                        default:
                                            Console.WriteLine("Invalid entry");
                                            break;
                                                
                                    }

                                } while (addConfrimation != "No");

                            };
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

                                foreach (Product product in context.Products.ToList())
                                {
                                    Console.WriteLine("\t\n Product ID Number: " + product.ProductID + "\t Product Name: " + product.ProductName + "\t Quantity Currently in Stock: " + product.QuantityInStock);
                                    // Console.WriteLine("\n{0, 3} {1, 30} {2, 60}", "Product ID: " +product.ProductID, "Product Name:" +product.ProductName, "Quantity Currently In /Stock: " + product.QuantityInStock);

                                }

                                Console.WriteLine("\n Please select a Product ID number from the list above to update: ");

                                try
                                {

                                    tempProductID = int.Parse(Console.ReadLine().Trim());
                                    tempQuantityInStock = context.Products.Where(x => x.ProductID == tempProductID).Single().QuantityInStock;
                                    tempProductName = context.Products.Where(x => x.ProductID == tempProductID).Single().ProductName;

                                    Console.WriteLine("You entered " + tempProductID);

                                    Console.WriteLine("How many units would you like to add to the " + tempQuantityInStock + " units of " + tempProductName + " you currently have in stock?");

                                    updateQuantity = int.Parse(Console.ReadLine());

                                    updateQuantity += tempQuantityInStock;

                                    context.Products.Where(x => x.ProductID == tempProductID).Single().QuantityInStock = updateQuantity;
                                    context.SaveChanges();

                                    updatedQuantityOnHand = context.Products.Where(x => x.ProductID == tempProductID).Single().QuantityInStock;

                                    Console.WriteLine("The database has been successfully updated. There are now " + updatedQuantityOnHand + " " + tempProductName + " units in inventory.");
                                }

                                catch (Exception ex)
                                {
                                    Console.WriteLine("Sorry, an error occurred updating the database. " + ex.Message);
                                }

                            }

                            break;
                        case "C":
                            Console.WriteLine("Products to Discontinue.");

                            bool itemsToDiscontinue = true;
                            int itemToSelect = 0;
                            string confrim = "";
                            bool validator = false;
                            string enterInput = "";
                            using (DatabaseContext context = new DatabaseContext())
                            {
                                foreach (Product product in context.Products.ToList().Where(x => x.Discontinued == false))
                                {
                                    Console.WriteLine(product.ProductID + "ID " + " Product Name: " + product.ProductName + " Quantity In Stock: " + product.QuantityInStock);
                                }
                              
                                do
                                {

                                    Console.WriteLine("Would you like to discontinue a product: Yes || No ");
                                    confrim = Console.ReadLine().Trim();

                                    switch (confrim)
                                    {
                                        case "Yes":
                                            try
                                            {
                                                foreach (Product product in context.Products.ToList().Where(x => x.Discontinued == false))
                                                {
                                                    Console.WriteLine("Product ID: " + product.ProductID  + " Product Name: " + product.ProductName + " Quantity In Stock: " + product.QuantityInStock + "   Discontinued? " + product.Discontinued);
                                                }

                                                itemToSelect = InputNumberFn("\nPlease enter the Product you would like to discontinue by the ID #: \n");
                                                context.Products.Where(x => x.ProductID == itemToSelect).Single().Discontinued = true;

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine("ERROR: " + ex.Message);
                                            }

                                            context.SaveChanges();
                                            break;

                                        case "No":

                                            break;
                                        default:
                                            Console.WriteLine("Invalid entry");
                                            break;

                                    }

                                } while (confrim != "No");
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
