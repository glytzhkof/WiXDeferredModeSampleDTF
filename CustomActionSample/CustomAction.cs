using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CustomActionSample
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult SaveDeferredCustomActionData(Session session)
        {
            session.Log("Begin CustomAction SaveDeferredCustomActionData");

            var cad = new CustomActionData
            {
                // The COMPANYID is defined in the WiX source, the other properties are not
                { "COMPANYID", session["COMPANYID"] },
                { "DBTYPE", "mysql" },
                { "LOGSFOLDER", "LOGSFOLDER" },
                { "CONNECTIONSTRING", "Database something something" },
                { "INSTALLFOLDER", @"C:\test" }
            };

            // Send the properties to one or more deferred custom actions:
            session.DoAction("DeferredAction", cad);

            // To send to another custom action, use the same approach:
            // session.DoAction("SomeCustomAction", cad);

            return ActionResult.Success;
        }

        [CustomAction]
        public static ActionResult DeferredAction(Session session)
        {
            MessageBox.Show("COMPANYID:\n\n" + session.CustomActionData["COMPANYID"], "Debugging:", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            var connectionstring = "DBTYPE=" + session.CustomActionData["DBTYPE"] + ";" +
                                   "CONNECTIONSTRING=" + session.CustomActionData["CONNECTIONSTRING"] + ";" +
                                   "INSTALLFOLDER=" + session.CustomActionData["INSTALLFOLDER"];

            MessageBox.Show("We are in deferred mode custom action. Here is the string we have made via string concatenation:\n\n" + connectionstring, "Debugging:", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            return ActionResult.Success;
        }
    }
}
