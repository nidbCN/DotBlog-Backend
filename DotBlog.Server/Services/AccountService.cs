using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotBlog.Server.Data;
using DotBlog.Server.Entities;
using DotBlog.Server.Models;
using DotBlog.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DotBlog.Server.Services
{
    public class AccountService : IAccountService
    {
        private IOptions<AppConfig> Options { get; }
        private DotBlogDbContext Context { get; }
        public AccountService(IOptions<AppConfig> options, DotBlogDbContext context)
        {
            Options = options
                      ?? throw new ArgumentNullException(nameof(options));
            Context = context
                      ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<bool> Verify(AccountLoginDto account)
        {
            account.Password =
                Helper.EncryptPassword.EncryptPasswordWithSalt(account.Password, Options.Value.PasswordSalt);

            return await Context.Accounts.AnyAsync (it =>
                (it.UserId.ToString() == account.Identification ||
                it.Name == account.Identification ||
                it.Mail == account.Identification ||
                it.Phone == account.Identification) &&
                it.PasswordHash == account.Password
            );
        }
    }
}
