
using var context = new MasterContext();


using var unitOfWork = new UnitOfWork(context);


var seedService = new SeedService(unitOfWork);
await seedService.SeedEverythingAsync();

var cityService = new CityService(unitOfWork);
var senzorService = new SenzorService(unitOfWork);
var bookStoreService = new BookStoreService(unitOfWork);
var parseExecutor = new ParseExecutor();

var cityUI = new SmartCityUI(cityService);
var senzorUI = new SenzorUI(senzorService);
var bookStoreUI = new BookStoreUI(bookStoreService, parseExecutor);






var masterUI = new MasterUI(cityUI, senzorUI, bookStoreUI);
await masterUI.Start();