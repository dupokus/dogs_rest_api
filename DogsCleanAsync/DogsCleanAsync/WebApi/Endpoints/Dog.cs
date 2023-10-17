using Application.Dogs.Create;
using Application.Dogs.Delete;
using Domain.Dogs;
using Carter;
using MediatR;
using Application.Dogs.Update;
using Microsoft.AspNetCore.Mvc;
using Application.Dogs.Get;
using Application.Dogs.GetById;

namespace WebApi.Endpoints
{
    public class Dog : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("dogs", async (CreateDogCommand command, ISender sender) =>
            {
                await sender.Send(command);
                return Results.Ok();
            });

            app.MapGet("dogs", async (
                string? searchTerm,
                string? sortColumn,
                string? sortOrder,
                int page, 
                int pageSize,
                ISender sender) =>
            {
                var query = new GetDogsQuery(searchTerm, sortColumn, sortOrder, page, pageSize);

                var dogs = await sender.Send(query);

                return Results.Ok(dogs);
            });

            app.MapGet("dogs/{id:guid}", async (Guid id, ISender sender) =>
            {
                try
                {
                    return Results.Ok(await sender.Send(new GetDogQuery(new DogId(id))));
                }
                catch (DogNotFoundException e)
                {
                    return Results.NotFound(e.Message);
                }
            });

            app.MapPut("dogs/{id:guid}", async (Guid id, [FromBody] UpdateDogRequest request, ISender sender) =>
            {
                var command = new UpdateDogCommand(
                    new DogId(id),
                    request.Name,
                    request.Color,
                    request.TailLength,
                    request.Weight);

                await sender.Send(command);

                return Results.NoContent();
            });
            app.MapDelete("dogs/{id:guid}", async (Guid id, ISender sender) =>
            {
                try
                {
                    await sender.Send(new DeleteDogCommand(new DogId(id)));

                    return Results.NoContent();
                }
                catch (DogNotFoundException e) 
                { 
                    return Results.NotFound(e.Message);
                }               
            }); 
        }
    }
}
