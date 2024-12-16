using WhatsForDinner.DataAccess.Postgres;
using WhatsForDinner.DataAccess.Postgres.Models;
using WhatsForDinner.Models;
using DishEntity = WhatsForDinner.DataAccess.Postgres.Models.DishEntity;

namespace WhatsForDinner.Services;

public interface IDishWriterService
{
    Task<DishEntity> CreateDishAsync(DishEntity dish, WfdDbContext dbContext);
    Task<bool> DeleteDishByIdAsync(Guid id, WfdDbContext dbContext);
    
    Task<DishEntity> UpdateDishAsync(DishEntity dish, WfdDbContext dbContext);
}