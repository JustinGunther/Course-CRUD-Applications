using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFundamentals.DataAccess
{
    public class SQLFundamentalsConfigManager : ISQLFundamentalsConfigManager
    {
        private readonly IConfiguration _configuration;

        public SQLFundamentalsConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SQLFundamentalsConnection
        {
            get
            {
                return _configuration["ConnectionStrings:SQLFundamentals"];
            }
        }

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName);
        }
    }
}
