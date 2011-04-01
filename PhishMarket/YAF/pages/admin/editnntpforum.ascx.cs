/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bj�rnar Henden
 * Copyright (C) 2006-2009 Jaben Cargman
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
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using YAF.Classes.Utils;
using YAF.Classes.Data;

namespace YAF.Pages.Admin
{
	/// <summary>
	/// Summary description for editgroup.
	/// </summary>
	public partial class editnntpforum : YAF.Classes.Base.AdminPage
	{

		protected void Page_Load( object sender, System.EventArgs e )
		{
			if ( !IsPostBack )
			{
				PageLinks.AddLink( PageContext.BoardSettings.Name, YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.forum ) );
				PageLinks.AddLink( "Administration", YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.admin_admin ) );
				PageLinks.AddLink( "NNTP Forums", "" );

				BindData();
				if ( Request.QueryString ["s"] != null )
				{
					using ( DataTable dt = YAF.Classes.Data.DB.nntpforum_list( PageContext.PageBoardID, null, Request.QueryString ["s"], DBNull.Value ) )
					{
						DataRow row = dt.Rows [0];
						NntpServerID.Items.FindByValue( row ["NntpServerID"].ToString() ).Selected = true;
						GroupName.Text = row ["GroupName"].ToString();
						ForumID.Items.FindByValue( row ["ForumID"].ToString() ).Selected = true;
						Active.Checked = ( bool ) row ["Active"];
					}
				}
			}
		}

		private void BindData()
		{
			NntpServerID.DataSource = YAF.Classes.Data.DB.nntpserver_list( PageContext.PageBoardID, null );
			NntpServerID.DataValueField = "NntpServerID";
			NntpServerID.DataTextField = "Name";
			ForumID.DataSource = YAF.Classes.Data.DB.forum_listall_sorted( PageContext.PageBoardID, PageContext.PageUserID );
			ForumID.DataValueField = "ForumID";
			ForumID.DataTextField = "Title";
			DataBind();
		}

		protected void Cancel_Click( object sender, System.EventArgs e )
		{
			YAF.Classes.Utils.YafBuildLink.Redirect( YAF.Classes.Utils.ForumPages.admin_nntpforums );
		}

		protected void Save_Click( object sender, System.EventArgs e )
		{
			object nntpForumID = null;
			if ( Request.QueryString ["s"] != null ) nntpForumID = Request.QueryString ["s"];
			YAF.Classes.Data.DB.nntpforum_save( nntpForumID, NntpServerID.SelectedValue, GroupName.Text, ForumID.SelectedValue, Active.Checked );
			YAF.Classes.Utils.YafBuildLink.Redirect( YAF.Classes.Utils.ForumPages.admin_nntpforums );
		}
	}
}
