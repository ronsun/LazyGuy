using System;
using FluentAssertions;
using LazyGuy.Tests.Constants;
using NUnit.Framework;

namespace LazyGuy.Utils.Tests
{
    [TestFixture()]
    public class StopwatchReporterTests_Nagative
    {
        #region Execute

        [Test()]
        public void ExecuteTest_NullAction_ThrowArgumentNullExceptionWithMessage()
        {
            //arrange
            Action stubAction = null;
            Action<long> stubReport = (tick) => { };

            //act
            Action act = () => { StopwatchReporter.Execute(stubAction, stubReport); };

            //assert
            act.Should().Throw<ArgumentNullException>().WithMessage(FakeMessageTemplates.ArgumentNull);
        }

        #endregion
    }
}