


using var context = new SenzorsContext();
var service = new SenzorService(context);

if (!context.Devices.Any())
{
    var data = DataInitializer.GetSeedData();
	foreach (var d in data)
	{
		await service.AddDevice(d);
	}
}


ConsoleUI user = new ConsoleUI(service);
await user.MainMenu(); 




