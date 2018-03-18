using Poker.Core.Maps;

namespace Poker.Models.TableMaps
{
    public class UserTableMap : TableMap
    {
        public UserTableMap() : base("User")
        {
            Map("Id", true);
            Map("UserName");
            Map("Password");
            Map("CreateDate");
            Map("BirthDate");
            Map("IsAdmin");
        }
    }
}
