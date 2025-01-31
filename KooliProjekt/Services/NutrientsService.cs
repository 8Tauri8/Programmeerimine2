using KooliProjekt.Data.Repositories;
using KooliProjekt.Data;
using KooliProjekt.Services;

public class NutrientsService : INutrientsService
{
    private readonly INutrientsRepository _nutrientsRepository;

    public NutrientsService(INutrientsRepository nutrientsRepository)
    {
        _nutrientsRepository = nutrientsRepository;
    }

    public async Task<PagedResult<Nutrients>> List(int page, int pageSize)
    {
        return await _nutrientsRepository.List(page, pageSize);
    }

    public async Task<Nutrients> Get(int id)
    {
        var result = await _nutrientsRepository.Get(id);
        return result ?? new Nutrients(); // Return a default Nutrient if not found
    }

    public async Task Save(Nutrients nutrient)
    {
        await _nutrientsRepository.Save(nutrient);
    }

    public async Task Delete(int id)
    {
        var nutrient = await _nutrientsRepository.Get(id);
        if (nutrient != null) // Only delete if the nutrient is found
        {
            await _nutrientsRepository.Delete(id);
        }
    }
}
