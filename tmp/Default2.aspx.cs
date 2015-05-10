using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class tmp_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sql = @"
SELECT [Amount]
      ,[Commision]
      ,[Code]
      ,[PaymentDate]
      ,[SenderID]
      ,[ReceiverID]
      ,[LocationID]
,TID
  FROM [tmp].[dbo].[KoromStar]
";

            DataSet ds = MSSQL.SQLExec(sql);
            string text = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                decimal amount = 0;
                decimal fee = 0;
                try
                {
                    amount = decimal.Parse(dr["Amount"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                catch (Exception ex)
                {
                    amount = 0;
                }

                try
                {
                    fee = decimal.Parse(dr["Commision"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                catch (Exception ex)
                {
                    fee = 0;
                }

                text = @"
INSERT INTO [KoromstarDB].[dbo].[TRANS]
           ([CUSTID]
           ,[RECEIVERID]
           ,[LOCATIONID]
           ,[TRANSDT]
           ,[TRANSAMOUNT]
           ,[TRANSFEES]
           ,[TRANSOTHERFEES]
           ,[CAUSETRANSOTHERFEES]
           ,[TRANSPROMOCODE]
           ,[TRANSPROMO]
           ,[TRANSTOTALAMOUNT]
           ,[FLAG_SM_RECEIVER]
           ,[SM_RECEIVER]
           ,[FLAG_CALL_RECEIVER]
           ,[RECEIVERPHONENO]
           ,[FLAG_DD]
           ,[FLAG_TESTQUESTION]
           ,[TESTQUESTION]
           ,[TESTANSWER]
           ,[FLAG_CALLSENDER]
           ,[FLAG_SMSSENDER]
           ,[FLAG_EMAILSENDER]
           ,[SENDEREMAILADDRESS]
           ,[TRANSSTATUS]
           ,[TRANSRECEIVEDID]
           ,[TRANSRECEIVEDATE]
           ,[CREATEDBY]
           ,[CREATEDON]
           ,[UPDATEDBY]
           ,[UPDATEDON]
           ,[AGENTID]
           ,[REFCODE])
     VALUES
           (" + dr["SenderID"].ToString() + @"--<CUSTID, int,>
           ," + dr["ReceiverID"].ToString() + @"--<RECEIVERID, int,>
           ," + dr["LocationID"].ToString() + @"--<LOCATIONID, int,>
           ,'" + dr["PaymentDate"].ToString().Split('/')[2] + "-" + dr["PaymentDate"].ToString().Split('/')[0] + "-" + dr["PaymentDate"].ToString().Split('/')[1] + @"'--<TRANSDT, datetime,>
           ," + amount + @"--<TRANSAMOUNT, decimal(10,2),>
           ," + fee + @"--<TRANSFEES, decimal(10,2),>
           ,0
           ,''
           ,''--<TRANSPROMOCODE, varchar(20),>
           ,0--<TRANSPROMO, int,>
           ,"+(amount+fee)+@"--<TRANSTOTALAMOUNT, decimal(10,2),>
           ,'N'--<FLAG_SM_RECEIVER, char(1),>
           ,''--<SM_RECEIVER, varchar(50),>
           ,'N'--<FLAG_CALL_RECEIVER, char(1),>
           ,''--<RECEIVERPHONENO, varchar(20),>
           ,'N'--<FLAG_DD, char(1),>
           ,'N'--<FLAG_TESTQUESTION, char(1),>
           ,''--<TESTQUESTION, varchar(100),>
           ,''--<TESTANSWER, varchar(100),>
           ,'N'--<FLAG_CALLSENDER, char(1),>
           ,'N'--<FLAG_SMSSENDER, char(1),>
           ,'N'--<FLAG_EMAILSENDER, char(1),>
           ,''--<SENDEREMAILADDRESS, varchar(50),>
           ,'PENDING'--<TRANSSTATUS, varchar(20),>
           ,''--<TRANSRECEIVEDID, varchar(20),>
           ,GETDATE()--<TRANSRECEIVEDATE, datetime,>
           ,1--<CREATEDBY, int,>
           ,GETDATE()--<CREATEDON, datetime,>
           ,1--<UPDATEDBY, int,>
           ,GETDATE()--<UPDATEDON, datetime,>
           ,4--<AGENTID, int,>
           ,'" +dr["Code"].ToString().Trim()+ @"'--<REFCODE, varchar(10),>
           );

Declare @ID int
set @ID=(Select SCOPE_IDENTITY());

update [tmp].[dbo].[KoromStar] set TRNID=@ID
where TID="+dr["TID"].ToString()+@"
";

               MSSQL.SQLExec(text);
            }

           // TextBox1.Text = text;
            
        }
    }
}