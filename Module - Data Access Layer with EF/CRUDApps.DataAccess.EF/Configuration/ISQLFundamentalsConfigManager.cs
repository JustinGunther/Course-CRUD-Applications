using System;
using System.Collections.Generic;
using System.Text;

namespace CRUDApps.DataAccess.EF.Configuration
{
    public interface ISQLFundamentalsConfigManager
    {
        string SQLFundamentalsConnection { get; }

        string GetConnectionString(string connectionName);
    }
}
