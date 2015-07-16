using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravesConsultingLLC.RiskManager.Core.Model
{
    public class Hierarchy
    {
        public int NodeID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public List<Hierarchy> Children { get; set; }

        public Hierarchy()
        {
            this.Children = new List<Hierarchy>();
        }

        public static void DeleteContainerViewEntry(int ContainerViewID, IRepository SqlRepository)
        {
            string Procedure = "Report.spDeleteContainerViewEntry";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@ContainerViewID", ContainerViewID }
            };

            SqlRepository.Put(Procedure, Parameters);
        }
    }
}
