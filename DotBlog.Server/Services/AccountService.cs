using System;
using System.Threading.Tasks;
using DotBlog.Server.Data;
using DotBlog.Server.Models;
using DotBlog.Shared.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DotBlog.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly IOptions<AppConfig> _options;
        private readonly DotBlogDbContext _context;

        public AccountService(IOptions<AppConfig> options, DotBlogDbContext context)
        {
            _options = options
                      ?? throw new ArgumentNullException(nameof(options));
            _context = context
                      ?? throw new ArgumentNullException(nameof(context));

        }

        public async Task<bool> Verify(AccountLoginDto account)
        {
            throw new NotSupportedException("Have not finished!");

            account.Password =
                Helper.EncryptPassword.EncryptPasswordWithSalt(account.Password, _options.Value.PasswordSalt);

            return await _context.Accounts.AnyAsync (it =>
                (it.UserId.ToString() == account.Identification ||
                it.Name == account.Identification ||
                it.Mail == account.Identification ||
                it.Phone == account.Identification) &&
                it.PasswordHash == account.Password
            );
        }
    }
}
