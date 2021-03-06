using CgmInfo.Traversal;

namespace CgmInfo.Commands.Attributes
{
    public class CharacterHeight : Command
    {
        public CharacterHeight(double height)
            : base(5, 15)
        {
            Height = height;
        }

        public double Height { get; private set; }

        public override void Accept<T>(ICommandVisitor<T> visitor, T parameter)
        {
            visitor.AcceptAttributeCharacterHeight(this, parameter);
        }
    }
}
