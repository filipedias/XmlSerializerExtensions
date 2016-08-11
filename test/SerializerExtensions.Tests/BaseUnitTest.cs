using Ploeh.AutoFixture;

namespace SerializerExtensions.Tests
{
    public class BaseUnitTest
    {
        protected readonly Fixture _fixture;

        public BaseUnitTest()
        {
            _fixture = new Fixture();
        }
    }
}
