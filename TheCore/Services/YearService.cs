using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace TheCore.Services
{
    public class YearService
    {
        public List<int> AllYears = new List<int> { 1986, 1987, 1988, 1989, 1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000, 2003, 2004, 2009, 2010, 2011 };
        public List<int> ThreePoint0Years = new List<int> { 2009, 2010, 2011 };
        public List<int> TwoPoint0Years = new List<int> { 2003, 2004 };
        public List<int> OnePoint0Years = new List<int> { 1987, 1988, 1989, 1990, 1991, 1992, 1993, 1994, 1995, 1996, 1997, 1998, 1999, 2000 };

        public ListItem[] GetAllPhishYears()
        {
            return GetYearsList(AllYears);
        }

        public ListItem[] GetPhish3Point0Years()
        {
            return GetYearsList(ThreePoint0Years);
        }

        private ListItem[] GetYearsList(List<int> years)
        {
            var list = new ListItem[years.Count];
            var count = 0;

            foreach (var year in years)
            {
                list[count] = new ListItem(year.ToString(), year.ToString());
                count++;
            }

            return list;
        }
    }
}
