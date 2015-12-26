<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title></title>
</head>
<body>
	<div>
		<%= Html.Encode(ViewData["Message"]) %>
	</div>
</body>

