

LibraryLimitException.OnLimitReached += (vrijeme, poruka) =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"[LOG - {vrijeme: hh,mm,ss}]: {poruka}");
    Console.ResetColor();
};

LibraryLimitException.OnLimitReached += (vrijeme, poruka) => {

    File.AppendAllText("log.txt", poruka);

};

using var bookStoreContext = new BookStoreContext();
var service = new BookStoreService(bookStoreContext);
var executor = new ParseExecutor();
bookStoreContext.StoreItems.RemoveRange(bookStoreContext.StoreItems);
await bookStoreContext.SaveChangesAsync();
if (!bookStoreContext.StoreItems.Any())
{
    // Punimo knjige
    var books = DataInitializer.InitializeBookData();
    foreach (var item in books) await service.AddNewItem(item);

    // Punimo filmove - OVO TI JE FALILO
    var movies = DataInitializer.InitializeMovieData();
    foreach (var item in movies) await service.AddNewItem(item);
}
var ConsoleMenu = new ConsoleUI(service, executor);
    await ConsoleMenu.MainMenu();