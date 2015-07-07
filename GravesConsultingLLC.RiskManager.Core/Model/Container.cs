using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravesConsultingLLC.RiskManager.Core.Model
{
    public class Container
    {
        public int? ContainerViewID { get; set; } 
        public string Name { get; set; }
        public int? ViewID { get; set; }
        public int? ParentContainerViewID { get; set; }

        public int Create(IRepository SqlRepository)
        {
            string Procedure = "Report.spCreateContainer";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@Name", this.Name } ,
                { "@ViewID", this.ViewID } ,
                { "@ParentContainerViewID", this.ParentContainerViewID } 
            };

            this.ContainerViewID =
                SqlRepository.Put<int>(Procedure, Parameters, "@ContainerViewID");

            return (int)this.ContainerViewID;
        }
    }
}
