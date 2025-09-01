using Application.Interfaces.IStatus;
using Application.Response;
using Domain.Entities;
using Application.Request.Create;
using Application.Request.Update;
namespace Application.UseCase
{
    public class StatusService : IStatusService
    {
        private readonly IStatusCommand _command;
        private readonly IStatusQuery _query;

        public StatusService(IStatusCommand command, IStatusQuery query)
        {
            _command = command;
            _query = query;
        }
        public Task<StatusResponse> CreateStatusAsync(CreateStatusRequest request)
        {
            var status = new Status() { Name = request.Name };

        }

        public Task<bool> DeleteStatusAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<StatusResponse>> GetAllStatusesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> GetStatusByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<StatusResponse> UpdateStatusAsync(int id, UpdateStatusRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
