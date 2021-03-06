namespace Parrot.Nodes
{
    using Parrot;
    using Parrot.Nodes;

    public class ParameterNode : AbstractNode
    {
        public string Value { get; private set; }

        public ValueType ValueType { get; private set; }

        public ParameterNode(string value)
        {

            if (IsWrappedInQuotes(value))
            {
                ValueType = ValueType.StringLiteral;
                //strip quotes
                value = value.Substring(1, value.Length - 2);
            }
            else
            {
                if (value == "this")
                {
                    ValueType = Nodes.ValueType.Local;
                }
                else
                {
                    ValueType = ValueType.Property;
                }
            }

            Value = value;
        }

        public object GetPropertyValue()
        {

            if (ValueType == ValueType.Property)
            {
                var value = GetModelValue(Value);
                return value;
            }

            if (ValueType == ValueType.Local)
            {
                return Model;
            }

            return Value;
        }

        private bool IsWrappedInQuotes(string value)
        {
            return (value.StartsWith("\"") && value.EndsWith("\"")) || (value.StartsWith("'") || value.EndsWith("'"));
        }

        public override bool IsTerminal
        {
            get { return true; }
        }
    }
}