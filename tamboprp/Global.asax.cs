using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Negocio;
using tamboprp;

namespace tamboprp
{
    public class Global : HttpApplication
    {
        private const string DummyCacheItemKey = "TareaProg";
        private const string DummyPageUrl = "http://www.tamboprp.uy/BlankPage.aspx";

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            RegisterCacheEntry();
        }

        private bool RegisterCacheEntry()
        {
            if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return false;

            HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", null,
                DateTime.MaxValue, TimeSpan.FromMinutes(720),
                CacheItemPriority.Normal, new CacheItemRemovedCallback(CacheItemRemovedCallback));

            return true;
        }

        public void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            Debug.WriteLine("Cache item callback: " + DateTime.Now.ToString());
            HitPage();
            DoWork();
        }

        private void HitPage()
        {
            var client = new WebClient();
            client.DownloadData(DummyPageUrl);
        }


        private void DoWork()
        {
            Fachada.Instance.CorrerTareaProgramadas();
            //DoSomeFileWritingStuff();
        }


        //private void DoSomeFileWritingStuff()
        //{
        //    Debug.WriteLine("Writing to file...");

        //    try
        //    {
        //        using (StreamWriter writer = new StreamWriter(@"c:\Cachecallback.txt", true))
        //        {
        //            writer.WriteLine("Cache Callback: {0}", DateTime.Now);
        //            writer.Close();
        //        }
        //    }
        //    catch (Exception x)
        //    {
        //        Debug.WriteLine(x);
        //    }

        //    Debug.WriteLine("File write successful");
        //}

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            // If the dummy page is hit, then it means we want to add another item in cache

            if (HttpContext.Current.Request.Url.ToString() == DummyPageUrl)
            {
                // Add the item in cache and when succesful, do the work.

                RegisterCacheEntry();
            }
        }


        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
