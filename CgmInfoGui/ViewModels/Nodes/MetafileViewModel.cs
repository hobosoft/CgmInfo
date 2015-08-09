﻿namespace CgmInfoGui.ViewModels.Nodes
{
    public class MetafileViewModel : NodeBase
    {
        public MetafileViewModel(string name)
        {
            Name = name;
            Descriptor = new SimpleNode("METAFILE DESCRIPTOR");
            Nodes.Add(Descriptor);
        }
        public string Name { get; }
        public SimpleNode Descriptor { get; }

        public override string DisplayName
        {
            get { return string.Format("BEGIN METAFILE: '{0}'", Name); }
        }
    }
}
