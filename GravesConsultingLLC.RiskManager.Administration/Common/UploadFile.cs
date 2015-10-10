using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace GravesConsultingLLC.RiskManager.Administration.Common
{
    public class UploadFile
    {
        public enum UploadFileType {  Discovery, Defect }
        public enum UploadFileState { WaitingForLastFile = 1, ReadyToProcess = 2, ErrorProcessing = 3, Processed = 4 }

        public string FilePath { get; set; }
        public string Identifier { get; set; }
        public int PageNumber { get; set; }
        public bool LastPage { get; set; }
        public UploadFileType FileType { get; set; }
        public UploadFileState FileState { get; set; }

        public UploadFile(string File)
        {
            this.FilePath = File;
        }

        public void Track(IRepository SqlRepository)
        {
            this.ParseHeader();


        }

        private void ParseHeader()
        {
            using (XmlTextReader Reader = new XmlTextReader(this.FilePath))
            {
                while (Reader.Read())
                {
                    if (Reader.IsStartElement("gc:discovery"))
                    {
                        this.FileType = UploadFileType.Discovery;
                        GetCommonHeaderAttributes(Reader);
                        break;
                    }


                    if (Reader.IsStartElement("gc:defects"))
                    {
                        this.FileType = UploadFileType.Defect;
                        GetCommonHeaderAttributes(Reader);
                        break;
                    }
                }
            }
        }

        private void GetCommonHeaderAttributes(XmlTextReader Reader)
        {
            this.Identifier = Reader.GetAttribute("id");

            int Page;
            if (int.TryParse(Reader.GetAttribute("page-number"), out Page))
            {
                this.PageNumber = Page;
            }
            else
            {
                throw new Exception("Error parsing page number");
            }

            bool Last;
            if (bool.TryParse(Reader.GetAttribute("last-page"), out Last))
            {
                this.LastPage = Last;
            }
            else
            {
                throw new Exception("Error parsing last page indicator");
            }
        }

    }
}