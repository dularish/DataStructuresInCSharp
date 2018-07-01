using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    public enum NodeLabelType
    {
        Temporary, Permanent
    }
    public class NodeLabel
    {
        public int NodeIndex;

        public int LabelValue;

        public NodeLabelType Type;

        public int PermanentNodeRef = -1;

        public NodeLabel(int nodeIndex, int labelValue, NodeLabelType type, int permNodRef = -1)
        {
            NodeIndex = nodeIndex;
            LabelValue = labelValue;
            Type = type;

            if (type == NodeLabelType.Temporary)
            {
                PermanentNodeRef = permNodRef;
            }
        }
    }
}
