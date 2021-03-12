using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotBlog.Shared.Dto;

namespace DotBlog.Server.Services
{
    public interface IAccountService
    {
        Task<bool> Verify(AccountLoginDto account);
    }
}
