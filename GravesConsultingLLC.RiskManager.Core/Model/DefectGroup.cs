using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravesConsultingLLC.RiskManager.Core.Model
{
    public class DefectGroup
    {
        public int? NodeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }

        public void Create(IRepository SqlRepository)
        {
            string Procedure = "Report.spCreateDefectGroup";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@Name", this.Name },
                { "@Description", this.Description },
                { "@ParentDefectGroupID", this.ParentID }
            };

            this.NodeID =
                SqlRepository.Put<int>(Procedure, Parameters, "@DefectGroupID");
        }

        public void Update(IRepository SqlRepository)
        {
            string Procedure = "Report.spUpdateDefectGroup";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@DefectGroupID", this.NodeID },
                { "@Name", this.Name },
                { "@Description", this.Description },
                { "@ParentDefectGroupID", this.ParentID }
            };

            SqlRepository.Put(Procedure, Parameters);
        }

        public static void Delete(int DefectGroupID, IRepository SqlRepository)
        {
            string Procedure = "Report.spDeleteDefectGroup";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@DefectGroupID", DefectGroupID }
            };

            SqlRepository.Put(Procedure, Parameters);
        }

        public static IEnumerable<Hierarchy> GetHierarchy(IRepository SqlRepository)
        {
            string Procedure = "Report.spGetDefectGroups";

            IEnumerable<Hierarchy> BaseHierarchy =
                SqlRepository.Get<Hierarchy>(Procedure, null, true);

            if (BaseHierarchy != null && BaseHierarchy.Count() > 0)
            {
                foreach (Hierarchy View in BaseHierarchy)
                {
                    if (View.ParentID != null)
                    {
                        Hierarchy Parent = BaseHierarchy.First<Hierarchy>(x => x.NodeID == View.ParentID);
                        Parent.Children.Add(View);
                    }
                }
            }

            return
                BaseHierarchy.Where(
                x => x.ParentID == null
            );
        }
    }
}
