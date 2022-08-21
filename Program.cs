using System;
using System.Security.Principal;
using ClassroomStart.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;




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
    Console.Write("\nPlease select option: ");

    userChoice = Console.ReadLine().Trim();
    Console.WriteLine("");
    switch (userChoice)
    {
        case "1":
            Console.Clear();
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
                   // pass.Remove(pass.Length - 1);
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }

            } while (key != ConsoleKey.Enter);

            if (pass == passCode)
            //

            {
                do
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\n\tPRODUCT MENU");
                    Console.ResetColor();

                    Console.WriteLine("\t 1) Add product \"A\" \n\t 2) Add Inventory \"B\" \n\t 3) Discontinue the Product \"C\" \n\t 4) Update Product Name \"D\" \n\t 5) Update Product Description \"E\"\n\t 6) Update Product Sale Price \"F\" \n\t 7) View Product List \"G\"");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\n\tSUPPLIER MENU");
                    Console.ResetColor();

                    Console.WriteLine("\t 8) Update Supplier Info \"H\"");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n\n\tADMIN MENU");
                    Console.ResetColor();

                    Console.WriteLine("\t 9) Admin Logout \"Q\"");


                   // Console.WriteLine("\n \n\tPRODUCT MENU  \n\t 1) Add product \"A\" \n\t 2) Add Inventory \"B\" \n\t 3) Discontinue the Product \"C\" \n\t 4) Update Product Name \"D\" \n\t 5) Update Product Description \"E\" \n\n\tSUPPLIER MENU \n\t 6) Update Supplier Info \"F\"\n\n\tADMIN MENU \n\t 7) Admin Logout \"Q\" ");
                    Console.WriteLine("\nPlease select the letter next to the menu item you want in the menu above.");
                    


                    userChoice = Console.ReadLine().Trim();

                    switch (userChoice.ToUpper())
                    {
                        case "A":
                            Console.Clear();
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
                                    Console.WriteLine("Please confirm you would like add this Product: YES || NO  ");
                                    addConfrimation = Console.ReadLine().Trim();
                                    switch (addConfrimation.ToUpper())
                                    {
                                        case "YES":
                                            validator = true;
                                            context.SaveChanges();
                                            Console.WriteLine(prodName + "Has been added to the Inventory\n");
                                            break;

                                        case "NO":
                                            break;

                                        default:
                                            Console.WriteLine("Invalid entry");
                                            break;
                                    }

                                } while (!validator && addConfrimation != "NO");
                          
                            };
                            break;

                        case "B":
                            Console.Clear();
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
                                    string verifyUpdate = "";

                                    do
                                    {

                                        if (updateQuantity < -50 || updateQuantity > 100)         // Ensure the user cannot delete more than 50 units or add more than 100 units.
                                        {
                                            Console.WriteLine("Please enter a number between -50 and + 100.");

                                            updateQuantity = InputNumberFn("\nPlease enter only numbers.");
                                        }

                                        else
                                        {
                                            bool confirmUpdate;
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
                                            } while (!confirmUpdate && verifyUpdate != "NO");
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
                            Console.Clear();
                            Console.WriteLine("Products to Discontinue.");

                            bool itemsToDiscontinue = true;
                            int itemToSelect;
                            string confirm = "";
                            validator = false;
                            string enterInput = "";
                            using (DatabaseContext context = new DatabaseContext())
                            {
                                foreach (Product product in context.Products.ToList().Where(x => x.Discontinued == false))
                                {
                                    Console.WriteLine("Product ID# " + product.ProductID + " Product Name: " + product.ProductName + " Quantity In Stock: " + product.QuantityInStock);
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
                                    Console.WriteLine("Would you like to discontinue a product: YES || NO ");
                                    confirm = Console.ReadLine().Trim();
                                    switch (confirm.ToUpper())
                                    {

                                        case "YES":
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

                                        case "NO":

                                            break;
                                        default:
                                            Console.WriteLine("Invalid entry");
                                            break;

                                    }

                                } while (!validator && confirm != "NO");

                            }

                            break;


                        case "D":
                            Console.WriteLine("You are in the Update Product Name section.");
                            int tempProdID = 0;

                            using (DatabaseContext context = new DatabaseContext())
                            {
                                Console.WriteLine("Below is a list of all current products in inventory:");

                                foreach (Product product in context.Products.ToList())
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("\n\t Product ID Number: " + product.ProductID + "\n\t Product Name: " + product.ProductName);
                                    Console.ResetColor();
                                    Console.WriteLine("\t Product Description: " + product.Description);
                                }

                                Console.WriteLine("");
                                tempProdID = InputNumberFn("\nPlease select the ID number of the product whose name you wish to change from the list above (Please enter only numbers):");
                                try
                                {
                                   // bool upDateBool = false;
                                    bool tempIDInputExist = false;
                                    string verifyUpdate = "";
                                    int verifyInputID = context.Products.Where(x => x.ProductID == tempProdID).Select(x => x.ProductID).FirstOrDefault();

                                    do
                                    {

                                         verifyInputID = context.Products.Where(x => x.ProductID == tempProdID).Select(x => x.ProductID).FirstOrDefault();


                                        if (tempProdID != verifyInputID)
                                        {
                                            Console.WriteLine("Sorry, that number does not exist in the database");
                                            tempIDInputExist = false;
                                            Console.WriteLine("");
                                            tempProdID = InputNumberFn("\nPlease select the ID number of the product whose name you wish to change from the list above (Please enter only numbers):");

                                        }
                                        else
                                        {

                                            string upDateProdName = "";
                                            Console.WriteLine("Please enter the new name of the product: ");
                                            upDateProdName = Console.ReadLine().Trim();

                                            bool confirmUpdate;
                                            do
                                            {
                                                //string updatedQuantityOnHand = "";
                                                confirmUpdate = false;
                                                Console.WriteLine("Please confirm you would like to update this product's name to " + upDateProdName + ": Yes || No ");
                                                verifyUpdate = Console.ReadLine().Trim();
                                                switch (verifyUpdate.ToUpper())
                                                {                                                    //Verify the client wants to  update, and didn't get here by mistake.

                                                    case "YES":

                                                        context.Products.Where(x => x.ProductID == tempProdID).Single().ProductName = upDateProdName;
                                                        context.SaveChanges();

                                                        //updatedQuantityOnHand = context.Products.Where(x => x.ProductID == tempProdID).Single().QuantityInStock;

                                                        Console.WriteLine("The database has been successfully updated. The Product is now called: " + upDateProdName);
                                                        //upDateBool = true;
                                                        confirmUpdate = true;
                                                        break;

                                                    case "NO":
                                                        //breakout of the switch/do while
                                                        break;

                                                    default:
                                                        Console.WriteLine("Please enter either a 'YES' or a 'NO' only.");
                                                        break;

                                                }
                                            } while (!confirmUpdate && verifyUpdate != "NO");
                                           tempIDInputExist = true;
                                        }
                                       
                                    } while (!tempIDInputExist);
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


                        case "E":
                            Console.WriteLine(" You are in the Update Product Description.");
                           
                            int tempDescProdID = 0;

                            using (DatabaseContext context = new DatabaseContext())
                            {
                                Console.WriteLine(" Below is a list of all current products in inventory:");

                                foreach (Product product in context.Products.ToList())
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("\n\t Product ID Number: " + product.ProductID);
                                    Console.ResetColor();

                                    Console.WriteLine("\t Product Name: " + product.ProductName);

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("\t Product Description: " + product.Description);
                                    Console.ResetColor();
                                }

                                Console.WriteLine("");
                                tempProdID = InputNumberFn("\n Please select the ID number of the product whose description you wish to change from the list above (Please enter only numbers):");
                                try
                                {
                                    // bool upDateBool = false;
                                    bool tempIDInputExist = false;
                                    string verifyUpdate = "";
                                    int verifyInputID = context.Products.Where(x => x.ProductID == tempDescProdID).Select(x => x.ProductID).FirstOrDefault();

                                    do
                                    {

                                        verifyInputID = context.Products.Where(x => x.ProductID == tempDescProdID).Select(x => x.ProductID).FirstOrDefault();


                                        if (tempDescProdID != verifyInputID)
                                        {
                                            Console.WriteLine(" Sorry, that number does not exist in the database");
                                            tempIDInputExist = false;
                                            Console.WriteLine("");
                                            tempDescProdID = InputNumberFn("\n Please select the ID number of the product whose description you wish to change from the list above (Please enter only numbers):");

                                        }
                                        else
                                        {

                                            string upDateProdDesc = "";
                                            Console.WriteLine(" Please enter the new description of the product: ");
                                            upDateProdDesc = Console.ReadLine().Trim();

                                            bool confirmUpdate;
                                            do
                                            {
                                              
                                                confirmUpdate = false;
                                                Console.WriteLine(" Please confirm you would like to update this product's description to " + upDateProdDesc + ": Yes || No ");
                                                verifyUpdate = Console.ReadLine().Trim();
                                                switch (verifyUpdate.ToUpper())
                                                {                                                    //Verify the client wants to  update, and didn't get here by mistake.

                                                    case "YES":

                                                        context.Products.Where(x => x.ProductID == tempProdID).Single().Description = upDateProdDesc;
                                                        context.SaveChanges();
                                                     

                                                        Console.WriteLine(" The database has been successfully updated. The Product Description is now : " + upDateProdDesc);
                                                        //upDateBool = true;
                                                        confirmUpdate = true;
                                                        break;

                                                    case "NO":
                                                        //breakout of the switch/do while
                                                        break;

                                                    default:
                                                        Console.WriteLine(" Please enter either a 'YES' or a 'NO' only.");
                                                        break;

                                                }
                                            } while (!confirmUpdate && verifyUpdate != "NO");
                                            tempIDInputExist = true;
                                        }

                                    } while (!tempIDInputExist);
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

                        case "F":
                            Console.WriteLine("You are in the Update Product Price Section.");
                            
                            int tempPriceProdID = 0;

                            using (DatabaseContext context = new DatabaseContext())
                            {
                                Console.WriteLine(" Below is a list of all current products in inventory:");

                                foreach (Product product in context.Products.ToList())
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("\n\t Product ID Number: " + product.ProductID);
                                    Console.ResetColor();

                                    Console.WriteLine("\t Product Name: " + product.ProductName);

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("\t Product Sale Price: $" + product.SalePrice);
                                    Console.ResetColor();
                                }

                                Console.WriteLine("");
                                tempProdID = InputNumberFn("\n Please select the ID number of the product whose description you wish to change from the list above (Please enter only numbers):");
                                try
                                {
                                    // bool upDateBool = false;
                                    bool tempIDInputExist = false;
                                    string verifyUpdate = "";
                                    int verifyInputID = context.Products.Where(x => x.ProductID == tempPriceProdID).Select(x => x.ProductID).FirstOrDefault();

                                    do
                                    {

                                        verifyInputID = context.Products.Where(x => x.ProductID == tempPriceProdID).Select(x => x.ProductID).FirstOrDefault();


                                        if (tempPriceProdID != verifyInputID)
                                        {
                                            Console.WriteLine(" Sorry, that number does not exist in the database");
                                            tempIDInputExist = false;
                                            Console.WriteLine("");
                                            tempDescProdID = InputNumberFn("\n Please select the ID number of the product whose sale price you wish to change from the list above (Please enter only numbers):");

                                        }
                                        else
                                        {

                                            decimal upDateProdPrice = 0;
                                            Console.WriteLine(" Please enter the new sale price of the product: ");
                                            upDateProdPrice = decimal.Parse (Console.ReadLine().Trim());

                                            bool confirmUpdate;
                                            do
                                            {

                                                confirmUpdate = false;
                                                Console.WriteLine(" Please confirm you would like to update this product's sale price to " + upDateProdPrice + ": Yes || No ");
                                                verifyUpdate = Console.ReadLine().Trim();
                                                switch (verifyUpdate.ToUpper())
                                                {                                                    //Verify the client wants to  update, and didn't get here by mistake.

                                                    case "YES":

                                                        context.Products.Where(x => x.ProductID == tempProdID).Single().SalePrice = upDateProdPrice;
                                                        context.SaveChanges();


                                                        Console.WriteLine(" The database has been successfully updated. The Product Sale Price is now : $" + upDateProdPrice);
                                                        //upDateBool = true;
                                                        confirmUpdate = true;
                                                        break;

                                                    case "NO":
                                                        //breakout of the switch/do while
                                                        break;

                                                    default:
                                                        Console.WriteLine(" Please enter either a 'YES' or a 'NO' only.");
                                                        break;

                                                }
                                            } while (!confirmUpdate && verifyUpdate != "NO");
                                            tempIDInputExist = true;
                                        }

                                    } while (!tempIDInputExist);
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

                        case "G":
                           Console.WriteLine("\tYou are in the View Product List Section.");
                            using (DatabaseContext context = new DatabaseContext())
                            {
                                foreach (Product product in context.Products.ToList())
                                {

                                    context.Entry(product).Reference(x => x.Supplier).Load();
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("\n\n\t Product ID Number: " + product.ProductID);
                                    Console.ResetColor();
                                    Console.WriteLine("\t Supplier ID Number: " + product.Supplier.SupplierID + "\n\t Supplier Name: " + product.Supplier.CompanyName + "\n\t Product Name: " + product.ProductName + "\n\t Description: "+ product.Description + "\n\t Quantity in Stock: " + product.QuantityInStock + "\n\t Discontinued: " + product.Discontinued + "\n\t Sale Price : $" + product.SalePrice);
                                }
                            }    

                                break;


                        case "H":
                            Console.WriteLine("You are in the Update Supplier Info Section.");

                            break;


                        case "Q":
                            //This switch option is used to exit the admin menu.
                            break;


                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }

                } while (userChoice.ToUpper() != "Q");


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



/////
int InputNumberFn(string consoleMessage)
{

    bool validator = false;
    int NumberOuput = 0;

    do
    {
        Console.Write(consoleMessage);

        if (int.TryParse(Console.ReadLine().Trim(), out NumberOuput)) validator = true;

        else Console.WriteLine("Invalid entry.");

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
///
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
                        Console.WriteLine("Wrong selection! Please choose 'A' for shopping cart, 'B' to exit.");
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