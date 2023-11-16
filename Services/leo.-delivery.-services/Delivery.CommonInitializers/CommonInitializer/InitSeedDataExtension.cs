using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.CommonInitializers.CommonInitializer
{
    public class InitSeedDataExtension
    {
        private readonly UserDbContext _userDbContext;

        public InitSeedDataExtension(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
    }
}
