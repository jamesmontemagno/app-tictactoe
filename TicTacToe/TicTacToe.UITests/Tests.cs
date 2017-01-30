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
            Assert.IsFalse(app.Query("StartGameButton").First().Enabled, "Button should not be enabled");

            app.EnterText("Player1", "James");
            app.DismissKeyboard();
            app.Screenshot("Entered Player 1.");
            Assert.IsFalse(app.Query("StartGameButton").First().Enabled, "Button should not be enabled");

            app.EnterText("Player2", "Heather");
            app.DismissKeyboard();

            app.Screenshot("Entered Player 2.");

            Assert.True(app.Query("StartGameButton").First().Enabled, "Button should be enabled");
        }

        [Test]
        public void PlayGame()
        {
            app.Screenshot("First screen.");
            app.EnterText("Player1", "Heather");
            app.EnterText("Player2", "James");
            app.DismissKeyboard();
            app.Screenshot("Name Entered.");
            app.Tap("StartGameButton");

            app.WaitForElement("Play0");
            app.Tap("Play0");

            app.Screenshot("Play 1.");

            app.Tap("Play2");

            app.Screenshot("Play 2.");

            app.Tap("Play4");

            app.Screenshot("Play 3.");

            app.Tap("Play5");

            app.Screenshot("Play 4.");

            app.Tap("Play8");

            app.Screenshot("Play 5.");

            app.WaitForElement("Heather Wins!");

            app.Screenshot("Win dialog up.");
        }
    }
}

