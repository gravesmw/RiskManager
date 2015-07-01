using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravesConsultingLLC.RiskManager.Core.Model
{
    public class ContainerView
    {
        public int ViewID { get; set; }
        public string Name { get; set; }

        public static IEnumerable<ContainerView> Get(IRepository SqlRepository)
        {
            string Procedure = 
                "Report.spGetContainerViews";

            return
                SqlRepository.Get<ContainerView>(Procedure, null, true);
        }

        public static List<ContainerViewHierarchy> GetHierarchy(int ViewID, IRepository SqlRepository)
        {
            List<ContainerViewHierarchy> ContainerViewHierarchies = new List<ContainerViewHierarchy>();

            string Procedure = "Report.spGetContainerView";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
              { "@ViewID", ViewID }
            };

            IEnumerable<ContainerViewHierarchy> Hierarchy =
                SqlRepository.Get<ContainerViewHierarchy>(Procedure, Parameters, true);

            if (Hierarchy != null && Hierarchy.Count() > 0)
            {
                foreach (ContainerViewHierarchy View in Hierarchy)
                {
                    if (View.ParentContainerViewID != null)
                    {
                        ContainerViewHierarchy Parent = Hierarchy.First<ContainerViewHierarchy>(x => x.ContainerViewID == View.ParentContainerViewID);
                        Parent.Children.Add(View);
                    }
                }
            }

            ContainerViewHierarchies.Add(
                Hierarchy.FirstOrDefault(x => x.ParentContainerViewID == null)
            );

            return ContainerViewHierarchies;
        }
    }
}
