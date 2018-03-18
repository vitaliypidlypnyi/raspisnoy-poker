using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Poker.Core.SqlBuilders;
using Poker.Core.SqlOperations;
using Poker.Models.TableMaps;
using Poker.Models.User;
using Poker.Server.DataAccess;

namespace Poker.Server.Identity
{
    public class UserStore<TUser> : IUserStore<TUser, int> where TUser : User
    {
        private bool _disposed;
        private readonly IRepository<TUser> _userRepository;

        public UserStore()
        {
            var conString = string.Empty;
#if DEBUG
            conString = System.Configuration.ConfigurationManager.ConnectionStrings["PokerLocalConnection"].ConnectionString;
#else
            conString = System.Configuration.ConfigurationManager.ConnectionStrings["PokerHostConnection"].ConnectionString;
#endif
            var connectionFactory = new MssqlConnectionFactory(conString);

            _userRepository = new Repository<TUser>(connectionFactory);
        }

        public async Task CreateAsync(TUser user)
        {
            ThrowIfDisposed();
            var userExists = (await FindByNameAsync(user.UserName)) != null;
            if (userExists)
            {
                throw new InvalidOperationException();
            }

            await _userRepository.SaveAsync(new UserTableMap(), user);
        }

        public async Task DeleteAsync(TUser user)
        {
            ThrowIfDisposed();
            var filter = new Filter();
            filter.And(Operation.IdEquals(user.Id));
            await _userRepository.DeleteAsync(new UserTableMap(), filter);
        }

        public async Task<TUser> FindByIdAsync(int userId)
        {
            ThrowIfDisposed();
            var filter = new Filter();
            filter.And(Operation.IdEquals(userId));
            return await _userRepository.SelectFirstAsync(new UserTableMap(), filter);
        }

        public async Task<TUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            var filter = new Filter();
            filter.And(Operation.Equals("UserName", userName));
            return await _userRepository.SelectFirstAsync(new UserTableMap(), filter);
        }

        public async Task UpdateAsync(TUser user)
        {
            ThrowIfDisposed();
            var userToUpdate = await FindByNameAsync(user.UserName);
            if(userToUpdate == null)
            {
                throw new ArgumentException(nameof(user));
            }

            userToUpdate.BirthDate = user.BirthDate;

            var filter = new Filter();
            filter.And(Operation.IdEquals(user.Id));
            await _userRepository.UpdateAsync(new UserTableMap(), filter, userToUpdate);
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}