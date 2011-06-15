using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PhishPond.Concrete.EventArgs;

namespace PhishMarket.Controls
{
    public partial class YearSelector : System.Web.UI.UserControl
    {
        public delegate void SelectYearCommandEventHandler(object sender, SelectYearCommandEventArgs e);
        public event SelectYearCommandEventHandler YearSelected;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void YearClicked(object sender, CommandEventArgs e)
        {
            var year = int.Parse(e.CommandArgument.ToString());

            OnYearClicked(new SelectYearCommandEventArgs { Year = year });
        }


        protected virtual void OnYearClicked(SelectYearCommandEventArgs e)
        {
            if (YearSelected != null) YearSelected(this, e);
        }


    }
}