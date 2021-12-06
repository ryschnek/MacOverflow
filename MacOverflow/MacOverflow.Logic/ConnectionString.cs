using System;
using System.Collections.Generic;
using System.Text;

namespace MacOverflow.Logic
{
    public static class ConnectionString
    {
        public static string MyConnectionString { get; set; } = "Server=tcp:4hc3.database.windows.net,1433;Initial Catalog=4HC3Project;Persist Security Info=False;User ID=schneker;Password=secret;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
