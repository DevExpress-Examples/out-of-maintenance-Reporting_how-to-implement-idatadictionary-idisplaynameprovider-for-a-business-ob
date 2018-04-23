Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections
Imports System.Reflection
Imports System.Text
Imports DevExpress.Data
' ...

Namespace WindowsApplication1

	#Region "CustomAttribute"
	<AttributeUsage(AttributeTargets.All, AllowMultiple := True)> _
	Public Class Reportable
		Inherits Attribute
		Private altName As String
		Public Property AlternateName() As String
			Get
				Return Me.altName
			End Get
			Set(ByVal value As String)
				Me.altName = value
			End Set
		End Property
	End Class

	#End Region
	#Region "BO - Phone class"
	Public Class Phone
		Private phoneNumber_Renamed As String
		<Reportable(AlternateName := "Phone number")> _
		Public Property PhoneNumber() As String
			Get
				Return phoneNumber_Renamed
			End Get
			Set(ByVal value As String)
				phoneNumber_Renamed = value
			End Set
		End Property
		Private contactName_Renamed As String
		<Reportable(AlternateName := "Contact Name")> _
		Public Property ContactName() As String
			Get
				Return contactName_Renamed
			End Get
			Set(ByVal value As String)
				contactName_Renamed = value
			End Set
		End Property
		Public Sub New(ByVal number As String, ByVal name As String)
			contactName_Renamed = name
			phoneNumber_Renamed = number
		End Sub
	End Class
	#End Region
	#Region "BO - Address"
	Public Class Address
		Private city_Renamed As String
		Private business_address_Renamed As String
		Private secondaryCode_Renamed As String
		Public Sub New()
			phones_Renamed = New List(Of Phone)()
		End Sub
		Public Sub New(ByVal City As String, ByVal Address As String, ByVal SecondaryCode As String)
			Me.city_Renamed = City
			business_address_Renamed = Address
			Me.secondaryCode_Renamed = SecondaryCode
			phones_Renamed = New List(Of Phone)()
		End Sub
		Public Property SecondaryCode() As String
			Get
				Return secondaryCode_Renamed
			End Get
			Set(ByVal value As String)
				secondaryCode_Renamed = value
			End Set
		End Property

		<Reportable()> _
		Public Property City() As String
			Get
				Return city_Renamed
			End Get
			Set(ByVal value As String)
				city_Renamed = value
			End Set
		End Property
		<Reportable(AlternateName := "Business address")> _
		Public Property Business_address() As String
			Get
				Return business_address_Renamed
			End Get
			Set(ByVal value As String)
				business_address_Renamed = value
			End Set
		End Property

		Private phones_Renamed As List(Of Phone)
		<Reportable(AlternateName := "Public phones")> _
		Public ReadOnly Property Phones() As List(Of Phone)
			Get
				Return phones_Renamed
			End Get
		End Property
		Public Sub AddPhone(ByVal phone As Phone)
			phones_Renamed.Add(phone)
		End Sub
	End Class
	#End Region
	#Region "BO - Company"
	<Reportable()> _
	Public Class Company
		Public Sub New()
			addressList = New List(Of Address)()
		End Sub
		Public Sub New(ByVal CompanyName As String, ByVal PublicEMail As String, ByVal RegistrationID As String)
			name_Renamed = CompanyName
			emailAddress_Renamed = PublicEMail
			Me.registrationID_Renamed = RegistrationID
			addressList = New List(Of Address)()
		End Sub
		Public Sub AddAddress(ByVal address As Address)
			addressList.Add(address)
		End Sub

		Private name_Renamed As String
		Private emailAddress_Renamed As String
		Private registrationID_Renamed As String
		Private addressList As List(Of Address)

		<Reportable(AlternateName := "Company Name")> _
		Public Property Name() As String
			Get
				Return name_Renamed
			End Get
			Set(ByVal value As String)
				name_Renamed = value
			End Set
		End Property

		<Reportable(AlternateName := "e-mail Address")> _
		Public Property EmailAddress() As String
			Get
				Return emailAddress_Renamed
			End Get
			Set(ByVal value As String)
				emailAddress_Renamed = value
			End Set
		End Property

		<Reportable(AlternateName := "Registration Number")> _
		Public Property RegistrationID() As String
			Get
				Return registrationID_Renamed
			End Get
			Set(ByVal value As String)
				registrationID_Renamed = value
			End Set
		End Property

		<Reportable(AlternateName := "Address")> _
		Public Property RptOnlyAddress() As List(Of Address)
			Get
				Return addressList
			End Get
			Set(ByVal value As List(Of Address))
				addressList = value
			End Set
		End Property

	End Class
	#End Region
	#Region "IDisplayNameProvider Implementation - Companies collection"
	Public Class Companies
		Inherits List(Of Company)
		Implements IDisplayNameProvider
		#Region "IDisplayNameProvider Members"

		Public Function GetDataSourceDisplayName() As String Implements IDisplayNameProvider.GetDataSourceDisplayName
			Return "Companies List"
		End Function

		Public Function GetFieldDisplayName(ByVal fieldAccessors() As String) As String Implements IDisplayNameProvider.GetFieldDisplayName
			If Count = 0 Then
				Return String.Empty
			End If

			Dim mObj As Object = Me(0)
			Dim mType As Type = mObj.GetType()

			For i As Integer = 0 To fieldAccessors.Length - 1
				Dim pi As PropertyInfo = mType.GetProperty(fieldAccessors(i))
				If pi Is Nothing Then
					Return String.Empty
				End If

				Dim reportable() As Reportable = CType(pi.GetCustomAttributes(GetType(Reportable), False), Reportable())
				If reportable.Length = 0 Then
					Return String.Empty
				End If

				If i = fieldAccessors.Length - 1 Then
					If String.IsNullOrEmpty(reportable(0).AlternateName) Then
						Return fieldAccessors(fieldAccessors.Length - 1)
					Else
						Return reportable(0).AlternateName
					End If
				End If

				If pi.PropertyType.IsGenericType AndAlso (Not pi.PropertyType.IsValueType) Then
					mObj = pi.GetValue(mObj, Nothing)
					Dim list As IList = CType(mObj, IList)
					If list.Count = 0 Then
						Exit For
					End If
					mObj = list(0)
					mType = mObj.GetType()
				End If
			Next i

			Return String.Empty
		End Function

		#End Region
	End Class
	#End Region

End Namespace
