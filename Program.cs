using System.Text.RegularExpressions;


const string passCode = "password";
string admin = "";
const string USER_DATA = "userData.txt";
string userChoice = "";
string userName = "";
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

            Console.WriteLine(success ? "Successfull foud you in database" : "We need further details");

            if (!success)
            {
                phoneNumber = getValidation("Enter your Phone Number (should be 10 digit long): ", @"^[2-9][\d]{9}$");
                //address = getValidation("Enter your Address (should be less than 50 characters long): ", @"^[A-Za-z0-9]+(?:\s[A-Za-z0-9'_-]+)+$");
                using (StreamWriter writer = File.AppendText(USER_DATA))
                {
                    writer.WriteLine($"{userName}|{phoneNumber}");
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
