using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Account.Helpers
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentTime();
    }
}
