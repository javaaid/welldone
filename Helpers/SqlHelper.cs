using System;
using System.Configuration;
using System.ServiceProcess;

namespace WelldonePOS.Helpers
{
    public static class SqlHelper
    {
        public static string GetConnectionString()
        {
            string connString = string.Empty;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["WelldonePOS.Properties.Settings.connectionString"];

            if (settings != null)
                connString = settings.ConnectionString;

            return connString;
        }

        public static string GetServiceStatus()
        {
            string serviceName = "MSSQLSERVER";
            string serviceStatus;

            ServiceController serviceController = new ServiceController(serviceName);

            try
            {
                serviceStatus = serviceController.Status.ToString();
            }
            catch (Exception) 
            {
                serviceStatus = string.Format("Layanan [{0}] tidak ditemukan. Pastikan layanan telah terpasang sebelum melanjutkan proses.", serviceName);
            }

            return serviceStatus;
        }
    }
}
