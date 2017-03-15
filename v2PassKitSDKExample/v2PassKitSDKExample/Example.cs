/** PassKit C# SDK Example for API CORE v2
*
* @author  Apoorva Katta
* @email apoorvakatta@gmail.com
*/
using System;
using v2PassKitSDK;

namespace v2PassKitSDKExample
{
    class Example
    {
        static void Main(string[] args)
        {
            PassKit pk = new PassKit("<< yourApiKey >>","<< yourApiSecret >>");

            try
            {
                /* Create a Pass */
                //Pass toBeCreatedPass = new Pass();
                //toBeCreatedPass.templateName = "";
                //dynamic createdPassId = pk.createPass(toBeCreatedPass);
                //Console.WriteLine(createdPassId);

                /* Retrieve a Pass */
                //Pass toBeRetrievedPass = pk.retrievePass("<< passId>>");
                //Console.WriteLine(pk.PassToJsonString(toBeRetrievedPass));

                /* Search for a Pass */
                //Search search = new Search();
                //search.size = 10;
                //search.from = 2;
                //search.passFilter = new PassFilter();
                //search.passFilter.createdAt = new Operation();
                //search.passFilter.createdAt.gt = "2016"; // passes created in and after 2016 are returned
                //SearchResult searchResult = pk.searchPass(search);
                //Console.WriteLine("Search Result: \n" + pk.SearchResultToJsonString(searchResult));
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
