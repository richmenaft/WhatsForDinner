using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Infrastructure;
using WhatsForDinner.DataAccess.Postgres;
using WhatsForDinner.DataAccess.Postgres.Models;
using WhatsForDinner.Models;
using WhatsForDinner.Services;
using DishEntity = WhatsForDinner.DataAccess.Postgres.Models.DishEntity;

namespace WhatsForDinner.Endpoints;

public static class DishesEndpoints
{
    public static void MapDishesEndpoints(this WebApplication app)
    {
        RouteGroupBuilder dishesApi = app.MapGroup("/api/dishes");

        dishesApi.MapGet("/getAll", (IDishReaderService dishService, ILogger<Program> logger, WfdDbContext dbContext) =>
            {
                logger.LogInformation("All dishes has sended");
                return dishService.GetAllDishesAsync(dbContext);
            })
            .WithName("GetAllDishesEndpoint")
            .WithSummary("Get all dishes")
            .WithDescription("Returns all the dishes");
        
        dishesApi.MapPost("/createDish", (DishEntity dish,  IDishWriterService dishService, ILogger<Program> logger, WfdDbContext dbContext) =>
            {
                // if (dish.DishId == Guid.Empty)
                // {
                //     dish.DishId = Guid.CreateVersion7(TimeProvider.System.GetUtcNow());
                //     logger.LogInformation("Created dish with id {DishId}", dish.Id);
                // }

                DishEntity newDish;
                try
                {
                    newDish = dishService.CreateDishAsync(dish, dbContext).Result;
                }
                catch
                {
                    logger.LogError("Error creating new dish");
                    return Results.Problem("Error creating new dish");
                }

                logger.LogInformation("Created new dish: {newDish.Name}", newDish.Title);
                return Results.Ok(newDish);
            })
            .WithSummary("Create a new dish");
        dishesApi.MapDelete("/deleteDish", (string id, IDishWriterService dishService, ILogger<Program> logger, WfdDbContext dbContext) =>
            {
                bool isSuccessfull = dishService.DeleteDishByIdAsync(Guid.Parse(id), dbContext).Result;
                if (!isSuccessfull)
                {
                    logger.LogError("Cant find dish with id {id}", id);
                    return Results.NotFound($"Cant found dish with id {id}");
                }

                logger.LogInformation("Dish [id: {id}] has been deleted. ", id);
                return Results.Ok(id);
            })
            .WithSummary("Delete a dish");

        dishesApi.MapPut("/updateDish", (DishEntity dish, IDishWriterService dishService, ILogger<Program> logger, WfdDbContext dbContext) =>
            {
                DishEntity updatedDish;
                try
                {
                    updatedDish = dishService.UpdateDishAsync(dish, dbContext).Result;
                }
                catch(KeyNotFoundException)
                {
                    logger.LogError("Dish not found");
                    return Results.NotFound();
                }

                logger.LogInformation("Dish [id: {id}] has been updated. ", updatedDish.DishId);
                return Results.Ok(updatedDish);
            })
            .WithSummary("Change a dish");
    }
}