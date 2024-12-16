using Microsoft.EntityFrameworkCore;
using WhatsForDinner.DataAccess.Postgres;
using WhatsForDinner.DataAccess.Postgres.Models;
using WhatsForDinner.Models;

namespace WhatsForDinner.Services;

public class DishReaderService: IDishReaderService
{
    public async Task<IEnumerable<DishEntity>> GetAllDishesAsync(WfdDbContext dbContext)
    {
        return dbContext.Dishes
            .Include(d => d.Tags)
            .Include(d => d.Img)
            .AsNoTracking()
            .AsEnumerable()
            .ToList();
    }
}