<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128601396/11.2.7%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E667)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [BO.cs](./CS/BO.cs) (VB: [BO.vb](./VB/BO.vb))
* [Form1.cs](./CS/Form1.cs) (VB: [Form1.vb](./VB/Form1.vb))
<!-- default file list end -->
# How to implement IDataDictionary/IDisplayNameProvider for a Business Object


<p><strong>Description</strong>:<br />
I'm using the <strong>System.Collections.Generic.List</strong> collection as a report's datasource.<br />
I set it like this:</p>

```cs
List<MyObject> list = new List<MyObject>();<newline/>
report.Datasource = list;
```

<p> </p><p>How do I change/specify the names in the field list with my desired display name?</p><p><strong>Solution</strong>:<br />
You should implement the <strong>IDisplayNameProvider</strong><strong> </strong>(<strong>IDataDictionary</strong><strong> </strong>in versions prior to v2011 vol 2) interface over your collection object. The <strong>IDisplayNameProvider </strong>interface has two methods that will help to change the display name of the required field. In the attached sample I show how you can change the display name, by using the custom attributes implemented over the Business Objects properties.</p><p><strong>See also: </strong><a href="https://www.devexpress.com/Support/Center/p/E459">How to provide custom names for the Field List data items</a>.</p>

<br/>


