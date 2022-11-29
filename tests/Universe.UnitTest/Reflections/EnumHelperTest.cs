using Universe.Reflections;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace Universe.UnitTest.Reflections
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertToDic()
        {
            var dics = EnumHelper.ToDictionary<EnumA>();
            Assert.That(dics.Count, Is.EqualTo(2));
        }

        [Test]
        public void ConvertToDicWithDescription()
        {
            var dics = EnumHelper.ToDictionary<EnumA>();
            Assert.IsTrue(dics.Keys.Contains("Alpha"));
        }
    }
    public enum EnumA
    {
        [Description("Alpha")]
        A = 1,
        B = 2
    }
}