Sub AnalyzeStockData()
    Dim ws As Worksheet
    Dim ticker As String
    Dim openPrice As Double
    Dim closePrice As Double
    Dim volume As Double
    Dim summaryRow As Integer
    Dim lastRow As Long
    Dim i As Long
    
    ' Loop through each worksheet
    For Each ws In ThisWorkbook.Worksheets
        ' Initialize the summary row
        summaryRow = 2
        lastRow = ws.Cells(Rows.Count, 1).End(xlUp).Row

        ' Add headers to the summary table
        ws.Cells(1, 9).Value = "Ticker"
        ws.Cells(1, 10).Value = "Quarterly Change"
        ws.Cells(1, 11).Value = "Percentage Change"
        ws.Cells(1, 12).Value = "Total Volume"

        ' Initialize variables for tracking greatest changes and volume
        Dim greatestIncrease As Double
        Dim greatestDecrease As Double
        Dim greatestVolume As Double
        Dim greatestIncreaseTicker As String
        Dim greatestDecreaseTicker As String
        Dim greatestVolumeTicker As String
        
        greatestIncrease = 0
        greatestDecrease = 0
        greatestVolume = 0

        ' Loop through each row of stock data
        For i = 2 To lastRow
            ' Check if we are at the beginning of a new ticker
            If ws.Cells(i, 1).Value <> ws.Cells(i - 1, 1).Value Then
                ' Record the ticker symbol
                ticker = ws.Cells(i, 1).Value
                ' Record the opening price
                openPrice = ws.Cells(i, 3).Value
                ' Initialize volume
                volume = ws.Cells(i, 7).Value
            Else
                ' Accumulate the volume
                volume = volume + ws.Cells(i, 7).Value
            End If
            
            ' Check if we are at the end of a ticker
            If ws.Cells(i, 1).Value <> ws.Cells(i + 1, 1).Value Or i = lastRow Then
                ' Record the closing price
                closePrice = ws.Cells(i, 6).Value
                
                ' Calculate quarterly change
                Dim quarterlyChange As Double
                quarterlyChange = closePrice - openPrice
                
                ' Calculate percentage change
                Dim percentageChange As Double
                If openPrice <> 0 Then
                    percentageChange = (quarterlyChange / openPrice) * 100
                Else
                    percentageChange = 0
                End If
                
                ' Output the summary data
                ws.Cells(summaryRow, 9).Value = ticker
                ws.Cells(summaryRow, 10).Value = quarterlyChange
               ws.Cells(summaryRow, 11).Value = percentageChange
                ws.Cells(summaryRow, 12).Value = volume
                summaryRow = summaryRow + 1
                
                ' Track the greatest changes and volume
                If percentageChange > greatestIncrease Then
                    greatestIncrease = percentageChange
                    greatestIncreaseTicker = ticker
                End If
                
                If percentageChange < greatestDecrease Then
                    greatestDecrease = percentageChange
                    greatestDecreaseTicker = ticker
                End If
                
                If volume > greatestVolume Then
                    greatestVolume = volume
                    greatestVolumeTicker = ticker
                End If
            End If
        Next i
        
        ' Output the greatest values
        ws.Cells(2, 15).Value = "Greatest % Increase"
        ws.Cells(3, 15).Value = "Greatest % Decrease"
        ws.Cells(4, 15).Value = "Greatest Total Volume"
        ws.Cells(1, 16).Value = "Ticker"
        ws.Cells(2, 16).Value = greatestIncreaseTicker
        ws.Cells(3, 16).Value = greatestDecreaseTicker
        ws.Cells(4, 16).Value = greatestVolumeTicker
        ws.Cells(1, 17).Value = "Value"
        ws.Cells(2, 17).Value = greatestIncrease
        ws.Cells(3, 17).Value = greatestDecrease
        ws.Cells(4, 17).Value = greatestVolume
    Next ws
End Sub