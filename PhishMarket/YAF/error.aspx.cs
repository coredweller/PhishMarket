/* Yet Another Forum.net
 * Copyright (C) 2003 Bj�rnar Henden
 * http://www.yetanotherforum.net/
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace YAF
{
	/// <summary>
	/// Summary description for error.
	/// </summary>
	public partial class error : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//YAF.Classes.Utils.YafBuildLink.Redirect( YAF.Classes.Utils.ForumPages.error,Request.QueryString.ToString());
      if ( !IsPostBack )
      {
        string errorMessage = @"There has been a serious error loading the forum. No further information is available.";

        // show error message if one was provided...
        if ( Session[ "StartupException" ] != null )
        {
          errorMessage = "Startup Error: " + Session ["StartupException"].ToString();
          Session ["StartupException"] = null;
        }

        ErrorMsg.Text = errorMessage + "<br/><br/>" + "Please contact the administrator if this message persists.";
      }
		}
	}
}
