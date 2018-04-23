Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports DevExpress.Data
Imports System.Reflection
Imports System.Collections

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
			Set
				Me.altName = Value
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
			Set
				phoneNumber_Renamed = Value
			End Set
		End Property
		Private contanctName_Renamed As String
		<Reportable(AlternateName := "Contact Name")> _
		Public Property ContanctName() As String
			Get
				Return contanctName_Renamed
			End Get
			Set
				contanctName_Renamed = Value
			End Set
		End Property
		Public Sub New(ByVal number As String, ByVal name As String)
			contanctName_Renamed = name
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
			Set
				secondaryCode_Renamed = Value
			End Set
		End Property

		<Reportable()> _
		Public Property City() As String
			Get
				Return city_Renamed
			End Get
			Set
				city_Renamed = Value
			End Set
		End Property
		<Reportable(AlternateName := "Business address")> _
		Public Property Business_address() As String
			Get
				Return business_address_Renamed
			End Get
			Set
				business_address_Renamed = Value
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
			Set
				name_Renamed = Value
			End Set
		End Property

		<Reportable(AlternateName := "e-mail Address")> _
		Public Property EmailAddress() As String
			Get
				Return emailAddress_Renamed
			End Get
			Set
				emailAddress_Renamed = Value
			End Set
		End Property

		<Reportable(AlternateName := "Registration Number")> _
		Public Property RegistrationID() As String
			Get
				Return registrationID_Renamed
			End Get
			Set
				registrationID_Renamed = Value
			End Set
		End Property

		<Reportable(AlternateName := "Address")> _
		Public Property RptOnlyAddress() As List(Of Address)
			Get
				Return addressList
			End Get
			Set
				addressList = Value
			End Set
		End Property

	End Class
	#End Region
	#Region "IDataDictionary Implementation - Companies collection"
	Public Class Companies
		Inherits List(Of Company)
		Implements IDataDictionary
		#Region "IDataDictionary Members"

		Public Function GetDataSourceDisplayName() As String Implements IDataDictionary.GetDataSourceDisplayName
			Return "Companies List"
		End Function

		Public Function GetObjectDisplayName(ByVal dataMember As String) As String Implements IDataDictionary.GetObjectDisplayName
			If dataMember <> "" AndAlso Me.Count > 0 Then
				Dim names As String() = dataMember.Split("."c)
				Dim altNames As String() = dataMember.Split("."c)
				Dim mObj As Object = Me(0)
				Dim mType As Type = mObj.GetType()

				Dim i As Integer = 0
				Do While i < names.Length
					Dim pi As PropertyInfo = mType.GetProperty(names(i))
					If Not pi Is Nothing Then
						Dim reportable As Reportable() = CType(pi.GetCustomAttributes(GetType(Reportable), False), Reportable())
						If reportable.Length > 0 Then
							If Not reportable(0).AlternateName Is Nothing Then
								altNames(i) = reportable(0).AlternateName
							End If
						Else
							Return ""
						End If

					Else
						Return dataMember
					End If
					If pi.PropertyType.IsGenericType AndAlso (Not pi.PropertyType.IsValueType) Then

						mObj = pi.GetValue(mObj, Nothing)
						mObj = (CType(mObj, IList))(0)
						mType = mObj.GetType()
					End If

					i += 1
				Loop
				Dim s As String = String.Join(".", altNames)
				Return s
			Else
				Return dataMember
			End If
		End Function


		#End Region
	End Class
	#End Region

End Namespace
