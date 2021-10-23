using System.Configuration;

namespace SQLFundamentals.DataAccess.Controllers
{
    public abstract class Controller
    {
        protected string sqlConnectionString = ConfigurationManager.ConnectionStrings["SQLFundamentals"].ConnectionString;
    }
}