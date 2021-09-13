using System.Collections;
using System.Collections.Generic;
using IMDBClone.Domain.DTO.User;
using IMDBClone.Domain.Extensions.Types;
using Xunit;

namespace IMDBClone.GeneralTest.UnitTests
{
    public class TypeConverterTest
    {
        [Theory, ClassData(typeof(EmailEnumerator))]
        public void TestTypeShouldWork<T>(List<T> list, string type ,bool isValid) where T : class
        {
            var elem = list.ToListString();
            var dataType = elem.GetType().ToString();
            Assert.Equal(dataType == type, isValid);
        }
    }
    
    public class EmailEnumerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[]{new List<string>{"palushishpend@gmail.com"}, "System.String", true},
            new object[]{ new List<string>{"palushishpend@gmail.com", "user_test@domain.com"}, "System.String", true},
            new object[]{ new List<LoginDTO>{new LoginDTO(){Password = "test", Username = "test"}},"IMDBClone.Domain.DTO.User.LoginDTO", false },
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}