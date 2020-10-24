using System.ComponentModel.DataAnnotations;
using Serilog;

namespace CoreMentoringApp.WebSite.Options
{
    public class ProductViewOptions
    {
        public const string ProductView = "ProductView";

        private int _amount;
        
        [Range(0, int.MaxValue)]
        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                Log.Information("Product view option Amount is set to: {value}", value);
            }
        }
    }
}
