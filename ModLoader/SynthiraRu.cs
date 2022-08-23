﻿using AngleSharp;
using AngleSharp.Dom;
using AngleSharpExtantions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace ModLoader
{
    internal class SynthiraRu : BaseParse, IParseConfig, IParseData
    {
        // propfull = быстро создать свойсво

        internal SynthiraRu(string url) : base(url)
        {


        }

        //public string Name{ get => document.QuerySelectorAll(".filekmod .entryLink"); }

        public string DateUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LinkToPageMod { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LinkDownload { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Img { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsUpdate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IHtmlCollection<IElement> Names => FindAll(".entryLink");

        public IHtmlCollection<IElement> Descriptions => FindAll(".messzhfg span");

        public IHtmlCollection<IElement> Blocks => FindAll(".filekmod");


        public string GetData()
        {
            var blocks = Blocks;
            IBlockData[] datas = new IBlockData[blocks.Length];
            for (int i = 0; i < blocks.Length; i++)
            {
                var data = Data;
                var modInfo = blocks[i].Find(".entryLink");
                data.Link = modInfo.GetAttribute("href");
                data.Name = modInfo.Text().RemoveTabbing();
                data.Description = blocks[i].Find(".messzhfg span").Text();
                data.DateUpdate = blocks[i].Find(".datemsz").Html().ParseDate();
                datas[i] = data;
            }

            return  datas.DumpAsYaml();
        }

        public string GetData(IBlockData data)
        {
            var blocks = Blocks;
            //string[] data = new string[blocks.Length];
            for (int i = 0; i < blocks.Length; i++)
            {
                //data[i] = blocks[i].Text();
                data.Name= blocks[i].Text();
            }
            
            return string.Join(", ", data);
            //return Blocks[1].Html();
            //test.Html();
            //return test.FindAll(".entryLink").Text();
            //var context = BrowsingContext.New(Configuration.Default);
            //var document = await context.OpenAsync(r => r.Content(test.Html()));
            //return document.ToHtml();
        }
    }
}
