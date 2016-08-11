# XmlSerializerExtensions

Utils extension methods for XmlSerializer

===================

[![NuGet Downloads](http://img.shields.io/nuget/dt/SerializerExtensions.svg?style=flat)](https://www.nuget.org/packages/SerializerExtensions/)

## Major Features

The following major features are currently implemented:
+ Serialize object (with namespace list)
+ Serialize to file
+ Serialize to byte[]
+ Deserialize object
+ Deserialize object from byte[]

### Serialize object:

These extensions allow easy serialize object

Example:
        var model = new Model { Id = 1, Description = "Example1" };
        var xmlString = model.Serialize();

### Deserialize object:

These extensions allow easy deserialize xml string to specific object

Example:
        var xmlString = @"<?xml version='1.0'?>
							<Model xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>
							  <Id>113</Id>
							  <Description>Description6788108e-8979-4f8f-ac82-4842dad37d3d</Description>
							</Model>";

		var model = xmlString.Deserialize<Model>();


## How to Contribute

Feel free to fork the project, work on your fork and send me the pull requests.
You can also create issues with the features or changes that you think important.

Also, this repository is built with autocrlf = true.


## Version History

v1.0.0-alpha01
- Initial Version