USE [AbiMatuEnterpriseDB]
GO
/****** Object:  StoredProcedure [dbo].[AbiMatuEnterprise_GetAllCUSTOMERForSearch]    Script Date: 05/15/2014 11:33:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AbiMatuEnterprise_GetAllCUSTOMERForSearch]
(
@CUSTOMERID int,
@CUSTHPHONE varchar(15),
@CUSTSSN varchar(15),
@CUSTFNAME varchar(100),
@CUSTMNAME varchar(100),
@CUSTLNAME varchar(100)
)
AS

BEGIN
         DECLARE @SelectStatement NVARCHAR(4000)
         
         Declare @WhereClause Nvarchar(1000)

--set @ParentID=0;
 
							     
SET @WhereClause = ' ' 
--and ParentID = ' + CONVERT(VARCHAR(19),@ParentID) + '
--print(@WhereClause);
CREATE TABLE #chkSearchOption 
(
searchValue varchar(256) ,
searchQuery ntext

)

INSERT #chkSearchOption (searchValue,searchQuery) VALUES 
(
@CUSTOMERID , ' CUSTOMERID = '+ CONVERT(VARCHAR(256),@CUSTOMERID) +' '
);

INSERT #chkSearchOption (searchValue,searchQuery) VALUES 
(

@CUSTHPHONE , ' CUSTHPHONE like ''%'+ @CUSTHPHONE +'%'' '

);
INSERT #chkSearchOption (searchValue,searchQuery) VALUES 
(

@CUSTSSN , ' CUSTSSN like ''%'+ @CUSTSSN +'%'' '

);
INSERT #chkSearchOption (searchValue,searchQuery) VALUES 
(

@CUSTFNAME , ' CUSTFNAME like '''+ @CUSTFNAME +'%'' '
);

INSERT #chkSearchOption (searchValue,searchQuery) VALUES 
(

@CUSTMNAME , ' CUSTMNAME like ''%'+ @CUSTMNAME +'%'' '
);



INSERT #chkSearchOption (searchValue,searchQuery) VALUES 
(

@CUSTLNAME , ' CUSTLNAME like ''%'+ @CUSTLNAME +'%'' '
);

--SELECT searchValue , searchQuery from  #chkSearchOption;

         DECLARE @searchValue2 varchar(256)
         DECLARE @searchQuery2 varchar(4000)
         declare @counter int
--set @WhereClause=
set @counter=1;
DECLARE mycursor CURSOR READ_ONLY
FOR SELECT searchValue , searchQuery
FROM #chkSearchOption 
where searchValue !='0'


OPEN mycursor

FETCH NEXT FROM mycursor
INTO @searchValue2 , @searchQuery2

WHILE @@FETCH_STATUS = 0
BEGIN

if(@counter = 1)
begin
--print('Where ' + @searchQuery2);
set @WhereClause= 'Where ' + @WhereClause + ' ' + @searchQuery2 ;
end

else
begin
--print('and ' + @searchQuery2);

set @WhereClause= @WhereClause + 'and ' + @searchQuery2 ;
end


set @counter = @counter + 1;


    FETCH NEXT FROM mycursor
    INTO @searchValue2 ,  @searchQuery2


END

CLOSE mycursor
DEALLOCATE mycursor

--print(@WhereClause);

         SET @SelectStatement = ' SELECT [CUSTOMERID]
      ,[EMTID]
      ,[USERNAME]
      ,[CUSTFNAME]
      ,[CUSTMNAME]
      ,[CUSTLNAME]
      ,[CUSTADDRESS1]
      ,cast([dbo].[getTotalTransactionAmount] (CUSTOMERID) as varchar(100))as [CUSTADDRESS2]
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
      ,[UPDATEDON]
  FROM [CUSTOMER]
   '+@WhereClause+'  '

--print(@SelectStatement);

	--set 	@FullStatement=@SelectStatement + @WhereClause
      EXECUTE (@SelectStatement)    
         
 -- print(@FullStatement);       

 END






RETURN

RETURN
