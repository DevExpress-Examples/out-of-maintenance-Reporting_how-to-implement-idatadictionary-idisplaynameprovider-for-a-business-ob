using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Data;
using System.Reflection;
using System.Collections;

namespace WindowsApplication1 {

    #region CustomAttribute
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]

    public class Reportable : Attribute {
        private string altName;
        public string AlternateName {
            get { return this.altName; }
            set { this.altName = value; }
        }
    }
    
    #endregion
    #region BO - Phone class
    public class Phone {
        string phoneNumber;
        [Reportable(AlternateName = "Phone number")]
        public string PhoneNumber {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        string contanctName;
        [Reportable(AlternateName = "Contact Name")]
        public string ContanctName {
            get { return contanctName; }
            set { contanctName = value; }
        }
        public Phone(string number, string name) {
            contanctName = name;
            phoneNumber = number;
        }
    } 
    #endregion
    #region BO - Address
    public class Address {
        string city;
        string business_address;
        string secondaryCode;
        public Address() { phones = new List<Phone>(); }
        public Address(string City, string Address, string SecondaryCode) {
            city = City;
            business_address = Address;
            secondaryCode = SecondaryCode;
            phones = new List<Phone>();
        }
        public string SecondaryCode {
            get { return secondaryCode; }
            set { secondaryCode = value; }
        }

        [Reportable()]
        public string City {
            get { return city; }
            set { city = value; }
        }
        [Reportable(AlternateName = "Business address")]
        public string Business_address {
            get { return business_address; }
            set { business_address = value; }
        }

        List<Phone> phones;
        [Reportable(AlternateName = "Public phones")]
        public List<Phone> Phones {
            get { return phones; }
        }
        public void AddPhone(Phone phone) {
            phones.Add(phone);
        }
    } 
    #endregion
    #region BO - Company
    [Reportable()]
    public class Company {
        public Company() { addressList = new List<Address>(); }
        public Company(string CompanyName, string PublicEMail, string RegistrationID) {
            name = CompanyName;
            emailAddress = PublicEMail;
            registrationID = RegistrationID;
            addressList = new List<Address>();
        }
        public void AddAddress(Address address) {
            addressList.Add(address);
        }

        string name;
        string emailAddress;
        string registrationID;
        List<Address> addressList;

        [Reportable(AlternateName = "Company Name")]
        public string Name {
            get { return name; }
            set { name = value; }
        }

        [Reportable(AlternateName = "e-mail Address")]
        public string EmailAddress {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        [Reportable(AlternateName = "Registration Number")]
        public string RegistrationID {
            get { return registrationID; }
            set { registrationID = value; }
        }

        [Reportable(AlternateName = "Address")]
        public List<Address> RptOnlyAddress {
            get {
                return addressList;
            }
            set {
                addressList = value;
            }
        }

    } 
    #endregion
    #region IDataDictionary Implementation - Companies collection
    public class Companies : List<Company>, IDataDictionary {
        #region IDataDictionary Members

        public string GetDataSourceDisplayName() {
            return "Companies List";
        }

        public string GetObjectDisplayName(string dataMember) {
            if(dataMember != "" && this.Count > 0) {
                string[] names = dataMember.Split('.');
                string[] altNames = dataMember.Split('.');
                object mObj = this[0];
                Type mType = mObj.GetType();

                for(int i = 0; i < names.Length; i++) {
                    PropertyInfo pi = mType.GetProperty(names[i]);
                    if(pi != null) {
                        Reportable[] reportable = (Reportable[])pi.GetCustomAttributes(typeof(Reportable), false);
                        if(reportable.Length > 0) {
                            if(reportable[0].AlternateName != null)
                                altNames[i] = reportable[0].AlternateName;
                        }
                        else
                            return "";

                    }
                    else
                        return dataMember;
                    if (pi.PropertyType.IsGenericType && !pi.PropertyType.IsValueType) {
                        mObj = pi.GetValue(mObj, null);
                        mObj = ((IList)mObj)[0];
                        mType = mObj.GetType();
                    }

                }
                string s = String.Join(".", altNames);
                return s;
            }
            else
                return dataMember;
        }


        #endregion
    } 
    #endregion

}
