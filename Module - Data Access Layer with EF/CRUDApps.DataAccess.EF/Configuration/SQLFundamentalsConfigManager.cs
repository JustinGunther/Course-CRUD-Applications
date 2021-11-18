using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CRUDApps.DataAccess.EF.Configuration
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
