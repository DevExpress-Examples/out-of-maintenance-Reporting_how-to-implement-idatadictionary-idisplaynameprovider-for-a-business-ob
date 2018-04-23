Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraReports.UI

Namespace WindowsApplication1
	Public Partial Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles button1.Click
			Dim c As Companies = New Companies()
			Dim c1 As Company = New Company("Microfost", "info@microfost.com", "1234-1234")
			Dim a1 As Address = New Address("Seattle", "507 - 20th Ave.", "12345")
			c1.AddAddress(a1)
			a1.AddPhone(New Phone("555-123-12-12", "Billy"))
			a1.AddPhone(New Phone("555-124-14-14", "Johny"))
			Dim a2 As Address = New Address("Tacoma", "908 W. Capital Way", "54321")
			a2.AddPhone(New Phone("555-777-12-12", "Paul"))
			a2.AddPhone(New Phone("555-777-14-14", "Mary"))
			c1.AddAddress(a2)
			c.Add(c1)

			Dim c2 As Company = New Company("BigApple", "info@bigapple.com", "4321-4321")
			Dim a3 As Address = New Address("Kirkland", "722 Moss Bay Blvd.", "67890")
			a3.AddPhone(New Phone("555-888-12-12", "Steve"))
			c2.AddAddress(a3)
			Dim a4 As Address = New Address("Redmond", "4110 Old Redmond Rd.", "09876")
			a4.AddPhone(New Phone("555-333-12-12", "Jack"))
			c2.AddAddress(a4)
			c.Add(c2)

			Dim r As XtraReport = New XtraReport()
			r.Bands.Add(New DetailBand())
			r.DataSource = c
			r.ShowDesignerDialog()
		End Sub
	End Class
End Namespace