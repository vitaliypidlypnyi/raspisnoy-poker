using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Poker.Models.User;
using Poker.Server.DataAccess.Interfaces;

namespace Poker.Server.DataAccess.EntityFactory
{
    public class EntityFactory<TEntity>
    {
        private Dictionary<Type, IEntityFactory> _dictionary = new Dictionary<Type, IEntityFactory>();

        public EntityFactory()
        {
            _dictionary.Add(typeof(IUser<int>), new UserFactory());
            _dictionary.Add(typeof(User), new UserFactory());
        }

        public TEntity Build(dynamic entity)
        {
            ValidateKey();
            return _dictionary[typeof(TEntity)].Build<TEntity>(entity);
        }

        public IEnumerable<TEntity> BuildMultiple(IEnumerable<dynamic> entities)
        {
            ValidateKey();
            var result = new List<TEntity>();
            foreach (var entity in entities)
            {
                result.Add(_dictionary[typeof(TEntity)].Build<TEntity>(entity));
            }
            return result;
        }

        private void ValidateKey()
        {
            if (!_dictionary.ContainsKey(typeof(TEntity)))
            {
                throw new InvalidOperationException();
            }
        }
    }
}
