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
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections.Generic;
using YAF.Classes.Utils;
using YAF.Classes.Data;

namespace YAF.Pages // YAF.Pages
{
	/// <summary>
	/// Summary description for avatar.
	/// </summary>
	public partial class avatar : YAF.Classes.Base.ForumPage
	{
		protected System.Web.UI.WebControls.Label title;

		public int pagenum = 0;
		public int pagesize = 20;

		private int returnUserID = 0;

		string filepath = "";

		public avatar()
			: base( "AVATAR" )
		{
		}

		protected string CurrentDirectory
		{
			get
			{
				if ( ViewState ["CurrentDir"] != null )
					return ( string ) ViewState ["CurrentDir"];
				else
					return "";
			}
			set
			{
				ViewState ["CurrentDir"] = value;
			}
		}

		protected void Page_Load( object sender, System.EventArgs e )
		{
			if ( Request.QueryString ["u"] != null )
			{
				returnUserID = Convert.ToInt32( Request.QueryString ["u"] );
			}

			if ( !IsPostBack )
			{
				PageLinks.AddLink( PageContext.BoardSettings.Name, YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.forum ) );

				if ( returnUserID > 0 )
				{
					PageLinks.AddLink( "Administration", YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.admin_admin ) );
					PageLinks.AddLink( "Users", YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.admin_users ) );
				}
				else
				{
					PageLinks.AddLink( HtmlEncode( PageContext.PageUserName ), YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.cp_profile ) );
					PageLinks.AddLink( GetText( "CP_EDITAVATAR", "TITLE" ), YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.cp_editavatar ) );
				}
				PageLinks.AddLink( GetText( "TITLE" ), "" );
				BindData();
			}
		}

		private void BindData()
		{
			string strDirectory = YafForumInfo.ForumRoot + "images/avatars/" + CurrentDirectory;

			DirectoryInfo baseDirectory = new DirectoryInfo( Server.MapPath( strDirectory ) );

			if ( CurrentDirectory == "" )
			{
				files.Visible = false;
				directories.Visible = true;
				directories.DataSource = DirectoryListClean( baseDirectory );
				directories.DataBind();
			}
			else
			{
				files.Visible = true;
				directories.Visible = false;
				files.DataSource = FilesListClean( baseDirectory );
				files.DataBind();
			}
		}

		protected List<DirectoryInfo> DirectoryListClean( DirectoryInfo baseDir )
		{
			DirectoryInfo [] directories = baseDir.GetDirectories();
			List<DirectoryInfo> directoryList = new List<DirectoryInfo>();

			foreach ( DirectoryInfo dir in directories )
			{
				if ((dir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden &&
						(dir.Attributes & FileAttributes.System) != FileAttributes.System)
				{
					// add it since it's not hidden or system
					directoryList.Add( dir );
				}
			}

			return directoryList;
		}

		protected List<FileInfo> FilesListClean( DirectoryInfo baseDir )
		{
			FileInfo [] files = baseDir.GetFiles("*.*");
			List<FileInfo> filesList = new List<FileInfo>();

			foreach ( FileInfo file in files )
			{
				if (( file.Attributes & FileAttributes.Hidden ) != FileAttributes.Hidden &&
						( file.Attributes & FileAttributes.System ) != FileAttributes.System &&
							IsValidAvatarExtension(file.Extension.ToLower()))
				{
					// add it since it's not hidden or system
					filesList.Add( file );
				}
			}

			return filesList;
		}

		protected bool IsValidAvatarExtension( string extension )
		{
			if ( extension == ".gif" || extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp" )
			{
				return true;
			}

			return false;
		}

		public void Files_Bind( object sender, DataListItemEventArgs e )
		{
			string strDirectory = YafForumInfo.ForumRoot + "images/avatars/" + CurrentDirectory;

			Literal fname = ( Literal ) e.Item.FindControl( "fname" );

			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				FileInfo finfo = new FileInfo( Server.MapPath( Convert.ToString( DataBinder.Eval( e.Item.DataItem, "name" ) ) ) );
				string tmpExt = finfo.Extension.ToLower();

				if ( tmpExt == ".gif" || tmpExt == ".jpg" || tmpExt == ".jpeg" || tmpExt == ".png" || tmpExt == ".bmp" )
				{
					string link;

					if ( returnUserID > 0 )
					{
						link = YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.admin_edituser, "u={0}&av={1}", returnUserID, Server.UrlEncode( CurrentDirectory + "/" + finfo.Name ) );
					}
					else
					{
						link = YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.cp_editavatar, "av={0}", Server.UrlEncode(CurrentDirectory + "/" + finfo.Name ));
					}

					fname.Text = string.Format( @"<div align=""center""><a href=""{0}""><img src=""{1}"" alt=""{2}"" class=""borderless"" /></a><br /><small>{2}</small></div>{3}", link, ( strDirectory + "/" + finfo.Name ), finfo.Name, Environment.NewLine );
				}
			}

			if (e.Item.ItemType == ListItemType.Header) 
			{
				// get the previous directory...
				string previousDirectory = "";
				string [] pardir = CurrentDirectory.Split( '/' );
				for ( int i = 0; i < pardir.Length - 1; i++ )
				{
					previousDirectory += pardir [i] + "/";
				}
				if ( previousDirectory.Length > 0 )
				{
					previousDirectory = previousDirectory.Substring( 0, previousDirectory.Length - 1 );
				}

				LinkButton up = e.Item.FindControl( "up" ) as LinkButton;
				up.CommandArgument = previousDirectory;
				up.Text = String.Format( @"<p align=""center""><img src=""{0}"" alt=""Up"" /><br />UP</p>", YafForumInfo.ForumRoot + "images/folder.gif" );
			}
		}

		protected override void Render( HtmlTextWriter writer )
		{
			base.Render( writer );
		}

		public void Directories_Bind( object sender, DataListItemEventArgs e )
		{
			string strDirectory = YafForumInfo.ForumRoot + "images/avatars/";

			if ( e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem )
			{
				LinkButton dirName = e.Item.FindControl( "dirName" ) as LinkButton;
				dirName.CommandArgument = filepath + Convert.ToString( DataBinder.Eval( e.Item.DataItem, "name" ) );
				dirName.Text = String.Format( @"<p align=""center""><img src=""{0}"" alt=""{1}"" /><br />{1}</p>", YafForumInfo.ForumRoot + "images/folder.gif", Convert.ToString( DataBinder.Eval( e.Item.DataItem, "name" ) ) );
			}
		}

		protected void ItemCommand( object source, DataListCommandEventArgs e )
		{
			if ( e.CommandName == "directory" )
			{
				CurrentDirectory = e.CommandArgument.ToString();
				BindData();
			}
		}
}
}
