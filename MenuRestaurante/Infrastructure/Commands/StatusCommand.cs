using Application.Interfaces.IStatus;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Responsabilidad: Solo llevar la información necesaria para ejecutar la acción, luego un handler (o el servicio) hace el trabajo.*/
namespace Infrastructure.Commands
{
    public class StatusCommand : IStatusCommand
    {
        private readonly AppDbContext _context;

        public StatusCommand(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateStatusAsync(Status status)
        {
            _context.Statuses.Add(status); 
            await _context.SaveChangesAsync(); //Se guardan los cambios en la base de datos de manera asíncrona.
        }

        public async Task DeleteStatusAsync(Status status)
        {
           _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStatusAsync(Status status)
        {
            _context.Statuses.Update(status);
            await _context.SaveChangesAsync();
        }
    }
}
