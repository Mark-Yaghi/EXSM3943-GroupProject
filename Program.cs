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
do
{
    Console.WriteLine("1) Enter \"1\" Make Purchase\n2) Enter \"2\" For Admin Login\n3) Enter \"0\" to Quit");
    Console.Write("Please select option: ");
    userChoice = Console.ReadLine().Trim();
    switch (userChoice)
    {
        case "1":
            Console.WriteLine("Enter your First Name: ");
            userFirstName = Console.ReadLine().Trim();
            Console.WriteLine("Enter your Last Name: ");
            userLastName = Console.ReadLine().Trim();
            //string userID = "";
            try
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    //userID = context.Customers.Where(x => x.FirstName == userFirstName && x.LastName == userLastName).Single().CustomerID;

                    if ((context.Customers.Where(x => x.FirstName == userFirstName && x.LastName == userLastName).Single().CustomerID) == null)
                    {
                        phoneNumber = getValidation("Enter your Phone Number (should be 10 digit long): ", @"^[2-9][\d]{9}$");
                        address = getValidation("Enter your Address (should be maximum 50 characters long): ", @"^[A-Za-z\d][\w\s.]{1,50}$");

                        context.Customers.Add(new Customer(userFirstName, userLastName, address, getIntValue(phoneNumber)) { });
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sorry, couldn't find you. {ex.Message}");
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
                            Console.WriteLine("Add Product");
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


//bool success = false;

//using (StreamReader reader = File.OpenText(USER_DATA))
//{
//    string line;
//    while ((line = reader.ReadLine()) != null && (success = success || line.Split("|")[0] == userName)) ;
//}

//Console.WriteLine(success ? $"Successfully found you in database, {userName}." : $"We need further details, {userName}");



//if (!success)
//{
//    phoneNumber = getValidation("Enter your Phone Number (should be 10 digit long): ", @"^[2-9][\d]{9}$");
//    address = getValidation("Enter your Address (should be maximum 50 characters long): ", @"^[A-Za-z\d][\w\s.]{1,50}$");
//    using (StreamWriter writer = File.AppendText(USER_DATA))
//    {
//        writer.WriteLine($"{userName}|{phoneNumber}|{address}");
//    }
//}