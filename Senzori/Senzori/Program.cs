List<Uredjaj> AllData = new List<Uredjaj>();
StorageManager<Uredjaj> mananger = new StorageManager<Uredjaj>();
AllData.AddRange(DataInitializer.GetSeedData());

SenzorService service = new SenzorService(AllData, mananger);

ConsoleUI user = new ConsoleUI(service);
await user.MainMenu(); 




