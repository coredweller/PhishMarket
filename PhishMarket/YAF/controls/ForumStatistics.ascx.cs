/* Yet Another Forum.NET
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
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using YAF.Classes.Utils;
using YAF.Classes.Data;

namespace YAF.Controls
{
	public partial class ForumStatistics : YAF.Classes.Base.BaseUserControl
	{
		public ForumStatistics()
		{
			this.Load += new EventHandler( ForumStatistics_Load );
		}

		void ForumStatistics_Load( object sender, EventArgs e )
		{

		}

		public override void DataBind()
		{
			BindData();
			base.DataBind();
		}

		protected void BindData()
		{
			// Active users
			// Call this before forum_stats to clean up active users
			ActiveUsers1.ActiveUserTable = YAF.Classes.Data.DB.active_list( PageContext.PageBoardID, null );

			// "Active Users" Count and Most Users Count
			DataRow activeStats = YAF.Classes.Data.DB.active_stats( PageContext.PageBoardID );

			ActiveUserCount.Text = FormatActiveUsers( activeStats );

			// Forum Statistics
			string key = YafCache.GetBoardCacheKey( Constants.Cache.BoardStats );
			DataRow statisticsDataRow = ( DataRow ) Cache [key];
			if ( statisticsDataRow == null )
			{
				statisticsDataRow = YAF.Classes.Data.DB.board_poststats( PageContext.PageBoardID );
				Cache.Insert( key, statisticsDataRow, null, DateTime.Now.AddMinutes( PageContext.BoardSettings.ForumStatisticsCacheTimeout ), TimeSpan.Zero );
			}

			// show max users...
			if ( !statisticsDataRow.IsNull( "MaxUsers" ) )
			{
				MostUsersCount.Text = String.Format( PageContext.Localization.GetText( "MAX_ONLINE" ), statisticsDataRow ["MaxUsers"], YafDateTime.FormatDateTimeTopic( statisticsDataRow ["MaxUsersWhen"] ) );
			}
			else
			{
				MostUsersCount.Text = String.Format( PageContext.Localization.GetText( "MAX_ONLINE" ), activeStats ["ActiveUsers"], YafDateTime.FormatDateTimeTopic( DateTime.Now ) );
			}

			// Posts and Topic Count...
			StatsPostsTopicCount.Text = String.Format( PageContext.Localization.GetText( "stats_posts" ), statisticsDataRow ["posts"], statisticsDataRow ["topics"], statisticsDataRow ["forums"] );

			// Last post
			if ( !statisticsDataRow.IsNull( "LastPost" ) )
			{
				StatsLastPostHolder.Visible = true;

				LastPostUserLink.UserID = Convert.ToInt32(statisticsDataRow ["LastUserID"]);
				LastPostUserLink.UserName = statisticsDataRow ["LastUser"].ToString();

				StatsLastPost.Text = String.Format( PageContext.Localization.GetText( "stats_lastpost" ), YafDateTime.FormatDateTimeTopic( ( DateTime ) statisticsDataRow ["LastPost"] ) );
			}
			else
			{
				StatsLastPostHolder.Visible = false;
			}
			
			// Member Count
			StatsMembersCount.Text = String.Format( PageContext.Localization.GetText( "stats_members" ), statisticsDataRow ["members"] );

			// Newest Member
			StatsNewestMember.Text = PageContext.Localization.GetText( "stats_lastmember" );
			NewestMemberUserLink.UserID = Convert.ToInt32( statisticsDataRow ["LastMemberID"] );
			NewestMemberUserLink.UserName = statisticsDataRow ["LastMember"].ToString();
		}

		protected string FormatActiveUsers( DataRow activeStats )
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			int activeUsers = Convert.ToInt32( activeStats ["ActiveUsers"] );
			int activeHidden = Convert.ToInt32( activeStats ["ActiveHidden"] );
			int activeMembers = Convert.ToInt32( activeStats["ActiveMembers"]);
			int activeGuests = Convert.ToInt32( activeStats["ActiveGuests"]);

			// show hidden count to admin...
			if ( PageContext.IsAdmin ) activeUsers += activeHidden;

			if ( General.CheckPermission( PageContext, PageContext.BoardSettings.ActiveUsersViewPermissions ) )
			{
				// always show active users...
				sb.Append( String.Format( "<a href=\"{1}\">{0}</a>",
					String.Format( PageContext.Localization.GetText( activeUsers == 1 ? "ACTIVE_USERS_COUNT1" : "ACTIVE_USERS_COUNT2" ), activeUsers ),
					YAF.Classes.Utils.YafBuildLink.GetLink( YAF.Classes.Utils.ForumPages.activeusers ) ) );
			}
			else
			{
				// no link because no permissions...
				sb.Append( String.Format( PageContext.Localization.GetText( activeUsers == 1 ? "ACTIVE_USERS_COUNT1" : "ACTIVE_USERS_COUNT2" ), activeUsers ) );
			}

			if ( activeMembers > 0 ) 
			{
				sb.Append( String.Format( ", {0}", String.Format( PageContext.Localization.GetText( activeMembers == 1 ? "ACTIVE_USERS_MEMBERS1" : "ACTIVE_USERS_MEMBERS2" ), activeMembers ) ) );
			}

			if ( activeGuests > 0 ) 
			{
				sb.Append( String.Format( ", {0}", String.Format( PageContext.Localization.GetText( activeGuests == 1 ? "ACTIVE_USERS_GUESTS1" : "ACTIVE_USERS_GUESTS2" ), activeGuests ) ) );
			}

			if ( activeHidden > 0 && PageContext.IsAdmin )
			{	
				sb.Append( String.Format( ", {0}", String.Format( PageContext.Localization.GetText( "ACTIVE_USERS_HIDDEN" ), activeHidden ) ) );
			}

			return sb.ToString();
		}

		public event EventHandler<EventArgs> NeedDataBind;

		protected void CollapsibleImage_OnClick( object sender, ImageClickEventArgs e )
		{
			if ( NeedDataBind != null ) NeedDataBind( this, new EventArgs() );
		}
	}
}