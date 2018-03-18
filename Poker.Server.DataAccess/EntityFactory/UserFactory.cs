using Poker.Models.User;
using Poker.Server.DataAccess.Interfaces;

namespace Poker.Server.DataAccess.EntityFactory
{
    public class UserFactory : IEntityFactory
    {
        public User Build(dynamic entity)
        {
            return new User
            {
                Id = entity.Id,
                BirthDate = entity.BirthDate,
                UserName = entity.UserName,
                Password = entity.Password,
                CreateDate = entity.CreateDate
            };
        }

        public TEntity Build<TEntity>(dynamic entity)
        {
            return Build(entity);
        }
    }
}
