using DogWalking.DL.Context;
using DogWalking.DL.Migrations; // Add this using directive for Configuration
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace DogWalking.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Database.SetInitializer(
                    new MigrateDatabaseToLatestVersion<
                        DogWalkingContext,
                        Configuration
                    >()
                );

                // Force initialization
                using (var ctx = new DogWalkingContext())
                {
                    ctx.Database.Initialize(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error initializing the database:\n" + ex.Message,
                    "Startup Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}