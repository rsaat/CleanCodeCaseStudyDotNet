namespace cleancoderscom.tests.fixtures
{

    public class FixtureSetup
    {
        public FixtureSetup()
        {
            //LaunchDebugger();

            TestSetup.setupContext();
        }

        private void LaunchDebugger()
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debugger.Launch();
            }
        }

    }

}