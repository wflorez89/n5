using Microsoft.EntityFrameworkCore;
using System;

namespace WilmerFlorez.Utilities.Implementation.UnitOfWorks
{
    public class StateHelper
    {
        public static EntityState ConvertState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Detached:
                    return EntityState.Unchanged;

                case EntityState.Unchanged:
                    return EntityState.Unchanged;

                case EntityState.Added:
                    return EntityState.Added;

                case EntityState.Deleted:
                    return EntityState.Deleted;

                case EntityState.Modified:
                    return EntityState.Modified;

                default:
                    throw new ArgumentOutOfRangeException("state");
            }
        }
    }
}
