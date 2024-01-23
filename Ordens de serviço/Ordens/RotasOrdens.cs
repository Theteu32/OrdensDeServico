using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Ordens_de_serviço.Data;
using System;
using System.Diagnostics.Metrics;

namespace Ordens_de_serviço.Ordens;

public static class RotasOrdens
{
    public static void AddRotasOrdens(this WebApplication app)
    {

         var rotasOrdens = app.MapGroup("ordens");

        //adicionando Ordens
        rotasOrdens.MapPost("criar", async (AddOrdemRequest request, AppDbContext context, CancellationToken ct ) =>
             {
            var novaOrdem = new Ordem(request.nome, request.setor, request.motivo, request.hora);

            await context.Ordens.AddAsync(novaOrdem, ct);

            await context.SaveChangesAsync(ct);
            });
        //filtrando so as ordens que ainda nao foram concluidas 
        rotasOrdens.MapGet("naoConcluidas", async (AppDbContext context, CancellationToken ct) =>
             {
             var ordens = await context.Ordens.Where(ordem => ordem.Concluida == false).ToListAsync(ct);

                return ordens;

            });
        //todas as ordens concluidas
        rotasOrdens.MapGet("concluidas", async (AppDbContext context, CancellationToken ct) =>
        {
            var ordens = await context.Ordens.Where(ordem => ordem.Concluida == true).ToListAsync(ct);

            return ordens;

        });
        //todas as ordens 
        rotasOrdens.MapGet("todasAsOrdens", async (AppDbContext context, CancellationToken ct) =>
        {
            var ordens = await context.Ordens.ToListAsync(ct);

            return ordens;

        });
        //Atualizar as Ordens 
        rotasOrdens.MapPut("atualizar{id}", async (Guid id, AppDbContext context, CancellationToken ct) =>
            {       
            var ordem = await context.Ordens.SingleOrDefaultAsync(ordem => ordem.Id == id, ct);

                if (ordem == null)
                    return Results.NotFound();

            ordem.Concluida = true;

            await context.SaveChangesAsync(ct);
                return Results.Ok(ordem);

            });
        //Deletar uma Ordem
        rotasOrdens.MapDelete("deletar{id}", async (Guid id, AppDbContext context, CancellationToken ct) =>
            {
                var ordem = await context.Ordens.FindAsync(id, ct);

                if (ordem == null)
                {
                    return Results.NotFound($"A ordem com o ID {id} não foi encontrada.");
                }

                context.Ordens.Remove(ordem);
                await context.SaveChangesAsync(ct);

                return Results.Ok($"A ordem com o ID {id} foi excluída com sucesso.");

            });
    }
}

