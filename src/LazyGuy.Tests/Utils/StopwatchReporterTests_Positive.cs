using System;
using LazyGuy.Utils;
using NSubstitute;
using NUnit.Framework;

namespace LazyGuy.Tests.Utils
{
    [TestFixture()]
    public class StopwatchReporterTests_Positive
    {
        #region Execute

        [Test()]
        public void ExecuteTest_InputAction_Excuted()
        {
            //arrange
            Action mockAction = Substitute.For<Action>();

            //act
            StopwatchReporter.Execute(mockAction);

            //assert
            mockAction.Received(1);
        }

        [Test()]
        public void ExecuteTest_InputActionWithReport_Excuted()
        {
            //arrange
            Action mockAction = Substitute.For<Action>();
            Action<long> stubReport = (tick) => { };

            //act
            StopwatchReporter.Execute(mockAction, stubReport);

            //assert
            mockAction.Received(1);
        }

        [Test()]
        public void ExecuteTest_InputReport_Excuted()
        {
            //arrange
            Action stubAction = () => { };
            Action<long> mockReport = Substitute.For<Action<long>>();

            //act
            StopwatchReporter.Execute(stubAction, mockReport);

            //assert
            mockReport.Received(1);
        }

        #endregion
    }
}
