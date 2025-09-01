using Application.Request.Create;
using Application.Request.Update;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Responsabilidad:  Implementar reglas de negocio, no preocuparse por HTTP, UI ni detalles de base de datos (más allá de usar el DbContext).*/
namespace Application.Interfaces.IStatus
{
    public interface IStatusService
    {
        Task<IReadOnlyList<StatusResponse>> GetAllStatusesAsync();
        Task<StatusResponse> GetStatusByIdAsync(int id);
        Task<StatusResponse> CreateStatusAsync(CreateStatusRequest request);
        Task<StatusResponse> UpdateStatusAsync(int id, UpdateStatusRequest request);
        Task<bool> DeleteStatusAsync(int id);

    }
}
