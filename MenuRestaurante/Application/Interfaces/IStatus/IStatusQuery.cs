using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Responsabilidad: Solo traer datos y, a veces, mapearlos a un DTO*/
namespace Application.Interfaces.IStatus
{
    public interface IStatusQuery
    {
        Task<IReadOnlyList<Status>>GetAllStatusesAsync();
        Task<Status> GetStatusById(int id);
    }
}
