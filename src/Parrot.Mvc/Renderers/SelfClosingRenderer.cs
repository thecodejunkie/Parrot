using System;
using System.Collections.Generic;
using Parrot.Nodes;

namespace Parrot.Mvc.Renderers
{
    public class SelfClosingRenderer : HtmlRenderer
    {
        public override string Render(AbstractNode node, object model)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var blockNode = node as BlockNode;
            if (blockNode == null)
            {
                throw new ArgumentException("node");
            }

            var tag = CreateTag(model, blockNode);

            return tag.ToString(TagRenderMode.SelfClosing);
        }
    }
}