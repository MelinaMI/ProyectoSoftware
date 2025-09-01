﻿using Application.Interfaces.IStatus;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class StatusQuery : IStatusQuery
    {
        private readonly AppDbContext _context;

        public StatusQuery(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Status>> GetAllStatusesAsync()
        {
            return await _context.Statuses.AsNoTracking().ToListAsync(); //trae todos los registros en forma de lista
        }

        public async Task<Status> GetStatusById(int id)
        {
            return await _context.Statuses.AsNoTracking().FirstOrDefaultAsync(status => status.Id== id); //trae el registro que corresponda a la id 
        }
    }
}
