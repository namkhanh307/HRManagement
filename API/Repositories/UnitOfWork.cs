﻿using API.Models;

namespace API.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly HrmanagementContext _context;
        private readonly ILogger _logger;

        public IUserRepository Users {  get; private set; }


        public UnitOfWork(HrmanagementContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            Users = new UserRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
