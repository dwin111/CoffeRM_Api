using HtmlAgilityPack;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBlazor.Pages
{
    public class OLXParseBase: ComponentBase
    {
        public string UrlToRom { get; set; }
        public string PriceRom { get; set; }
        public string NumberOfRooms { get; set; }
        public string Name  { get; set; }
        public string Image { get; set; }

        protected override void OnInitialized()
        {
           
           

        }

         public void LOadOLXPage()
        {
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(UrlToRom);

            PriceRom = doc.DocumentNode.SelectNodes("//*[@id=\"root\"]/div[1]/div[3]/div[3]/div[1]/div[2]/div[3]/h3")[0].InnerText;
            NumberOfRooms = doc.DocumentNode.SelectNodes("//*[@id=\"root\"]/div[1]/div[3]/div[3]/div[1]/div[2]/ul/li[9]/p")[0].InnerText;
            Name = doc.DocumentNode.SelectNodes("//*[@id=\"root\"]/div[1]/div[3]/div[3]/div[1]/div[2]/div[2]/h1")[0].InnerText;
            Image = doc.DocumentNode.SelectNodes("//*[@id=\"root\"]/div[1]/div[3]/div[3]/div[1]/div[1]/div/div[1]/div[1]/div/img").First().Attributes["src"].Value;
        }

    }
}
