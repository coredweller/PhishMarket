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
	/// Administrative Page for the editting of forum properties.
	/// </summary>
	public partial class editforum : YAF.Classes.Base.AdminPage
	{

		protected void Page_Load( object sender, System.EventArgs e )
		{
			if ( !IsPostBack )
			{
				PageLinks.AddLink( PageContext.BoardSettings.Name, YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.forum ) );
				PageLinks.AddLink( "Administration", YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.admin_admin ) );
				PageLinks.AddLink( "Forums", "" );

				BindData();
				if ( Request.QueryString ["f"] != null )
				{
					using ( DataTable dt = YAF.Classes.Data.DB.forum_list( PageContext.PageBoardID, Request.QueryString ["f"] ) )
					{
						DataRow row = dt.Rows [0];
						ForumFlags flags = new ForumFlags(row["Flags"]);
						Name.Text = ( string ) row ["Name"];
						Description.Text = ( string ) row ["Description"];
						SortOrder.Text = row ["SortOrder"].ToString();
						HideNoAccess.Checked = flags.IsHidden;
						Locked.Checked = flags.IsLocked;
						IsTest.Checked = flags.IsTest;
						ForumNameTitle.Text = Name.Text;
						Moderated.Checked = flags.IsModerated;

						CategoryList.SelectedValue = row ["CategoryID"].ToString();
						// populate parent forums list with forums according to selected category
						BindParentList();

						if ( !row.IsNull( "ParentID" ) )
							ParentList.SelectedValue = row ["ParentID"].ToString();
						if ( !row.IsNull( "ThemeURL" ) )
							ThemeList.SelectedValue = row ["ThemeURL"].ToString();

						remoteurl.Text = row ["RemoteURL"].ToString();
					}
					NewGroupRow.Visible = false;
				}
			}
		}

		private void BindData()
		{
			int ForumID = 0;
			CategoryList.DataSource = YAF.Classes.Data.DB.category_list( PageContext.PageBoardID, null );
			CategoryList.DataBind();

			if ( Request.QueryString ["f"] != null )
			{
				ForumID = Convert.ToInt32( Request.QueryString ["f"] );
				AccessList.DataSource = YAF.Classes.Data.DB.forumaccess_list( ForumID );
				AccessList.DataBind();
			}

			// Load forum's combo
			BindParentList();

			// Load forum's themes
			ListItem listheader = new ListItem();
			listheader.Text = "Choose a theme";
			listheader.Value = "";

			AccessMaskID.DataBind();

			ThemeList.DataSource = YafStaticData.Themes();
			ThemeList.DataTextField = "Theme";
			ThemeList.DataValueField = "FileName";
			ThemeList.DataBind();
			ThemeList.Items.Insert( 0, listheader );
		}

		private void BindParentList()
		{
			ParentList.DataSource = YAF.Classes.Data.DB.forum_listall_fromCat(PageContext.PageBoardID, CategoryList.SelectedValue);
			ParentList.DataValueField = "ForumID";
			ParentList.DataTextField = "Title";
			ParentList.DataBind();
		}

		public void Category_Change( object sender, System.EventArgs e )
		{
			BindParentList();
		}

		override protected void OnInit( EventArgs e )
		{
			this.CategoryList.AutoPostBack = true;
			this.Save.Click += new System.EventHandler( this.Save_Click );
			this.Cancel.Click += new System.EventHandler( this.Cancel_Click );
			base.OnInit( e );
		}

		private void Save_Click( object sender, System.EventArgs e )
		{
			if ( CategoryList.SelectedValue.Trim().Length == 0 )
			{
				PageContext.AddLoadMessage( "You must select a category for the forum." );
				return;
			}
			if ( Name.Text.Trim().Length == 0 )
			{
				PageContext.AddLoadMessage( "You must enter a name for the forum." );
				return;
			}
			if ( Description.Text.Trim().Length == 0 )
			{
				PageContext.AddLoadMessage( "You must enter a description for the forum." );
				return;
			}
			if ( SortOrder.Text.Trim().Length == 0 )
			{
				PageContext.AddLoadMessage( "You must enter a value for sort order." );
				return;
			}
			int sortOrder = 0;
			if ( !int.TryParse( SortOrder.Text.Trim(), out sortOrder ) )
			{
				PageContext.AddLoadMessage( "You must enter an number value for sort order." );
				return;
			}

			// Forum
			long ForumID = 0;
			if ( Request.QueryString ["f"] != null )
			{
				ForumID = long.Parse( Request.QueryString ["f"] );
			}
			else if ( AccessMaskID.SelectedValue.Length == 0 )
			{
				PageContext.AddLoadMessage( "You must select an initial access mask for the forum." );
				return;
			}

			object parentID = null;
			if ( ParentList.SelectedValue.Length > 0 )
				parentID = ParentList.SelectedValue;

			if (parentID !=null && parentID.ToString() == Request.QueryString["f"])
			{
				PageContext.AddLoadMessage("Forum cannot be parent of self.");
				return;
			}

			object themeURL = null;
			if ( ThemeList.SelectedValue.Length > 0 )
				themeURL = ThemeList.SelectedValue;

			ForumID = YAF.Classes.Data.DB.forum_save( ForumID, CategoryList.SelectedValue, parentID, Name.Text, Description.Text, sortOrder, Locked.Checked, HideNoAccess.Checked, IsTest.Checked, Moderated.Checked, AccessMaskID.SelectedValue, IsNull( remoteurl.Text ), themeURL, false );

			// Access
			if ( Request.QueryString ["f"] != null )
			{
				for ( int i = 0; i < AccessList.Items.Count; i++ )
				{
					RepeaterItem item = AccessList.Items [i];
					int GroupID = int.Parse( ( ( Label ) item.FindControl( "GroupID" ) ).Text );
					YAF.Classes.Data.DB.forumaccess_save( ForumID, GroupID, ( ( DropDownList ) item.FindControl( "AccessmaskID" ) ).SelectedValue );
				}

				ClearCaches();
				YAF.Classes.Utils.YafBuildLink.Redirect( YAF.Classes.Utils.ForumPages.admin_forums );
			}

			ClearCaches();
			// Done
			YAF.Classes.Utils.YafBuildLink.Redirect( YAF.Classes.Utils.ForumPages.admin_editforum, "f={0}", ForumID );
		}

		private static void ClearCaches()
		{
			// clear moderatorss cache
			YafCache.Current.Remove( YafCache.GetBoardCacheKey( Constants.Cache.ForumModerators ) );
			// clear category cache...
			YafCache.Current.Remove( YafCache.GetBoardCacheKey( Constants.Cache.ForumCategory ) );
		}

		private void Cancel_Click( object sender, System.EventArgs e )
		{
			YAF.Classes.Utils.YafBuildLink.Redirect( YAF.Classes.Utils.ForumPages.admin_forums );
		}

		protected void BindData_AccessMaskID( object sender, System.EventArgs e )
		{
			( ( DropDownList ) sender ).DataSource = YAF.Classes.Data.DB.accessmask_list( PageContext.PageBoardID, null );
			( ( DropDownList ) sender ).DataValueField = "AccessMaskID";
			( ( DropDownList ) sender ).DataTextField = "Name";
		}

		private void InitializeComponent()
		{

		}

		protected void SetDropDownIndex( object sender, System.EventArgs e )
		{
			try
			{
				DropDownList list = ( DropDownList ) sender;
				list.Items.FindByValue( list.Attributes ["value"] ).Selected = true;
			}
			catch ( Exception )
			{
			}
		}
	}
}
