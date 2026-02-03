

LibraryLimitException.OnLimitReached += (vrijeme, poruka) =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"[LOG - {vrijeme: hh,mm,ss}]: {poruka}");
    Console.ResetColor();
};

LibraryLimitException.OnLimitReached += (vrijeme, poruka) => {

    File.AppendAllText("log.txt", poruka);

};
List<BibliotekaArtikal> allData = new List<BibliotekaArtikal>();
ParseExecutor executor = new ParseExecutor();
allData.AddRange(DataInitializer.InitializeBookData());
allData.AddRange(DataInitializer.InitializeMovieData());
StorageManager<BibliotekaArtikal> manager = new StorageManager<BibliotekaArtikal>();
BookStoreService service = new BookStoreService(allData, manager);
ConsoleUI ConsoleMenu = new ConsoleUI(service, executor);
await ConsoleMenu.MainMenu(); 