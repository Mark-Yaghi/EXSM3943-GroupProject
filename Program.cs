
string userChoice = "";
do
{
    Console.WriteLine("1) Enter \"1\" Make Purchase\n2) Enter \"2\" For Admin Login\n3) Enter \"0\" to Quit");
    Console.Write("Please Make A Selection: ");
    userChoice = Console.ReadLine().Trim();
    switch (userChoice)
    {
        case "1":
            Console.WriteLine("Make Purchases Section");
            break;
        case "2":
            Console.WriteLine("Admin Section.");
            break;
        case "0":
            break;
        default:
            Console.WriteLine("Invalid input. Please try again !!!");
            break;
    }
} while (userChoice != "0");


