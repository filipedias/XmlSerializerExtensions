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
    public class SerializeExtensionsTest : BaseUnitTest
    {
        [Test]
        public void SerializerRandomModelWithExtensionsMethodNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();

            //Do test
            var xmlString = model.Serialize();

            //Check
            var modelConfirm = SerializerManager.Deserialize<Model>(xmlString);
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm);

            Assert.True(xmlString == xmlStringConfirm);
        }
        [Test]
        public void SerializerRandomModelWithNamespacesNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("dmn", "http://www.domain.com");

            //Do test
            var xmlString = model.Serialize(namespaces);

            //Check
            var modelConfirm = SerializerManager.Deserialize<Model>(xmlString);
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm, namespaces);

            Assert.True(xmlString == xmlStringConfirm);
        }

        [Test]
        public void SerializerRandomModelWithNamespacesHasException()
        {
            try
            {
                //Prepare
                var model = _fixture.Create<Model>();
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add("dmn", "http://www.domain.com");

                //Do test
                var xmlString = SerializerManager.Serialize<ModelDetail>(namespaces);
            }
            catch (Exception ex)
            {
                //Check
                Assert.True(ex.Message.Contains("There was an error generating the XML document."));
            }

        }

        [Test]
        public void DeserializerByteArrayNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();
            var xmlString = model.Serialize<Model>();
            var bytes = SerializerManager.SerializeToByteArray(model);

            //Do test
            var modelDeserialized = bytes.Deserialize<Model>();

            //Check
            var xmlStringConfirm = SerializerManager.Serialize(modelDeserialized);

            Assert.True(xmlString == xmlStringConfirm);
        }

        [Test]
        public void IsValidXmlFromRandomModelWithExtensionsMethodNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();

            //Do test
            var xmlString = model.Serialize();

            //Check
            Assert.True(SerializerManager.IsValidXml(xmlString));
        }

        [Test]
        public void SerializerRandomModelWithExtensionsToByteArrayNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();

            //Test
            byte[] bytes = model.SerializeToByteArray();

            //Check
            var xmlString = SerializerManager.Serialize(model);
            var modelConfirm = SerializerManager.Deserialize<Model>(bytes);
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm);

            Assert.True(xmlString == xmlStringConfirm);
        }

        [Test]
        public void SerializerRandomModelWithExtensionsToFileNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();
            var filePath = Path.GetTempFileName();

            Exception ex = null;

            //Test
            var res = model.SerializeToFile(filePath, out ex);

            Assert.True(res);
            Assert.IsNull(ex);
        }
    }
}
