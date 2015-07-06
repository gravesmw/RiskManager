using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravesConsultingLLC.RiskManager.Core.Model
{
    public class ContainerViewHierarchy
    {
        public int ContainerViewID { get; set; }
        public string Name { get; set; }
        public int? ParentContainerViewID { get; set; }
        public List<ContainerViewHierarchy> Children { get; set; }

        public ContainerViewHierarchy()
        {
            this.Children = new List<ContainerViewHierarchy>();
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
