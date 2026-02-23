



public class SenzorService : BaseService
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;
    private IQueryable<Senzor> senzors => _unit.Uredjaji.GetQueryable().OfType<Senzor>();
    private IQueryable<Kontroler> controllers => _unit.Uredjaji.GetQueryable().OfType<Kontroler>();

    public SenzorService(IUnitOfWork unit, IMapper mapper, IServiceProvider provider) : base(provider)
    {
        _unit = unit;
        _mapper = mapper; 
    }

    public async Task<object> GetAnalytics()
    {
        var prosjek = await senzors
            .Include(x => x.Grad)
            .GroupBy(x => x.Grad.Name)
            .Select(m => new {
                Grad = m.Key,
                Prosjek = m.Average(n => (double?)n.Vrijednost ?? 0.0)
            }).ToListAsync();

        if (!prosjek.Any()) return null;

        var prosjekDrzave = prosjek.Average(x => x.Prosjek);
        var maxKritican = prosjek.MaxBy(x => x.Prosjek);

        return new
        {
            GradoviProsjeci = prosjek,
            DrzavniProsjek = prosjekDrzave,
            NajgoriGrad = maxKritican,
            KriticniGradovi = prosjek.Where(x => x.Prosjek > prosjekDrzave).ToList()
        };
    }

    public async Task<IEnumerable<DevicesReadDto>> GetAllDevices()
    {
        
        return _mapper.Map<IEnumerable<DevicesReadDto>>(await _unit.Uredjaji
            .GetQueryable()
            .Include(x => x.Grad)
            .ToListAsync());
    }

    public async Task<SenzorReadDto> AddSenzor(SenzorCreateDto dto)
    {
        await ValidateAsync(dto);
        var senzor = _mapper.Map<Senzor>(dto);        
        await _unit.Uredjaji.AddAsync(senzor);
        var res = await _unit.CompleteAsync();
        if (res <= 0) throw new Exception("Failed to add sensor");

        return _mapper.Map<SenzorReadDto>(senzor);
    }

    public async Task UpdateSenzor(int id, SenzorUpdateDto dto)
    {
        await ValidateAsync(dto);
        var existing = await _unit.Uredjaji.GetByIdAsync(id) as Senzor;
        if (existing == null) throw new Exception("Sensor not found");

        _mapper.Map(dto, existing);

        await _unit.CompleteAsync();
    }

    public async Task<ControllerReadDto> AddKontroler(ControllerCreateDto dto)
    {
        await ValidateAsync(dto);
        var kontroler = _mapper.Map<Kontroler>(dto);    
        await _unit.Uredjaji.AddAsync(kontroler);
        var res = await _unit.CompleteAsync();
        if (res <= 0) throw new Exception("Failed to add controller");

        return _mapper.Map<ControllerReadDto>(kontroler);
    }
    public async Task UpdateKontroler(int id, ControllerUpdateDto dto)
    {
        await ValidateAsync(dto);
        var existing = await _unit.Uredjaji.GetByIdAsync(id) as Kontroler;

      if (existing == null) throw new Exception("Controller not found");

       _mapper.Map(dto, existing);

        await _unit.CompleteAsync();
    }

    public async Task DeleteDevice(int id)
    {
        var device = await _unit.Uredjaji.GetByIdAsync(id);
        if (device == null) throw new Exception("Device not found");

        _unit.Uredjaji.Delete(device);
        await _unit.CompleteAsync();
    }
}