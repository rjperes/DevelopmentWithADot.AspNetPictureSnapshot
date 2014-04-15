using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DevelopmentWithADot.AspNetPictureSnapshot
{
	public class PictureSnapshot : WebControl, ICallbackEventHandler
	{
		public PictureSnapshot() : base("video")
		{
		}

		public event EventHandler<PictureTakenEventArgs> PictureTaken;

		protected override void OnInit(EventArgs e)
		{
			var sm = ScriptManager.GetCurrent(this.Page);
			var reference = this.Page.ClientScript.GetCallbackEventReference(this, "picture", "function(result, context){ debugger; }", String.Empty, "function (error, context) { debugger; }", true);
			var takeSnapshotScript = String.Format("\ndocument.getElementById('{0}').takeSnapshot = function(){{ var video = document.getElementById('{0}'); var canvas = document.createElement('canvas'); canvas.width = video.width; canvas.height = video.height; var context = canvas.getContext('2d'); context.drawImage(video, 0, 0, video.width, video.height); var picture = canvas.toDataURL(); {1} }};\n", this.ClientID, reference);
			var startCaptureScript = String.Format("\ndocument.getElementById('{0}').startCapture = function(){{ var video = document.getElementById('{0}'); if ((video.paused == true) && (video.src == '')) {{ var getMedia = (navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia); debugger; getMedia = getMedia.bind(navigator); getMedia({{ video: true, audio: false }}, function (stream) {{ video.src = window.URL.createObjectURL(stream); }}, function (error) {{ debugger; }}) }}; video.play(); }};\n", this.ClientID);
			var stopCaptureScript = String.Format("\ndocument.getElementById('{0}').stopCapture = function(){{ var video = document.getElementById('{0}'); video.pause(); }};\n", this.ClientID);
			var script = String.Concat(takeSnapshotScript, startCaptureScript, stopCaptureScript);

			if (sm != null)
			{
				this.Page.ClientScript.RegisterStartupScript(this.GetType(), String.Concat("snapshot", this.ClientID), String.Format("Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function() {{ {0} }});\n", script), true);
			}
			else
			{
				this.Page.ClientScript.RegisterStartupScript(this.GetType(), String.Concat("snapshot", this.ClientID), script, true);
			}

			if (this.Width != Unit.Empty)
			{
				this.Attributes.Add(HtmlTextWriterAttribute.Width.ToString().ToLower(), this.Width.ToString());
			}

			if (this.Height != Unit.Empty)
			{
				this.Attributes.Add(HtmlTextWriterAttribute.Height.ToString().ToLower(), this.Height.ToString());
			}

			this.Attributes.Remove("autoplay");
			this.Attributes.Remove("controls");
			this.Attributes.Remove("crossorigin");
			this.Attributes.Remove("loop");
			this.Attributes.Remove("mediagroup");
			this.Attributes.Remove("muted");
			this.Attributes.Remove("poster");
			this.Attributes.Remove("preload");
			this.Attributes.Remove("src");

			base.OnInit(e);
		}

		protected virtual void OnPictureTaken(PictureTakenEventArgs e)
		{
			var handler = this.PictureTaken;

			if (handler != null)
			{
				handler(this, e);
			}
		}

		#region ICallbackEventHandler Members

		String ICallbackEventHandler.GetCallbackResult()
		{
			return (String.Empty);
		}

		void ICallbackEventHandler.RaiseCallbackEvent(String eventArgument)
		{
			this.OnPictureTaken(new PictureTakenEventArgs(eventArgument));
		}

		#endregion
	}
}
