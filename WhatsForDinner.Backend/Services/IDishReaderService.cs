using WhatsForDinner.DataAccess.Postgres;
using WhatsForDinner.DataAccess.Postgres.Models;
using WhatsForDinner.Models;

namespace WhatsForDinner.Services;

public interface IDishReaderService
{
    Task<IEnumerable<DishEntity>> GetAllDishesAsync(WfdDbContext dbContext);
}