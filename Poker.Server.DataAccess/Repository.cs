using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Poker.Core.ColumnOperations;
using Poker.Core.DAL;
using Poker.Core.Maps;
using Poker.Core.SqlBuilders;
using Poker.Core.Validation;
using Poker.Server.DataAccess.EntityFactory;

namespace Poker.Server.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly EntityFactory<TEntity> _entityFactory;

        public Repository(IDbConnectionFactory connectionFactory)
        {
            Contract.RequiresNotNull(connectionFactory, $"{nameof(connectionFactory)} != null");

            _connectionFactory = connectionFactory;
            _entityFactory = Activator.CreateInstance<EntityFactory<TEntity>>();
        }

        public async Task<TEntity> SelectFirstAsync(TableMap tableMap, Filter filter)
        {
            return await SelectFirstAsync(tableMap, SelectColumn.All(), filter);
        }

        public async Task<TEntity> SelectFirstAsync(TableMap tableMap, SelectColumn columns, Filter filter)
        {
            var builder = SqlBuilder.Select(tableMap);
            var sql = builder.BuildSelect(columns, filter, out object parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<dynamic>(sql, parameters);
                return _entityFactory.Build(result.FirstOrDefault());
            }
        }

        public async Task<IEnumerable<TEntity>> SelectAllAsync(TableMap tableMap, Filter filter)
        {
            return await SelectAllAsync(tableMap, SelectColumn.All(), filter);
        }

        public async Task<IEnumerable<TEntity>> SelectAllAsync(TableMap tableMap, SelectColumn columns, Filter filter)
        {
            var builder = SqlBuilder.Select(tableMap);
            var sql = builder.BuildSelect(columns, filter, out object parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<dynamic>(sql, parameters);

                return _entityFactory.BuildMultiple(result);
            }
        }

        public async Task<int> SaveAsync(TableMap tableMap, object value)
        {
            return await SaveAsync(tableMap, SelectColumn.All(), value);
        }

        public async Task<int> SaveAsync(TableMap tableMap, SelectColumn columns, object value)
        {
            var builder = SqlBuilder.Save(tableMap);
            var sql = builder.Build(columns, value, out object parameters);

            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync(sql, parameters);
                return Convert.ToInt32(result);
            }
        }

        public async Task UpdateAsync(TableMap tableMap, Filter filter, object value)
        {
            var builder = SqlBuilder.Update(tableMap);

            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = builder.Build(filter, value, out object parameters);
                await connection.ExecuteScalarAsync(sql, parameters);
            }
        }

        public async Task DeleteAsync(TableMap tableMap, Filter filter)
        {
            var builder = SqlBuilder.Delete(tableMap);

            using (var connection = _connectionFactory.CreateConnection())
            {
                var sql = builder.Build(filter, out object parameters);
                await connection.ExecuteScalarAsync(sql, parameters);
            }
        }

        public async Task<IEnumerable<TEntity>> SelectSqlAsync(string sql, object parameters)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<dynamic>(sql, parameters);
                return _entityFactory.BuildMultiple(result);
            }
        }

        public async Task UpdateSqlAsync(string sql, object parameters)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.ExecuteScalarAsync(sql, parameters);
            }
        }
    }
}
