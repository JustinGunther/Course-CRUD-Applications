using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFundamentals.DataAccess
{
    public interface ISQLFundamentalsConfigManager
    {
        string SQLFundamentalsConnection { get; }

        string GetConnectionString(string connectionName);
    }
}
