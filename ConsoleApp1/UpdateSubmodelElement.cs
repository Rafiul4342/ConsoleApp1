using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using BaSyx.AAS.Client.Http;
using BaSyx.Models;
using BaSyx.Models.Core.AssetAdministrationShell.Generics;
using BaSyx.Models.Core.AssetAdministrationShell.Implementations;
using BaSyx.Models.Core.Common;
using BaSyx.Models.Extensions;
using BaSyx.Utils.ResultHandling;
using Newtonsoft.Json;

namespace MqttClientSouthboundSimulatedvalues
{

    public class UpdateSubmodelElement
    {
        public AssetAdministrationShellHttpClient client;

        public static SubmodelElementCollection? coll;
        public SubmodelElementCollection newCollection { get; set; }
        static int i = 0;

      public UpdateSubmodelElement(string url)
        {
            client = new AssetAdministrationShellHttpClient(new Uri(url));
            
        }


        public async Task UpdateRecords(string path, SubmodelElementCollection Collection) { 
        await Task.Delay(1000);
        }
        public async Task UpdateSubmodelElementVale(String Path,SubmodelElementCollection Col)
        {
            await Task.Delay(1000);
            var data =JsonConvert.SerializeObject(Col);
            Console.WriteLine(data);

            try
            {
                var record = client.RetrieveSubmodelElement("MaintenanceSubmodel", Path);
                if (record.Success && record!= null) 
                {
                    var type = Col.GetType();
                    var v = Col.Value;
                    Console.WriteLine(v.GetType());

                    record.Entity.SetValue(Col);
                    ISubmodelElement submodelElement = record.Entity;
                 //   ISubmodelElement submodelElement1 = JsonConvert.DeserializeObject<ISubmodelElement>(record.Entity.ToJson());
                // List<ISubmodelElement> Element= new List<ISubmodelElement>();
                // Element.Add(submodelElement);





                    try
                    {
                        var update = client.CreateOrUpdateSubmodelElement("MaintenanceSubmodel", "Maintenance_1", submodelElement);
                        Console.WriteLine(update.Messages);
                        if (update.Success)
                        {
                            Console.WriteLine("Value Update");

                        }
                        else
                        {
                            Console.WriteLine("Not Updated");
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                   
                    var vale = record.GetType();
                    var val = record.Entity.ToJson();


                    Console.WriteLine(val);
                    Console.WriteLine(vale.ToString());
                    
                 
                    
                    
               //  var updatedrecord = JsonConvert.DeserializeObject<SubmodelElementCollection>(updatejson);
                  //var value = updatedrecord.GetValue().GetType(); 
                    //Console.WriteLine($"Updated {value}");
                  //  Console.WriteLine(v.Value.ToJson());
                    try {
                        var UpdatedRecord = client.CreateOrUpdateSubmodelElement("MaintenanceSubmodel", string.Concat(Path,"_",i.ToString()), (ISubmodelElement)v.Values);
                        i++;
                        if (UpdatedRecord.Success)
                        {
                            Console.WriteLine(UpdatedRecord);
                        }
                        else
                        {
                            Console.WriteLine("RecordNotupdated");
                        }
                    } 
                    catch(Exception ex)
                    { 
                        Console.WriteLine(ex.ToString());
                    }
                    
                    

                }

            }
            catch 
            (Exception ex) 
            
            { Console.WriteLine(ex.Message); }
            
          
            
          
         //   var submodelElment = client.RetrieveSubmodelElement("MaintenanceSubmodel",Path);
           // Console.WriteLine(submodelElment.Entity.ToJson());

           
              
                 // submodelElment.Entity.SetValue(newCollection);
                    Console.WriteLine(client.RetrieveSubmodelElement("MaintenanceSubmodel",Path).Entity.ToJson());
                try
                {
                    var update = client.CreateOrUpdateSubmodelElement("MaintenanceSubmodel", Path, newCollection);
                    Console.WriteLine(client.RetrieveSubmodelElement("MaintenanceSubmodel", Path).Entity.ToJson());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                   
                    
               
              
       }

     

        public async Task<SubmodelElementCollection?> getData(string path)
        {
            await Task.Delay(1000);

            var data = client.RetrieveSubmodelElement("MaintenanceSubmodel", path);
            if (data != null)
            {
                var vale = data.Entity.ToJson();
                Console.WriteLine(vale);
              

           SubmodelElementCollection submodelElementColl = JsonConvert.DeserializeObject<SubmodelElementCollection>(vale);
      

                Console.WriteLine(submodelElementColl.GetType());
                
                    foreach(var item in submodelElementColl.Value)
                {
                    Console.WriteLine(item.IdShort);
                    Console.WriteLine(item.GetValue().Value);
                    if(item.IdShort == "MaintenanceStart")
                    {
                        IValue updatedValue = new ElementValue("10/27/2023 10:07:42 AM", typeof(string));
                        item.SetValue(updatedValue);
                        Console.WriteLine(item.GetValue().Value);
                        submodelElementColl.SetValue(item);
                      
                    }
                }

                data.Entity.SetValue(submodelElementColl.Value);
                Console.WriteLine(data.Entity.ToJson());

             //  var update= client.CreateOrUpdateSubmodelElement(path, data);
               
                Console.WriteLine(JsonConvert.SerializeObject(submodelElementColl));   
                return submodelElementColl;

            }
            else
            {
                return null;
            }

           
        }

    }
}
