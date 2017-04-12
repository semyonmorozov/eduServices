using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web;

namespace LibraryService
{
    public class Program
    {
        public static void Main()
        {
            var host = new ServiceHost(typeof(LibraryService), new Uri("http://localhost:8886/ls"));

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            smb.HttpGetUrl = new Uri("http://localhost:8886/ls/Meta");
            host.Description.Behaviors.Add(smb);

            host.Open();

            Console.WriteLine("Press enter...");
            Console.ReadLine();

            host.Close();
        }
    }
}