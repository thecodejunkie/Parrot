namespace Parrot.Nodes
{
    using Parrot.Nodes;

    public enum ValueType
    {
        StringLiteral,
        Property,
        Local,
        Keyword
    }

    public class AttributeNode : AbstractNode
    {
        public string Key { get; internal set; }
        public string Value { get; internal set; }

        public ValueType ValueType { get; internal set; }

        public AttributeNode(string key, string value)
        {
            Key = key;

            if (value != null)
            {
                if (IsWrappedInQuotes(value))
                {
                    ValueType = ValueType.StringLiteral;
                    //strip quotes
                    value = value.Substring(1, value.Length - 2);
                }
                else if (key != "class")
                {
                    if (key == "id")
                    {
                        ValueType = ValueType.StringLiteral;
                    }
                    else if (value == "this")
                    {
                        ValueType = ValueType.Local;
                    }
                    else
                    {
                        ValueType = ValueType.Property;
                    }
                }
                //else if (key == "id")
                //{
                //    ValueType = ValueType.Property;
                //}
            }

            Value = value;
        }

        public string GetValue()
        {
            if (Value == null)
            {
                return null;
            }


            if (ValueType == ValueType.Property)
            {
                var value = GetModelValue(Value);
                return value != null ? value.ToString() : null;
            }

            return Value;
        }

        private bool IsWrappedInQuotes(string value)
        {
            return value != null && ((value.StartsWith("\"") && value.EndsWith("\"")) || (value.StartsWith("'") || value.EndsWith("'")));
        }

        public override bool IsTerminal
        {
            get { return false; }
        }

        public override string ToString()
        {

            if (ValueType == ValueType.Property)
            {
                return string.Format("{0}=\"{1}\"", Key, GetModelValue(Value));
            }

            return string.Format("{0}={1}", Key, Value);
        }
    }
}