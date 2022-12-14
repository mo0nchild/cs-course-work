using CSCourseWork.Windows;

namespace CSCourseWork
{
    internal static class Program : System.Object
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>

        [STAThreadAttribute]
        public static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();
            Application.Run(new AuthForm());
        }
    }
}