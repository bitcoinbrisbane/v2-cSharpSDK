/*
 * PassKit C# Wrapper for PassKit API v2:
 * https://dev.passkit.net/v2
 */
using System;
using v2PassKitSDK;

namespace v2PassKitSDKExample
{
	class Example
	{
		static void Main(string[] args)
		{
			PassKit pk = new PassKit("<< your-api-key >>", "<< your-api-secret >>");

			try
			{
				/* Create a Pass */
				Pass p1 = new Pass();
				p1.templateName = "<< your-template-name >>";

				string pid = pk.createPass(p1);
				Console.WriteLine("https://q.passkit.net/p-" + pid);

				/* Retrieve a Pass */
                Pass p2 = pk.retrievePassById(pid);
                Console.WriteLine(p2.campaignName);

                /* Search for a Pass */
                Pass p3 = new Pass();
                p3.passbook = new PassPassbook();
                p3.passbook.fgColor = "#ffffff"; // set text colors to white
                p3.passbook.labelColor = "#ffffff"; // set text colors to white

                string pidUpdate = pk.updatePass(pid, p3);
                Console.WriteLine("https://q.passkit.net/p-" + pidUpdate);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
