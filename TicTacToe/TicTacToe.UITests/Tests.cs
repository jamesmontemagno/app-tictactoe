using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TicTacToe.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }


        [Test]
        public void EnterNames()
        {
            app.Screenshot("First screen.");
            Assert.IsFalse(app.Query("StartGame").First().Enabled, "Button should not be enabled");

            app.EnterText("Player1", "James");
            app.DismissKeyboard();

            Assert.IsFalse(app.Query("StartGame").First().Enabled, "Button should not be enabled");

            app.EnterText("Player2", "Heather");
            app.DismissKeyboard();

            Assert.True(app.Query("StartGame").First().Enabled, "Button should be enabled");
            app.Repl();
        }
    }
}

