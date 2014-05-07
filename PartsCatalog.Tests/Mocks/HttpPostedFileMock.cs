using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PartsCatalog.Tests.Mocks
{
    public class HttpPostedFileMock : HttpPostedFileBase
    {
        private string fileName;

        public override string FileName
        {
            get
            {
                return fileName;
            }
        }

        public override int ContentLength
        {
            get
            {
                return fileName.Length;
            }
        }

        public HttpPostedFileMock(string fileName)
        {
            this.fileName = fileName;
        }

        public override void SaveAs(string filename)
        {
        }
    }
}
