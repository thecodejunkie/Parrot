using Newtonsoft.Json.Linq;

namespace Parrot.Nodes
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Parrot;
    using Parrot.Nodes;
    using Parser;

    public class Statement : AbstractNode
    {
        public ParameterNodeList Parameters { get; private set; }
        public string Name { get; private set; }
        public AttributeNodeList Attributes { get; private set; }
        public StatementList Children { get; private set; }

        protected Statement(string name)
        {
            Attributes = new AttributeNodeList();
            Children = new StatementList();
            Parameters = new ParameterNodeList();

            //required bullshit
            if (name.Contains(".") || name.Contains("#"))
            {
                foreach (var part in GetIdentifierParts(name))
                {
                    switch (part.Type)
                    {
                        case IdentifierType.Id:
                            if (part.Name.Length == 1)
                            {
                                throw new ParserException("Id must have a length");
                            }

                            if (Attributes.Any(a => a.Key == "id"))
                            {
                                throw new ParserException("Id added more than once");
                            }

                            AddAttribute(new AttributeNode("id", part.Name));
                            break;
                        case IdentifierType.Class:
                            if (part.Name.Length == 1)
                            {
                                throw new ParserException("Id must have a length");
                            }

                            AddAttribute(new AttributeNode("class", part.Name));
                            break;
                        case IdentifierType.Literal:
                            Name = part.Name;
                            break;
                    }
                }
            }
            else
            {
                Name = name;
            }

        }

        public Statement(string name, StatementTail statementTail)
            : this(name)
        {
            if (statementTail != null)
            {
                AddParameters(statementTail.Parameters);
                AddAttributes(statementTail.Attributes);
                AddChildren(statementTail.Children);
            }
        }

        private void AddAttributes(AttributeNodeList attributes)
        {
            if (attributes != null && attributes.Any())
            {
                foreach (var attribute in attributes)
                {
                    Attributes.Add(attribute);
                }
            }
        }

        private void AddParameters(ParameterNodeList parameters)
        {
            if (parameters != null && parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    Parameters.Add(parameter);
                }
            }
        }

        private void AddChildren(StatementList statements)
        {
            if (statements != null && statements.Any())
            {
                foreach (var statement in statements)
                {
                    Children.Add(statement);
                }
            }
        }
        private void AddAttribute(AttributeNode node)
        {
            if (node.Key == "id")
            {
                if (node.Value != null && node.Value.Contains("."))
                {
                    var values = node.Value.Split(".".ToCharArray());
                    foreach (var value in values.Skip(1))
                    {
                        Attributes.Add(new AttributeNode("class", value));
                    }

                    Attributes.Add(new AttributeNode(node.Key, values[0]));
                    return;
                }
            }

            Attributes.Add(node);
        }

        public override bool IsTerminal
        {
            get { return false; }
        }

        private IEnumerable<Identifier> GetIdentifierParts(string source)
        {
            char[] splitBy = new[] { '.', '#' };
            int start = 0;
            int index = 0;

            string previousCharacter = "";

            while ((index = source.IndexOfAny(splitBy, start)) != -1)
            {
                index++;
                index = Interlocked.Exchange(ref start, index);

                switch (previousCharacter)
                {
                    case "#":
                        yield return new Identifier
                        {
                            Name = source.Substring(index, start - index - 1),
                            Type = IdentifierType.Id
                        };
                        break;
                    case ".":
                        yield return new Identifier
                        {
                            Name = source.Substring(index, start - index - 1),
                            Type = IdentifierType.Class
                        };
                        break;
                    default:
                        yield return new Identifier
                        {
                            Name = source.Substring(index, start - index - 1),
                            Type = IdentifierType.Literal
                        };
                        break;
                }

                previousCharacter = source.Substring(start - 1, 1);
            }

            if (start < source.Length)
            {
                switch (previousCharacter)
                {
                    case "#":
                        yield return new Identifier
                        {
                            Name = source.Substring(start),
                            Type = IdentifierType.Id
                        };
                        break;
                    case ".":
                        yield return new Identifier
                        {
                            Name = source.Substring(start),
                            Type = IdentifierType.Class
                        };
                        break;
                    default:
                        yield return new Identifier
                        {
                            Name = source.Substring(start),
                            Type = IdentifierType.Literal
                        };
                        break;
                }
            }
        }
    }
}