Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing

Public Class RMGridView
    Inherits DataGridView

    Public Sub New()
        Me.ApplyDefaultStyle()
        Me.EnableHeadersVisualStyles = False
    End Sub

    Public Sub ApplyDefaultStyle()
        ' Me.RowHeadersVisible = False
        'Me.AllowUserToAddRows = False
        'Me.AllowUserToDeleteRows = False
        Me.AllowUserToResizeRows = False
        '  Me.MultiSelect = False
        '  Me.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.DefaultCellStyle.Font = New Font("Khmer OS System", 8)
        Me.RowTemplate.DefaultCellStyle.Font = New Font("Khmer OS System", 8)
        Me.CellBorderStyle = DataGridViewCellBorderStyle.None
        Me.BorderStyle = Windows.Forms.BorderStyle.None
        Dim tmp As New DataGridViewCellStyle
        tmp.BackColor = Color.Azure
        Me.AlternatingRowsDefaultCellStyle = tmp
        Me.DefaultCellStyle.SelectionBackColor = Color.FromArgb(173, 205, 239)
        Me.DefaultCellStyle.SelectionForeColor = Color.Black
        Me.ColumnHeadersDefaultCellStyle.Font = New Font("Khmer OS System", 8)
        Me.RowTemplate.Height = 27
        Me.BackgroundColor = Color.White
        Me.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub UNTGridView_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles Me.RowPostPaint
        If Me.RowHeadersVisible = False Then Return
        Using b As SolidBrush = New SolidBrush(Me.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 12, e.RowBounds.Location.Y + 4)
        End Using
    End Sub
End Class
