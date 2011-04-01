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
	/// Summary description for bannedip_edit.
	/// </summary>
	public partial class replacewords_edit : YAF.Classes.Base.AdminPage
	{

		protected void Page_Load( object sender, System.EventArgs e )
		{
			string strAddEdit = ( Request.QueryString ["i"] == null ) ? "Add" : "Edit";

			if ( !IsPostBack )
			{
				PageLinks.AddLink( PageContext.BoardSettings.Name, YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.forum ) );
				PageLinks.AddLink( "Administration", YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.admin_admin ) );
				PageLinks.AddLink( strAddEdit + " Word Replace", "" );

				BindData();
			}
			badword.Attributes.Add( "style", "width:250px" );
			goodword.Attributes.Add( "style", "width:250px" );
		}

		private void BindData()
		{
			int id;

			if ( Request.QueryString ["i"] != null && int.TryParse( Request.QueryString ["i"], out id ) )
			{
				DataRow row = YAF.Classes.Data.DB.replace_words_list( PageContext.PageBoardID, id ).Rows [0];
				badword.Text = ( string )row ["badword"];
				goodword.Text = ( string )row ["goodword"];
			}
		}

		private void add_Click( object sender, EventArgs e )
		{
			YAF.Classes.Data.DB.replace_words_save( PageContext.PageBoardID, Request.QueryString ["i"], badword.Text, goodword.Text );
			YafCache.Current.Remove( YafCache.GetBoardCacheKey( Constants.Cache.ReplaceWords ) );
			YAF.Classes.Utils.YafBuildLink.Redirect( YAF.Classes.Utils.ForumPages.admin_replacewords );
		}

		private void cancel_Click( object sender, EventArgs e )
		{
			YAF.Classes.Utils.YafBuildLink.Redirect( YAF.Classes.Utils.ForumPages.admin_replacewords );
		}

		#region Web Form Designer generated code
		override protected void OnInit( EventArgs e )
		{
			save.Click += new EventHandler( add_Click );
			cancel.Click += new EventHandler( cancel_Click );
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit( e );
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}

