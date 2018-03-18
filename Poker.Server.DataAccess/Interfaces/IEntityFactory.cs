namespace Poker.Server.DataAccess.Interfaces
{
    public interface IEntityFactory
    {
        TEntity Build<TEntity>(dynamic entity);
    }
}
