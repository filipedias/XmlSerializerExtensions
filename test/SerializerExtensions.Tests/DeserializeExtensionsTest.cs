using NUnit.Framework;
using Ploeh.AutoFixture;
using SerializerExtensions.Tests.Entities;
using SerializerExtensions.Extensions;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace SerializerExtensions.Tests
{
    [TestFixture]
    public class DeserializeExtensionsTest : BaseUnitTest
    {
        [Test]
        public void DeserializerRandomModelWithExtensionsMethodNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();
            var xmlString = model.Serialize();

            //Do test
            var modelConfirm = xmlString.Deserialize<Model>();

            //Check
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm);

            Assert.True(xmlString == xmlStringConfirm);
        }


        [Test]
        public void DeserializerRandomModelWithExtensions2MethodNoErrors()
        {
            //Prepare
            var xmlString = "<?xml version=\"1.0\"?>\r\n<Model xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Id>113</Id>\r\n  <Description>Description6788108e-8979-4f8f-ac82-4842dad37d3d</Description>\r\n  <Details />\r\n</Model>";

            //Do test
            var modelConfirm = xmlString.Deserialize<Model>();

            //Check
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm);

            Assert.True(xmlString.Trim() == xmlStringConfirm.Trim());
        }

        [Test]
        public void DeserializerRandomModelWithExtensionsWithErrors()
        {
            try
            {
                //Prepare
                var model = _fixture.Create<Model>();
                var xmlString = model.Serialize();

                //Test
                var modelConfirm = xmlString.Deserialize<ModelDetail>();
            }
            catch (System.Exception ex)
            {
                Assert.True(ex.Message.Contains("There is an error in XML document"));
            }
        }
    }
}
