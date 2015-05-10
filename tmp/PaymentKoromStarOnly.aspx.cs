using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class SearchSenderPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            {
                //reLoadSession();
                loadLocationInfo();
                txtDate.Text = DateTime.Today.ToShortDateString();
                /*
                     if (Session["snsenderID"] != null && Session["snreceiverID"] != null && Session["snlocationID"] != null)
                     {
                         loadSenderInfo();
                         loadReceiverInfo();
                         loadLocationInfo();
                         //loadTransSession();
                         txtDate.Text = DateTime.Today.ToShortDateString();

                     }
                     else
                     {
                         //Response.Redirect("SearchSenderPage.aspx");
                     }
                  */
            }
            
        }
        //reLoadSession();
    }

    private void reLoadSession()
    {
        if (Session["userType"] == null || Session["aGENT"] != null || Session["lOCATION"] != null)
        {
            USERINFO userInfo = new USERINFO();
            userInfo = USERINFOManager.GetUSERINFOByUserNameType("Agent", User.Identity.Name);//"Agent" is dami in database i have not use the

            if (userInfo != null)
            {
                Session["userType"] = userInfo.Type.ToString();
                Session["userInfoID"] = userInfo.USERINFOID.ToString();
                Session["userName"] = userInfo.UserName.ToString();

                //if (userInfo.Agent_LocationID.ToString() == ddlAgent.SelectedItem.Value.ToString())
                if (userInfo.Type == "Agent")
                {
                    Session["aGENT"] = AGENTManager.GetAGENTByID(userInfo.Agent_LocationID);
                    Session["role"] = "Agent";
                }
                //else if (userInfo.Type == "Location")
                //{
                //    Session["lOCATION"] = LOCATIONGROUPManager.GetLOCATIONGROUPByID(userInfo.Agent_LocationID);
                //    Session["role"] = "Location";
                //}

            }
        }
    }

    protected void loadSenderInfo()
    {
        CUSTOMER cUSTOMER = new CUSTOMER();
        cUSTOMER = CUSTOMERManager.GetCUSTOMERByID(int.Parse(Session["snsenderID"].ToString()));

        if (cUSTOMER != null)
        {
            txtName.Text = cUSTOMER.CUSTFNAME.ToString() + " " + cUSTOMER.CUSTMNAME.ToString() + " " + cUSTOMER.CUSTLNAME.ToString();
            txtAddress1.Text = cUSTOMER.CUSTADDRESS1.ToString();
            txtAddress2.Text = cUSTOMER.CUSTADDRESS2.ToString();
            txtCity.Text = cUSTOMER.CUSTCITY.ToString();
            txtState.Text = cUSTOMER.CUSTSTATE.ToString();
            txtZIP.Text = cUSTOMER.CUSTZIP.ToString();
            txtPhoneNumber.Text = cUSTOMER.CUSTWPHONE.ToString();

        }

    }

    protected void loadReceiverInfo()
    {

        RECEIVER rECEIVER = new RECEIVER();
        rECEIVER = RECEIVERManager.GetRECEIVERByID(int.Parse(Session["snreceiverID"].ToString()));

        txtFirstName.Text = rECEIVER.RECEIVERFNAME.ToString();
        txtMiddleName.Text = rECEIVER.RECEIVERMNAME.ToString();
        txtLastName.Text = rECEIVER.RECEIVERLNAME.ToString();
        txtReceiverCity.Text = rECEIVER.RECEIVERCITY.ToString();

    }

    protected void loadLocationInfo()
    {
        //LOCATION lOCATION = new LOCATION();
        //lOCATION = LOCATIONManager.GetLOCATIONByID(int.Parse(Session["snlocationID"].ToString()));
        //txtBranch.Text = lOCATION.BRANCH.ToString();
        //lblRate.Text = lOCATION.AGENTRATE.ToString();
        string sql = "Select BRANCH,LocationID from LOCATION order by BRANCH";
        DataSet ds = MSSQL.SQLExec(sql);
        ddlLocationID.Items.Clear();
        ddlLocationID.Items.Add(new ListItem("Select","0"));
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            ddlLocationID.Items.Add(new ListItem(dr["BRANCH"].ToString(), dr["LocationID"].ToString()));
        }
    }

    protected void loadTransSession()
    {
        try
        {


            AGENT aGENT = (AGENT)Session["aGENT"];
            DateTime nullDate = DateTime.Parse(ConfigurationManager.AppSettings["NULL_DATE"].ToString());

            TRANS tRANS = new TRANS();

            tRANS.CUSTID = int.Parse(Session["snsenderID"].ToString());
            tRANS.RECEIVERID = int.Parse(Session["snreceiverID"].ToString());
            tRANS.LOCATIONID = int.Parse(Session["snlocationID"].ToString());
            tRANS.TRANSDT = DateTime.Parse(txtDate.Text); 
            tRANS.TRANSAMOUNT = decimal.Parse(txtSendingAmount.Text);
            tRANS.TRANSFEES = decimal.Parse(txtServiceCharge.Text);
            tRANS.TRANSOTHERFEES = decimal.Parse(txtOtherServiceCharge.Text);
            tRANS.CAUSETRANSOTHERFEES = txtCauseOtherServiceCharges.Text;
            tRANS.TRANSPROMOCODE = txtDiscount.Text;
            tRANS.TRANSPROMO = 0;
            tRANS.TRANSTOTALAMOUNT = decimal.Parse(txtTotalCharge.Text);
            tRANS.FLAG_SM_RECEIVER = 'N';
            tRANS.SM_RECEIVER = "";
            tRANS.FLAG_CALL_RECEIVER = 'N';
            tRANS.RECEIVERPHONENO = "";
            tRANS.FLAG_DD = 'N';
            tRANS.FLAG_TESTQUESTION = 'N';
            tRANS.TESTQUESTION = txtTestQuestion.Text;
            tRANS.TESTANSWER = txtTestAnswer.Text;
            tRANS.FLAG_CALLSENDER = 'N';
            tRANS.FLAG_SMSSENDER = 'N';
            tRANS.FLAG_EMAILSENDER = 'N';
            tRANS.SENDEREMAILADDRESS = "";
            tRANS.TRANSSTATUS = "PENDING";
            tRANS.TRANSRECEIVEDID = "";
            tRANS.TRANSRECEIVEDATE = nullDate;
            tRANS.CREATEDBY = int.Parse(Session["userInfoID"].ToString());
            tRANS.CREATEDON = DateTime.Now;
            tRANS.UPDATEDBY = int.Parse(Session["userInfoID"].ToString());
            tRANS.UPDATEDON = DateTime.Now;
            tRANS.AGENTID = aGENT.AGENTID;
            tRANS.REFCODE = lblReferenceCode.Text;



            if (Session["sessionTRANS"] == null)
            {
                List<TRANS> SSTRANS = new List<TRANS>();

                SSTRANS.Add(tRANS);
                Session.Remove("sessionTRANS");
                Session["sessionTRANS"] = SSTRANS;
            }


            else
            {
                ((List<TRANS>)Session["sessionTRANS"]).Add(tRANS);



            }

            //List<TRANS> SSTRANSFINAL = new List<TRANS>();
            //SSTRANSFINAL = (List<TRANS>)Session["sessionTRANS"];

            //gvTRANS.DataSource = SSTRANSFINAL;
            //gvTRANS.DataBind();

        }

        catch (Exception EX)
        {
            lblmessage.Text = EX.ToString();
        }
    }


    protected void btnProcessTransaction_Click(object sender, EventArgs e)
    {
        //try
        //{
            loadTransSession();

            if (Session["sessionTRANS"] != null)
            {
                int resutl1 = 0;
                //Session.Remove("ssReferenceCode");
                List<TRANS> SSTRANSFINAL = new List<TRANS>();
                SSTRANSFINAL = (List<TRANS>)Session["sessionTRANS"];
                TRANS tRANS1 = new TRANS();


                for (int i = 0; i < SSTRANSFINAL.Count; i++)
                {
                    if (SSTRANSFINAL[i].CUSTID > 0)
                    {
                        tRANS1.CUSTID = SSTRANSFINAL[i].CUSTID;
                    }
                    else
                    {
                        continue;
                    }
                    tRANS1.RECEIVERID = SSTRANSFINAL[i].RECEIVERID;
                    tRANS1.LOCATIONID = SSTRANSFINAL[i].LOCATIONID;
                    tRANS1.TRANSDT = SSTRANSFINAL[i].TRANSDT;
                    tRANS1.TRANSAMOUNT = SSTRANSFINAL[i].TRANSAMOUNT;
                    tRANS1.TRANSFEES = SSTRANSFINAL[i].TRANSFEES;
                    tRANS1.TRANSOTHERFEES = SSTRANSFINAL[i].TRANSOTHERFEES;
                    tRANS1.CAUSETRANSOTHERFEES = SSTRANSFINAL[i].CAUSETRANSOTHERFEES;
                    tRANS1.TRANSPROMOCODE = SSTRANSFINAL[i].TRANSPROMOCODE;
                    tRANS1.TRANSPROMO = SSTRANSFINAL[i].TRANSPROMO;
                    tRANS1.TRANSTOTALAMOUNT = SSTRANSFINAL[i].TRANSTOTALAMOUNT;
                    tRANS1.FLAG_SM_RECEIVER = SSTRANSFINAL[i].FLAG_SM_RECEIVER;
                    tRANS1.SM_RECEIVER = SSTRANSFINAL[i].SM_RECEIVER;
                    tRANS1.FLAG_CALL_RECEIVER = SSTRANSFINAL[i].FLAG_CALL_RECEIVER;
                    tRANS1.RECEIVERPHONENO = SSTRANSFINAL[i].RECEIVERPHONENO;
                    tRANS1.FLAG_DD = SSTRANSFINAL[i].FLAG_DD;
                    tRANS1.FLAG_TESTQUESTION = SSTRANSFINAL[i].FLAG_TESTQUESTION;
                    tRANS1.TESTQUESTION = SSTRANSFINAL[i].TESTQUESTION;
                    tRANS1.TESTANSWER = SSTRANSFINAL[i].TESTANSWER;
                    tRANS1.FLAG_CALLSENDER = SSTRANSFINAL[i].FLAG_CALLSENDER;
                    tRANS1.FLAG_SMSSENDER = SSTRANSFINAL[i].FLAG_SMSSENDER;
                    tRANS1.FLAG_EMAILSENDER = SSTRANSFINAL[i].FLAG_EMAILSENDER;
                    tRANS1.SENDEREMAILADDRESS = SSTRANSFINAL[i].SENDEREMAILADDRESS;
                    tRANS1.TRANSSTATUS = SSTRANSFINAL[i].TRANSSTATUS;
                    tRANS1.TRANSRECEIVEDID = SSTRANSFINAL[i].TRANSRECEIVEDID;
                    tRANS1.TRANSRECEIVEDATE = SSTRANSFINAL[i].TRANSRECEIVEDATE;
                    tRANS1.CREATEDBY = SSTRANSFINAL[i].CREATEDBY;
                    tRANS1.CREATEDON = SSTRANSFINAL[i].CREATEDON;
                    tRANS1.UPDATEDBY = SSTRANSFINAL[i].UPDATEDBY;
                    tRANS1.UPDATEDON = SSTRANSFINAL[i].UPDATEDON;
                    tRANS1.AGENTID = SSTRANSFINAL[i].AGENTID;
                    tRANS1.REFCODE = SSTRANSFINAL[i].REFCODE;

                    resutl1 = TRANSManager.InsertTRANS(tRANS1);

                    List<string> ReferenceCodeLst = new List<string>();

                    TRANS tRANSReferenceCode = new TRANS();
                    tRANSReferenceCode = TRANSManager.GetTRANSByID(resutl1);

                    if (Session["ssReferenceCode"] == null)
                    {

                        ReferenceCodeLst.Add(tRANSReferenceCode.REFCODE.ToString());

                        if (tRANSReferenceCode != null)
                        {
                            Session["ssReferenceCode"] = ReferenceCodeLst;
                        }
                    }


                    else
                    {
                        //TRANS tRANSReferenceCode = new TRANS();
                        //tRANSReferenceCode = TRANSManager.GetTRANSByID(resutl1);
                        ////((List<string>)Session["ssReferenceCode"]).Add(tRANSReferenceCode.REFCODE);

                        //if (tRANSReferenceCode != null)
                        //{
                        ((List<string>)Session["ssReferenceCode"]).Add(tRANSReferenceCode.REFCODE.ToString());
                        //}
                    }

                }
                if (resutl1 > 0)
                {
                    //Session.Remove("snlocationID");
                    //Session.Remove("snsenderID");
                    Session.Remove("snreceiverID");
                    Session.Remove("sessionTRANS");
                    Response.Redirect("Transmit.aspx");
                }
            }
            else
            {
                Response.Redirect("SearchSenderPage.aspx");
            }
        //}

        //catch (Exception Ex)
        //{
        //    lblmessage.Text = Ex.ToString();
        //}

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("SearchReceiverPage.aspx");
        Response.Redirect("Default.aspx");
    }

    //private void calculateTotal()
    //{

    //    txtServiceCharge.Text = String.Format("{0:0}", double.Parse(txtSendingAmount.Text) * 0.1);
    //    txtTotalCharge.Text = String.Format("{0:0}", double.Parse(txtSendingAmount.Text) + double.Parse(txtServiceCharge.Text) + double.Parse(txtOtherServiceCharge.Text) - double.Parse(txtDiscount.Text));
    //    lblAmountReceived.Text = String.Format("{0:0}", double.Parse(txtTotalCharge.Text) * double.Parse(lblRate.Text));
    //}

    //protected void txtSendingAmount_TextChanged(object sender, EventArgs e)
    //{
    //    //calculateTotal();

    //    txtTotalCharge.Text = txtSendingAmount.Text;
    //}
    //protected void txtServiceCharge_TextChanged(object sender, EventArgs e)
    //{
    //    calculateTotal();
    //}
    //protected void txtOtherServiceCharge_TextChanged(object sender, EventArgs e)
    //{
    //    calculateTotal();
    //}
    //protected void txtDiscount_TextChanged(object sender, EventArgs e)
    //{
    //    calculateTotal();
    //}
    protected void btnAnotherTransaction_Click(object sender, EventArgs e)
    {
            loadTransSession();
            Response.Redirect("SearchReceiverPage.aspx");

    }
    protected void btnNewTransaction_Click(object sender, EventArgs e)
    {
        Response.Redirect("paymentkoromstaronly.aspx");

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string sql = @"

SELECT ID,[SenderName]
      ,[ReceiverName]
      ,[SendingAmount]
      ,[ServiceCharge]
      ,[TotalPayment] as REFCode
      ,[Code] as  Location
      ,[Location] as PaymentDate
  FROM tmp.dbo.[KoromStar]
where [Location]<> ''
and [TotalPayment] <>'' and TransferDate is NULL
";

        DataSet ds = MSSQL.SQLExec(sql);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            txtName.Text = dr["SenderName"].ToString().Trim();
            txtReceiverName.Text = dr["ReceiverName"].ToString().Trim();
            txtDate.Text = dr["PaymentDate"].ToString().Trim();
            try
            {
                txtSendingAmount.Text = decimal.Parse(dr["SendingAmount"].ToString().Trim().Replace("$", "").Replace(",", "").Replace("X", "0").Replace("x", "0")).ToString("0.00");
            }
            catch (Exception ex)
            {
                txtSendingAmount.Text = "0";
            }

            try
            {
                txtServiceCharge.Text = decimal.Parse(dr["ServiceCharge"].ToString().Trim().Replace("$", "").Replace(",", "").Replace("X", "0").Replace("x", "0")).ToString("0.00");
            }
            catch (Exception ex)
            {
                txtServiceCharge.Text = "0";
            }
            
           // txtServiceCharge.Text = dr["ServiceCharge"].ToString().Trim().Replace("$", "").Replace(",", "").Replace("X", "0").Replace("x", "0");
            txtTotalCharge.Text = (decimal.Parse(txtSendingAmount.Text) + decimal.Parse(txtServiceCharge.Text)).ToString("0.00");
            lblReferenceCode.Text = dr["REFCode"].ToString().Trim();
            txtBranch.Text = dr["Location"].ToString().Trim();
            ProcessTransfer();
            MSSQL.SQLExec("update tmp.dbo.[KoromStar] set TransferDate='" + lblmessage.Text + "' where [ID]='" + dr["ID"].ToString().Trim() + "'");
        }
    }
    protected void ProcessTransfer()
    {
        string senderID = loadSenderID();
        string receiverID = loadReceiverID();
        string locationID = loadLoacationID(txtBranch.Text);

        if (
            senderID == ""
            || senderID == null
            || receiverID == ""
            || receiverID == null
            || locationID == "0"
            || lblReferenceCode.Text == ""
            //|| checkRefCodeDuplicate()
            )
        {
            lblmessage.Text = "Incorrect Data";
            return;
        }
        else
        {
            TRANS tRANS = new TRANS();



            tRANS.CUSTID = int.Parse(senderID);
            tRANS.RECEIVERID = int.Parse(receiverID);
            tRANS.LOCATIONID = int.Parse(locationID); ;
            tRANS.TRANSDT = DateTime.Parse(txtDate.Text);
            tRANS.TRANSAMOUNT = decimal.Parse(txtSendingAmount.Text);
            tRANS.TRANSFEES = decimal.Parse(txtServiceCharge.Text);
            tRANS.TRANSOTHERFEES = decimal.Parse(txtOtherServiceCharge.Text);
            tRANS.CAUSETRANSOTHERFEES = txtCauseOtherServiceCharges.Text;
            tRANS.TRANSPROMOCODE = txtDiscount.Text;
            tRANS.TRANSPROMO = 0;
            tRANS.TRANSTOTALAMOUNT = decimal.Parse(txtTotalCharge.Text);
            tRANS.FLAG_SM_RECEIVER = 'N';
            tRANS.SM_RECEIVER = "";
            tRANS.FLAG_CALL_RECEIVER = 'N';
            tRANS.RECEIVERPHONENO = "";
            tRANS.FLAG_DD = 'N';
            tRANS.FLAG_TESTQUESTION = 'N';
            tRANS.TESTQUESTION = txtTestQuestion.Text;
            tRANS.TESTANSWER = txtTestAnswer.Text;
            tRANS.FLAG_CALLSENDER = 'N';
            tRANS.FLAG_SMSSENDER = 'N';
            tRANS.FLAG_EMAILSENDER = 'N';
            tRANS.SENDEREMAILADDRESS = "";
            tRANS.TRANSSTATUS = "PENDING";
            tRANS.TRANSRECEIVEDID = "";
            tRANS.TRANSRECEIVEDATE = DateTime.Now;
            tRANS.CREATEDBY = 1;
            tRANS.CREATEDON = DateTime.Now;
            tRANS.UPDATEDBY = 1;
            tRANS.UPDATEDON = DateTime.Now;
            tRANS.AGENTID = 4;
            tRANS.REFCODE = lblReferenceCode.Text;

           lblmessage.Text=TRANSManager.InsertTRANS(tRANS).ToString();
        }
    }

    private string loadLoacationID(string location)
    {
        string sql = @"
        Declare @LOCATIONID int
        Set @LOCATIONID =
(
    select top 1 LOCATIONID from LOCATION where BRANCH='" + location.Trim().Replace("  ", " ").Trim().Replace("'", "''") + @"'
)

if @LOCATIONID is NULL
BEGIN
    INSERT INTO [LOCATION]
           ([COUNTRY]
           ,[CITY]
           ,[BRANCH]
           ,[BRANCH_CODE]
           ,[SEQUENCE]
           ,[AGENTID]
           ,[AGENTRATE])
     VALUES
           (''--<COUNTRY, varchar(50),>
           ,''--CITY, varchar(50),>
           ,'" + location.Trim().Replace("  ", " ").Trim().Replace("'", "''") + @"'--BRANCH, varchar,>
           ,''--BRANCH_CODE, varchar(15),>
           ,0--SEQUENCE, int,>
           ,4--AGENTID, int,>
           ,0--AGENTRATE, decimal(5,2),>
           );
SET @LOCATIONID =SCOPE_IDENTITY();
END

select @LOCATIONID;

        ";

        return MSSQL.SQLExec(sql).Tables[0].Rows[0][0].ToString();
    }

    private bool checkRefCodeDuplicate()
    {
        string sql = "select top 1 * from TRANS where REFCODE='" + lblReferenceCode.Text.Trim() + @"'";
        if (MSSQL.SQLExec(sql).Tables[0].Rows.Count > 0) return true;
        else 
            return false;
    }

    private string loadReceiverID()
    {
        string sql = @"
        Declare @ReceiverID int
        Set @ReceiverID =
(
    select top 1 RECEIVERID from RECEIVER where RECEIVERFNAME='" + txtReceiverName.Text.Trim().Replace("  ", " ").Trim().Replace("'", "''") + @"'
)

if @ReceiverID is NULL
BEGIN
    INSERT INTO [RECEIVER]
           ([USERNAME]
           ,[RECEIVERFNAME]
           ,[RECEIVERMNAME]
           ,[RECEIVERLNAME]
           ,[RECEIVERADDRESS1]
           ,[RECEIVERADDRESS2]
           ,[RECEIVERCITY]
           ,[RECEIVERSTATE]
           ,[RECEIVERZIP]
           ,[RECEIVERPHONE]
           ,[SCANURL]
           ,[CREATEDBY]
           ,[CREATEDON]
           ,[UPDATEDBY]
           ,[UPDATEDON])
     VALUES
           (''--<USERNAME, varchar(100),>
           ,'" + txtReceiverName.Text.Trim().Replace("  ", " ").Trim().Replace("'", "''") + @"'--<RECEIVERFNAME, varchar(256),>
           ,''--<RECEIVERMNAME, varchar(256),>
           ,''--<RECEIVERLNAME, varchar(256),>
           ,''--<RECEIVERADDRESS1, varchar(100),>
           ,''--<RECEIVERADDRESS2, varchar(100),>
           ,''--<RECEIVERCITY, varchar(50),>
           ,''--<RECEIVERSTATE, varchar(2),>
           ,''--<RECEIVERZIP, varchar(20),>
           ,''--<RECEIVERPHONE, varchar(20),>
           ,''--<SCANURL, varchar(256),>
           ,1--<CREATEDBY, int,>
           ,GETDATE()--<CREATEDON, datetime,>
           ,1--<UPDATEDBY, int,>
           ,GETDATE()--<UPDATEDON, datetime,>
           );
SET @ReceiverID =SCOPE_IDENTITY();
END

select @ReceiverID;

        ";

        return MSSQL.SQLExec(sql).Tables[0].Rows[0][0].ToString();
    }
    private string loadSenderID()
    {
        string sql = @"
        Declare @SenderID int
        Set @SenderID =
(
    select top 1 CUSTOMERID from CUSTOMER where CUSTFNAME='" + txtName.Text.Trim().Replace("  ", " ").Trim().Replace("'", "''") + @"'
)

if @SenderID is NULL
BEGIN
    INSERT INTO [CUSTOMER]
           ([EMTID]
           ,[USERNAME]
           ,[CUSTFNAME]
           ,[CUSTMNAME]
           ,[CUSTLNAME]
           ,[CUSTADDRESS1]
           ,[CUSTADDRESS2]
           ,[CUSTCITY]
           ,[CUSTSTATE]
           ,[CUSTZIP]
           ,[CUSTHPHONE]
           ,[CUSTCPHONE]
           ,[CUSTWPHONE]
           ,[CUSTSSN]
           ,[CUSTDRIVINGLICENSE]
           ,[CUSTIDTYPE]
           ,[CUSTIDNUMBER]
           ,[CUSTDOB]
           ,[CUSTISSUEDATE]
           ,[CUSTEXPIREDATE]
           ,[ISOFACVERIFIED]
           ,[CUSTREMARKS]
           ,[SCANURL]
           ,[CREATEDBY]
           ,[CREATEDON]
           ,[UPDATEDBY]
           ,[UPDATEDON])
     VALUES
           (''--<EMTID, varchar(10),>
           ,''--<USERNAME, varchar(100),>
           ,'" + txtName.Text.Trim().Replace("  ", " ").Replace("'", "''") + @"'--<CUSTFNAME, varchar(256),>
           ,''--<CUSTMNAME, varchar(256),>
           ,''--<CUSTLNAME, varchar(256),>
           ,''--<CUSTADDRESS1, varchar(100),>
           ,''--<CUSTADDRESS2, varchar(100),>
           ,''--<CUSTCITY, varchar(50),>
           ,''--<CUSTSTATE, varchar(2),>
           ,''--<CUSTZIP, varchar(15),>
           ,''--<CUSTHPHONE, varchar(15),>
           ,''--<CUSTCPHONE, varchar(15),>
           ,''--<CUSTWPHONE, varchar(15),>
           ,''--<CUSTSSN, varchar(15),>
           ,''--<CUSTDRIVINGLICENSE, varchar(30),>
           ,1--<CUSTIDTYPE, int,>
           ,''--<CUSTIDNUMBER, varchar(30),>
           ,GETDATE()--<CUSTDOB, datetime,>
           ,GETDATE()--<CUSTISSUEDATE, datetime,>
           ,GETDATE()--<CUSTEXPIREDATE, datetime,>
           ,''--<ISOFACVERIFIED, varchar(1),>
           ,''--<CUSTREMARKS, varchar(100),>
           ,''--<SCANURL, varchar(256),>
           ,1--<CREATEDBY, int,>
           ,GETDATE()--<CREATEDON, datetime,>
           ,1--<UPDATEDBY, int,>
           ,GETDATE()--<UPDATEDON, datetime,>
           );
SET @SenderID =SCOPE_IDENTITY();
END

select @SenderID;

        ";

        return MSSQL.SQLExec(sql).Tables[0].Rows[0][0].ToString();
    }
}