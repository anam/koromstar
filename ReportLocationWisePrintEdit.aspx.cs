using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class LocationWiseReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadReport();
        }
    }

    private void loadReport()
    {
        string html = "";
        string sql = @"
SELECT     TRANS.LOCATIONID, TRANS.CUSTID, TRANS.RECEIVERID, TRANS.TRANSID, 
TRANS.TRANSAMOUNT, TRANS.TRANSFEES, 
	TRANS.TRANSTOTALAMOUNT, TRANS.REFCODE, TRANS.TRANSDT, 
CUSTOMER.CUSTFNAME+' '+CUSTOMER.CUSTMNAME+' '+CUSTOMER.CUSTLNAME as SenderName,
RECEIVER.RECEIVERFNAME+' '+RECEIVER.RECEIVERMNAME+' '+RECEIVER.RECEIVERLNAME as ReceiverName,
LOCATION.COUNTRY, LOCATION.CITY, LOCATION.BRANCH,TRANS.[TRANSSTATUS]
FROM         TRANS INNER JOIN
	LOCATION ON TRANS.LOCATIONID = LOCATION.LOCATIONID INNER JOIN
	CUSTOMER ON TRANS.CUSTID = CUSTOMER.CUSTOMERID INNER JOIN
	RECEIVER ON TRANS.RECEIVERID = RECEIVER.RECEIVERID
where 1=1 
";
        if (Request.QueryString["fromDate"] != null)
        {
            sql += @"
               and TRANSDT between '" + DateTime.Parse(Request.QueryString["fromDate"]).ToString("yyyy-MM-dd") + @" 00:00:00 AM' and '" + DateTime.Parse(Request.QueryString["toDate"]).ToString("yyyy-MM-dd") + @" 11:59:59 PM'
                ";
        }
        
        if (Request.QueryString["Code"] != null)
        {
            sql += @"
               and REFCODE ='" + Request.QueryString["Code"] + @"'
                ";
        }
        sql += @"
order by
--LOCATION.COUNTRY,LOCATION.CITY,LOCATION.BRANCH,
--TRANSDT desc
TRANS.REFCODE,TRANSDT
";
       
        DataSet ds = MSSQL.SQLExec(sql);
        html = @"<table border='1' cellspacing='0' cellpadding='5' width='100%'>
<tr><td colspan='10' align='center'>
<center>";
        if (Request.QueryString["fromDate"] != null)
        {
            html += DateTime.Parse(Request.QueryString["fromDate"]).ToString("dd MMM yyyy") + " to " + DateTime.Parse(Request.QueryString["toDate"]).ToString("dd MMM yyyy");
           
        }

        if (Request.QueryString["Code"] != null)
        {
            html += Request.QueryString["Code"]; 

        }

        html += @"</center> 
        </td></tr>
        </table>
        ";
        lblPrint.Text = html;
        gvTras.DataSource = ds.Tables[0];
        gvTras.DataBind();

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvTras.Rows)
        {
            HiddenField hfTRANSID = (HiddenField)gvr.FindControl("hfTRANSID");
            TextBox txtRefCode = (TextBox)gvr.FindControl("txtRefCode");
            if (txtRefCode.Text.Trim() != "")
            {
                MSSQL.SQLExec("Update TRANS set REFCODE='"+txtRefCode.Text+"' where TRANSID=" + hfTRANSID.Value);
            }
        }
    }
}