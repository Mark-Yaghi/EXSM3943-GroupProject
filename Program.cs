using System.Text.RegularExpressions;


const string passCode = "password";
string admin = "";
string userChoice = "";
string userName = "";
string phoneNumber;
do
{
    Console.WriteLine("1) Enter \"1\" Make Purchase\n2) Enter \"2\" For Admin Login\n3) Enter \"0\" to Quit");
    Console.Write("Please Make A Selection: ");
    userChoice = Console.ReadLine().Trim();
    switch (userChoice)
    {
        case "1":
            Console.WriteLine("Enter your Name: ");
            userName = Console.ReadLine().Trim();
            Console.WriteLine("Enter your Phone Number (should be 10 digit long): ");
            phoneNumber = getValidation("Enter your Phone Number (should be 10 digit long): ", @"^[2-9][\d]{9}$");
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
