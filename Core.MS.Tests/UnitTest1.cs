using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.MS.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void SetUp()
        {
            try
            {
                Mapper.Configuration.AssertConfigurationIsValid();
            }
            catch (Exception)
            {
                Mapper.Initialize(cfg => cfg.AddProfile(new MappingProfile()));
            }
        }


        [DataRow(new object[]{"Mike", "Ruhl", 37})]
        [DataRow(new object[]{"Tony", "Stark", 42})]
        [DataRow(new object[]{"Barrack", "Obama", 57})]
        [TestMethod]
        public void MappingsValid(object[] input)
        {
            var firstName = (string)input[0];
            var lastName = (string)input[1];
            var age = (int)input[2];
            var domain = new DomainObject(){FirstName = firstName, LastName = lastName, Age = age};
            var mapped = Mapper.Map<DomainDto>(domain);
            Assert.AreEqual(firstName, mapped.FirstName);
            Assert.AreEqual(lastName, mapped.LastName);
            Assert.AreEqual(age, mapped.Age);
        }
    }
}
