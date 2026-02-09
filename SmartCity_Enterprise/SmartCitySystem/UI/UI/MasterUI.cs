public class MasterUI
{
    private readonly SmartCityUI _cityUI;
    private readonly SenzorUI _senzorUI;
    private readonly BookStoreUI _bookStoreUI;

    public MasterUI(SmartCityUI cityUI, SenzorUI senzorUI, BookStoreUI bookStoreUI)
    {
        _cityUI = cityUI;
        _senzorUI = senzorUI;
        _bookStoreUI = bookStoreUI;
    }

    public async Task Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== MASTER CITY MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Smart City Management (Traffic & Parking)");
            Console.WriteLine("2. IoT Sensor System (Air Quality & Devices)");
            Console.WriteLine("3. City Library & BookStore");
            Console.WriteLine("4. Exit Application");
            Console.Write("\nSelect a system to manage: ");

            string choice = Console.ReadLine();


            switch (choice)
            {
                case "1":
                    await _cityUI.MainMenu();
                    break;
                case "2":
                    await _senzorUI.MainMenu();
                    break;
                case "3":
                    await _bookStoreUI.MainMenu();
                    break;
                case "4":
                    Console.WriteLine("Exiting... Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}