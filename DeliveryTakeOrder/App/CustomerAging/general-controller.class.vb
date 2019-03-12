Public Class GeneralController
    Dim db As RMDB
    Public Sub New()
        db = New RMDB(AppSetting.ConnectionString)
    End Sub

    Public Function GetInvoiceDetail(pDivision As String, pInvoiceNumber As String) As List(Of InvoiceDetail)
        Dim sqlQuery As String = <SQL><![CDATA[
DECLARE @Query NVARCHAR(MAX) = N'',
        @SelectClause NVARCHAR(MAX) = N'
        SELECT D.InvNumber,
           D.ShipDate,
           D.CusNum,
           D.CusCom,
           D.DelTo,
           D.DeltoId,
           D.ProNumY,
           D.ProName,
           D.ProPacksize,
           D.SupNum,
           D.SupName,
           D.OrderAmount,
           D.PcsOrder,
           D.Amount - D.DiscountAmount + D.VatAmount AS GrandAmount';

SELECT @Query += @SelectClause + N', N''' + D.TableName + N''' AS TableName, N''' + D.Division + N''' AS Division '
                 + N' FROM Stock.dbo.' + D.TableName + ' AS D UNION ALL'
FROM dbo.Sys_AllDetailTables AS D
WHERE D.TypeValue = 1
      AND D.Division = N'{0}';

SET @Query = LEFT(@Query, LEN(@Query) - LEN(' UNION ALL '));

SELECT @Query
    = N'
;WITH vDelivery AS (' + @Query
      + N')
SELECT D.InvNumber,
       D.ShipDate,
       D.CusNum,
       C.CusName,
       D.CusCom,
       D.DeltoId,
       T.DelTo,
       D.ProNumY,
       D.ProName,
       D.ProPacksize,
       D.SupNum,
       D.SupName,
       D.OrderAmount,
       D.PcsOrder,
       D.GrandAmount,
       D.TableName,
       D.Division
FROM vDelivery AS D
    LEFT OUTER JOIN Stock.dbo.TPRCustomer AS C
        ON C.CusNum = D.CusNum
    LEFT OUTER JOIN Stock.dbo.TPRDelto AS T
        ON D.DeltoId = T.Id
WHERE D.InvNumber = N''{1}''';

EXEC (@Query);
]]></SQL>

        sqlQuery = String.Format(sqlQuery, pDivision, pInvoiceNumber)
        Dim ls As List(Of InvoiceDetail) = db.GetDataTableToObject(Of InvoiceDetail)(sqlQuery)
        Return ls
    End Function
End Class
