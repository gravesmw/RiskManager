using GravesConsultingLLC.RiskManager.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravesConsultingLLC.RiskManager.Core.Model
{
    public class ObjectType
    {
        public int? NodeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }

        public void Create(IRepository SqlRepository)
        {
            string Procedure = "Report.spCreateObjectType";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@Name", this.Name },
                { "@Description", this.Description },
                { "@ParentObjectTypeID", this.ParentID }
            };

            this.NodeID =
                SqlRepository.Put<int>(Procedure, Parameters, "@ObjectTypeID");
        }

        public void Update(IRepository SqlRepository)
        {
            string Procedure = "Report.spUpdateObjectType";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@ObjectTypeID", this.NodeID },
                { "@Name", this.Name },
                { "@Description", this.Description },
                { "@ParentObjectTypeID", this.ParentID }
            };

            SqlRepository.Put(Procedure, Parameters);
        }

        public static void Delete(int ObjectTypeID, IRepository SqlRepository)
        {
            string Procedure = "Report.spDeleteObjectType";

            Dictionary<string, object> Parameters = new Dictionary<string, object>(){
                { "@ObjectTypeID", ObjectTypeID }
            };

            SqlRepository.Put(Procedure, Parameters);
        }

        public static IEnumerable<Hierarchy> GetHierarchy(IRepository SqlRepository)
        {
            string Procedure = "Report.spGetObjectTypes";

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
