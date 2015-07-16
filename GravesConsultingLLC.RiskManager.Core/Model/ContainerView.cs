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

        public void Create(IRepository SqlRepository)
        {
            string Procedure =
                "Report.spCreateView";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@Name", this.Name }
            };

            this.ViewID = 
                SqlRepository.Put<int>(Procedure, Parameters, "@ViewID");
        }

        public static List<Hierarchy> GetHierarchy(int ViewID, IRepository SqlRepository)
        {
            List<Hierarchy> ContainerViewHierarchies = new List<Hierarchy>();

            string Procedure = "Report.spGetContainerView";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
              { "@ViewID", ViewID }
            };

            IEnumerable<Hierarchy> Hierarchy =
                SqlRepository.Get<Hierarchy>(Procedure, Parameters, true);

            if (Hierarchy != null && Hierarchy.Count() > 0)
            {
                foreach (Hierarchy View in Hierarchy)
                {
                    if (View.ParentID != null)
                    {
                        Hierarchy Parent = Hierarchy.First<Hierarchy>(x => x.NodeID == View.ParentID);
                        Parent.Children.Add(View);
                    }
                }
            }

            ContainerViewHierarchies.Add(
                Hierarchy.FirstOrDefault(x => x.ParentID == null)
            );

            return ContainerViewHierarchies;
        }
    }
}
