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
            var modelConfirm = SerializerManager.Deserialize<Model>(xmlString);

            //Check
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm);

            Assert.True(xmlString == xmlStringConfirm);
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
                var modelConfirm = SerializerManager.Deserialize<ModelDetail>(xmlString);
            }
            catch (System.Exception ex)
            {
                Assert.True(ex.Message.Contains("There is an error in XML document"));
            }
        }
    }
}
