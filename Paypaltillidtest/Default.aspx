<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="Paypaltillidtest._Default" %>

<h2>
    Welcome to paypaltillidtest!
</h2>
<form action="https://sandbox.paypal.com/cgi-bin/webscr" method="post">
<input type="hidden" name="cmd" value="_xclick" />
<input type="hidden" name="business" value="venkat_1361429954_biz@tillidsoft.com" />
<input type="hidden" name="item_name" value="Plan1" />
<input type="hidden" name="amount" value="10.00" />
<input type="submit" value="Buy!" />
</form>
