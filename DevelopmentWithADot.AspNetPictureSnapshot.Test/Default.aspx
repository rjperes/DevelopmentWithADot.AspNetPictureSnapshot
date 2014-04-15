<%@ Page Language="C#" CodeBehind="Default.aspx.cs" Inherits="DevelopmentWithADot.AspNetPictureSnapshot.Test.Default" %>
<%@ Register assembly="DevelopmentWithADot.AspNetPictureSnapshot" namespace="DevelopmentWithADot.AspNetPictureSnapshot" tagPrefix="web" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<script type="text/javascript">
		
		function takeSnapshot()
		{
			document.getElementById('picture').takeSnapshot();
		}

		function startCapture()
		{
			document.getElementById('picture').startCapture();
		}

		function stopCapture()
		{
			document.getElementById('picture').stopCapture();
		}

	</script>
</head>
<body>
	<form runat="server">
	<div>
		<web:PictureSnapshot runat="server" ID="picture" Width="400px" Height="400px" ClientIDMode="Static" OnPictureTaken="OnPictureTaken"/>
		<br/>
		<input type="button" value="Start Capturing" onclick="startCapture()"/>
		<input type="button" value="Take Snapshot" onclick="takeSnapshot()" />
		<input type="button" value="Stop Capturing" onclick="stopCapture()" />
	</div>
	</form>
</body>
</html>
