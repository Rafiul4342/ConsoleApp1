
using BaSyx.AAS.Client.Http;
using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.AssetAdministrationShell.Implementations;
using BaSyx.Models.Core.Common;
using BaSyx.Models.Extensions;
using MqttClientSouthboundSimulatedvalues;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        string path = "Maintenance_1/MaintenanceRecord/";
        string aaspath = "http://localhost:5180";


        string json = @"
       {
            ""idShort"": ""MaintenanceRecord"",
            ""modelType"": {
                ""name"": ""SubmodelElementCollection""
            },
            ""value"": [
                {
                    ""idShort"": ""MaintenanceStart"",
                    ""modelType"": {
                        ""name"": ""Property""
                    },
                    ""value"": ""12"",
                    ""valueType"": ""string""
                },
                {
                    ""idShort"": ""MaintenanceEnd"",
                    ""modelType"": {
                        ""name"": ""Property""
                    },
                    ""value"": ""12jnkjn"",
                    ""valueType"": ""string""
                },
                {
                    ""idShort"": ""MaintenanceCompletionTime"",
                    ""modelType"": {
                        ""name"": ""Property""
                    },
                    ""value"": 0.0,
                    ""valueType"": ""double""
                },
                {
                    ""idShort"": ""MaintenanceStaff"",
                    ""modelType"": {
                        ""name"": ""Property""
                    },
                    ""value"": ""100000"",
                    ""valueType"": ""string""
                },
                {
                    ""idShort"": ""MaintenanceCost"",
                    ""modelType"": {
                        ""name"": ""Property""
                    },
                    ""value"": 1000,
                    ""valueType"": ""double""
                }
]
        }";
        /*
            var Client = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var apiUrl = string.Concat(aaspath,"/", path);
            Console.WriteLine(apiUrl);

           var response = await Client.PutAsync("http://localhost:5180/Maintenance_1/MaintenanceRecord/Record1", content);

           if (response.IsSuccessStatusCode)
           {
               // The update was successful
               Console.WriteLine("Submodel element collection updated successfully.");
           }
           else
           {
               // Handle errors or error responses from the server
               Console.WriteLine("Failed to update submodel element collection.");
           }

            SubmodelElementCollection submodelElementColl = JsonConvert.DeserializeObject<SubmodelElementCollection>(json);
            //Console.WriteLine(submodelElementColl.GetType());
            Console.WriteLine(submodelElementColl.ToJson());
            string result = JsonConvert.SerializeObject(submodelElementColl) as string;
            Console.WriteLine(result);
            submodelElementColl.IdShort = "Record2";

        */
           
            var aasClient = new AssetAdministrationShellHttpClient(new Uri(aaspath));


        /*    
                foreach (var submodel in submodelElementColl.Value)
                    {

                    var vale = submodel.GetValue();
                    Console.WriteLine(submodel.IdShort);
                    Console.WriteLine(vale.Value);
                    try
                    {
                      var updv=  aasClient.UpdateSubmodelElementValue("MaintenanceSubmodel", string.Concat(path,"/",submodel.IdShort), vale);
                        if (updv.Success) { Console.WriteLine("value updated"); }
                        else { Console.WriteLine("Vale not updated"); }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    }

                */
        SubmodelElementCollection submodelElementColl = JsonConvert.DeserializeObject<SubmodelElementCollection>(json);
        //Console.WriteLine(submodelElementColl.GetType());
        Console.WriteLine(submodelElementColl.ToJson());
        string result = JsonConvert.SerializeObject(submodelElementColl) as string;
        Console.WriteLine(result);
        try
        {

            try
            {
                 var submodelElementCol = JsonConvert.DeserializeObject<SubmodelElementCollection>(json);

                Console.WriteLine(submodelElementColl);
                var data = submodelElementColl.Value[0];
                Console.WriteLine(data.GetType());
                Console.WriteLine($"This is the data we are trying to update {0}",data);

                var update = aasClient.CreateOrUpdateSubmodelElement("MaintenanceSubmodel", path , data);
                Console.WriteLine("Value updated");
                Console.WriteLine(update.Messages);

                if (update.Success)
                {
                    Console.WriteLine("updata successfull");
                }
                else
                {
                    Console.WriteLine("Not successful");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
           
            var datanew = aasClient.RetrieveSubmodelElement("MaintenanceSubmodel", path);
            Console.WriteLine(datanew.Entity.ToJson());
        } 
      
        catch(Exception ex) 
        {
        Console.WriteLine(ex.Message);
        }
        
       
       
        /*
       

        try
        {
            // Specify the path to the MaintenanceRecord in the AAS
            string submodelPath = "Maintenance_1/MaintenanceRecord";

            // Retrieve the existing MaintenanceRecord from the AAS
            var existingRecord =  aasClient.RetrieveSubmodelElement("MaintenanceSubmodel", submodelPath);

            if (existingRecord != null)
            {
                Console.WriteLine(existingRecord.Entity.ToJson());
                // Update the existing record with the new values
                existingRecord.Entity.SetValue(submodelElementColl);
                
                
              Console.WriteLine(existingRecord.Entity.ToJson());
               var data1 = existingRecord.Entity.ToJson();

                var collection = JsonConvert.DeserializeObject<SubmodelElementCollection>(data1);
          

                var updateResult = aasClient.CreateOrUpdateSubmodelElement("MaintenanceSubmodel", submodelPath,collection); 
          
                if (updateResult.Success)
                {
                    Console.WriteLine("MaintenanceRecord updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update MaintenanceRecord: " );
                }
            }
            else
            {
                Console.WriteLine("MaintenanceRecord not found in the AAS.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        // Display the number of command line arguments.
       */
       // UpdateSubmodelElement e = new UpdateSubmodelElement(aaspath);
     //  var data = await e.getData(path);
      // await e.UpdateSubmodelElementVale(path, submodelElementColl);
       // await e.UpdateSubmodelElementVale(path, data);*/
    }
}
