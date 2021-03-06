using System.Collections;
using System.Collections.Generic;
using CgmInfo.Commands;

namespace CgmInfoGui.ViewModels.Nodes
{
    public abstract class NodeBase : IEnumerable<NodeBase>
    {
        public abstract string DisplayName { get; }
        public List<NodeBase> Nodes { get; } = new List<NodeBase>();
        public Command Command { get; set; }

        public virtual void Add(NodeBase node)
        {
            Nodes.Add(node);
        }

        public IEnumerator<NodeBase> GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }
    }
}
