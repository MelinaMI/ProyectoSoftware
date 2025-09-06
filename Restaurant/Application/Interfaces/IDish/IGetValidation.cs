using Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IDish
{
    public interface IGetValidation
    {
        Task ValidateQueryAsync(string? name, int? category, OrderPrice? sortByPrice);
    }
}
