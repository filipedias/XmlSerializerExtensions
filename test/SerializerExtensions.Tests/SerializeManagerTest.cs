﻿using Xunit;
using Ploeh.AutoFixture;
using SerializerExtensions.Tests.Entities;
using System;

namespace SerializerExtensions.Tests
{
    public class SerializeManagerTest : BaseUnitTest
    {
        [Fact]
        public void SerializerRandomModelNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();

            //Test
            var xmlString = SerializerManager.Serialize(model);

            //Check
            var modelConfirm = SerializerManager.Deserialize<Model>(xmlString);
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm);

            Assert.True(xmlString == xmlStringConfirm);
        }

        [Fact]
        public void SerializerRandomModelWithErrors()
        {
            try
            {
                //Prepare
                var model = _fixture.Create<Model>();

                //Test
                var xmlString = SerializerManager.Serialize(model, typeof(ModelDetail));

            }
            catch (System.Exception ex)
            {
                Assert.True(ex.Message == "There was an error generating the XML document.");
            }
        }

        [Fact]
        public void SerializerNullObjectWithErrors()
        {
            try
            {
                //Test
                var xmlString = SerializerManager.Serialize(null);

            }
            catch (System.Exception ex)
            {
                Assert.True(ex != null);
            }
        }


        [Fact]
        public void SerializerStringNoErrors()
        {
            //Prepare
            var model = _fixture.Create<string>();

            //Test
            var xmlString = SerializerManager.Serialize(model);

            //Check
            var modelConfirm = SerializerManager.Deserialize<string>(xmlString);
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm);

            Assert.True(xmlString == xmlStringConfirm);
        }


        [Fact]
        public void IsValidXmlFromRandomModelNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();

            //Test
            var xmlString = SerializerManager.Serialize(model);

            //Check
            Assert.True(SerializerManager.IsValidXml(xmlString));
        }

        [Fact]
        public void SerializerRandomModelToByteArrayNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();

            //Test
            byte[] bytes = SerializerManager.SerializeToByteArray(model);

            //Check
            var xmlString = SerializerManager.Serialize(model);
            var modelConfirm = SerializerManager.Deserialize<Model>(bytes);
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm);

            Assert.True(xmlString == xmlStringConfirm);
        }

        [Fact]
        public void SerializerRandomModelToFileNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();
            var filePath = System.IO.Path.GetTempFileName();

            Exception ex = null;

            //Test
            var res = SerializerManager.SerializeToFile(model, filePath, out ex);

            //Check
            Assert.True(res);
            Assert.Null(ex);
        }

        [Fact]
        public void DeserializerRandomModelMethodNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();
            var xmlString = SerializerManager.Serialize(model);

            //Do test
            var modelConfirm = SerializerManager.Deserialize<Model>(xmlString);

            //Check
            var xmlStringConfirm = SerializerManager.Serialize(modelConfirm);

            Assert.True(xmlString == xmlStringConfirm);
        }

        [Fact]
        public void DeserializerRandomModelWithErrors()
        {
            try
            {
                //Prepare
                var model = _fixture.Create<Model>();
                var xmlString = SerializerManager.Serialize(model);

                //Test
                var modelConfirm = SerializerManager.Deserialize<ModelDetail>(xmlString);
            }
            catch (System.Exception ex)
            {
                Assert.True(ex.Message.Contains("There is an error in XML document"));
            }
        }

        [Fact]
        public void DeserializerRandomModelFromFileNoErrors()
        {
            //Prepare
            var model = _fixture.Create<Model>();
            var filePath = System.IO.Path.GetTempFileName();
            Exception ex = null;
            SerializerManager.SerializeToFile(model, filePath, out ex);

            //Test
            Model modelConfirm = null;
            var res = SerializerManager.DeserializeFromFile(filePath, out modelConfirm, out ex);

            //Check
            var xmlStringConfirm = SerializerManager.Serialize<Model>(modelConfirm);
            Assert.True(res);
            Assert.Null(ex);
            Assert.True(xmlStringConfirm.Trim() == System.IO.File.ReadAllText(filePath).Trim());
        }
    }
}
