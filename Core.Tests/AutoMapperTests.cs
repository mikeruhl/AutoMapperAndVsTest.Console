using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Core.Tests
{
    [TestFixture]
    public class AutoMapperTests
    {
        [SetUp]
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


        [TestCase(new object[]{"Mike", "Ruhl", 37})]
        [TestCase(new object[]{"Tony", "Stark", 42})]
        [TestCase(new object[]{"Barrack", "Obama", 57})]
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
