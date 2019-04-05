using NUnit.Framework;
using project.presentation_layer;

namespace Tests
{
    public class Tests
    {

        [Test]
        public void Test1()
        {
            project.presentation_layer.ConsoleUserInterface c = new ConsoleUserInterface();
            Assert.Pass();
        }
    }
}