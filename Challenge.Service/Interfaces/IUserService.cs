using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Model;

namespace Service.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();
    }
}
