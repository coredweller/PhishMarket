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
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using YAF.Classes.Utils;
using YAF.Classes.Data;

namespace YAF.Pages // YAF.Pages
{
	/// <summary>
	/// Information control displaying feedback information to users.
	/// </summary>
	public partial class info : YAF.Classes.Base.ForumPage
	{
		#region Constructors & Overridden Methods

		/// <summary>
		/// Default constructor.
		/// </summary>
		public info()
			: base("INFO")
		{
			CheckSuspended = false;
		}


		/// <summary>
		/// Creates page links for this page.
		/// </summary>
		protected override void CreatePageLinks()
		{
			// forum index
			PageLinks.AddLink(PageContext.BoardSettings.Name, YafBuildLink.GetLink(ForumPages.forum));
			// information title text
			PageLinks.AddLink(Title.Text);
		}

		#endregion


		#region Event Handlers

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!IsPostBack)
			{
				// localize button label
				Continue.Text = GetText("continue");

				// get redirect URL from parameter
				if (Request.QueryString["url"] != null)
				{
					// unescape ampersands
					RefreshURL = Request.QueryString["url"].Replace("&amp;", "&");
				}

				// try to get infomessage code from parameter
				try
				{
					// compare it converted to enumeration
					switch ((InfoMessage)int.Parse(Request.QueryString["i"]))
					{
						case InfoMessage.Moderated: /// Moderated
							Title.Text = GetText("title_moderated");
							Info.Text = GetText("moderated");
							break;
						case InfoMessage.Suspended: /// Suspended
							Title.Text = GetText("title_suspended");
							Info.Text = String.Format(GetText("suspended"), YafDateTime.FormatDateTime(PageContext.SuspendedUntil));
							break;
						case InfoMessage.RegistrationEmail: /// Registration email
							Title.Text = GetText("title_registration");
							Info.Text = GetText("registration");
							RefreshTime = 10;
							RefreshURL = YafBuildLink.GetLink(ForumPages.login);
							break;
						case InfoMessage.AccessDenied: /// Access Denied
							Title.Text = GetText("title_accessdenied");
							Info.Text = GetText("accessdenied");
							RefreshTime = 10;
							RefreshURL = YafBuildLink.GetLink(ForumPages.forum);
							break;
						case InfoMessage.Disabled: /// Disabled feature
							Title.Text = GetText("TITLE_ACCESSDENIED");
							Info.Text = GetText("DISABLED");
							RefreshTime = 10;
							RefreshURL = YafBuildLink.GetLink(ForumPages.forum);
							break;
						case InfoMessage.Invalid: /// Invalid argument!
							Title.Text = GetText("TITLE_INVALID");
							Info.Text = GetText("INVALID");
							RefreshTime = 10;
							RefreshURL = YafBuildLink.GetLink(ForumPages.forum);
							break;
						case InfoMessage.Failure: // some sort of failure
							Title.Text = GetText("TITLE_FAILURE");
							Info.Text = GetText("FAILURE");
							RefreshTime = 10;
							RefreshURL = YafBuildLink.GetLink(ForumPages.forum);
							break;
					}
				}
				// exception was thrown
				catch (Exception)
				{
					// get title for exception message
					Title.Text = GetText("title_exception");
					// exception message
					Info.Text = string.Format("{1} <b>{0}</b>.", PageContext.PageUserName, GetText("exception"));

					// redirect to forum main after 2 seconds
					RefreshTime = 2;
					RefreshURL = YafBuildLink.GetLink(ForumPages.forum);
				}

				// set continue button URL and visibility
				Continue.NavigateUrl = RefreshURL;
				Continue.Visible = RefreshURL != null;

				// create page links - must be placed after switch to display correct title (last breadcrumb trail)
				CreatePageLinks();
			}
		}
		
		#endregion


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
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
