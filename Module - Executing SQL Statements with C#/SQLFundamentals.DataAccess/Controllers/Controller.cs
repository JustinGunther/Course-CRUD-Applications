using System.Configuration;

namespace SQLFundamentals.DataAccess.Controllers
{
    public abstract class Controller
    {
        protected static string sqlConnectionString = ConfigurationManager.ConnectionStrings["SQLFundamentals"].ConnectionString;
    }
}