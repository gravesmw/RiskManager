using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravesConsultingLLC.RiskManager.Core.Model
{
    public class ContainerViewEntry
    {
        public int? NodeID { get; set; } 
        public string Name { get; set; }
        public int? ViewID { get; set; }
        public int? ParentID { get; set; }

        public int Create(IRepository SqlRepository)
        {
            string Procedure = "Report.spCreateContainerViewEntry";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@Name", this.Name } ,
                { "@ViewID", this.ViewID } ,
                { "@ParentContainerViewID", this.ParentID } 
            };

            this.NodeID =
                SqlRepository.Put<int>(Procedure, Parameters, "@ContainerViewID");

            return (int)this.NodeID;
        }

        public void Update(IRepository SqlRepository)
        {
            string Procedure = "Report.spUpdateContainerViewEntry";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@ContainerViewID", this.NodeID } ,
                { "@ParentContainerViewID", this.ParentID } 
            };

            SqlRepository.Put(Procedure, Parameters);
        }
    }
}
