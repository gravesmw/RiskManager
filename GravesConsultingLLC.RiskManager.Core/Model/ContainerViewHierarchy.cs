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
    }
}
