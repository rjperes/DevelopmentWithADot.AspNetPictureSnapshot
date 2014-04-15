using System;
using System.Web.UI;

namespace DevelopmentWithADot.AspNetPictureSnapshot.Test
{
	public partial class Default : Page
	{
		protected void OnPictureTaken(Object sender, PictureTakenEventArgs e)
		{
			//do something with e.Picture
			e.Picture.RawFormat.ToString();
		}
	}
}