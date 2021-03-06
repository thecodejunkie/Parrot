﻿// -----------------------------------------------------------------------
// <copyright file="ForeachRenderer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Parrot.Mvc.Renderers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Nodes;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ForeachRenderer : IRenderer
    {
        public string Render(AbstractNode node, object model)
        {
            object localModel = model;
            
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var blockNode = node as BlockNode;
            if (blockNode == null)
            {
                //somehow we're not rendering a blockNode
                throw new InvalidCastException("node");
            }

            //use the passed in parameter property or use the page model
            if (blockNode.Parameters.Any())
            {
                blockNode.Parameters[0].SetModel(model);
                localModel = blockNode.Parameters[0].GetPropertyValue();
            }

            //Assert that we're looping over something
            IEnumerable loop = localModel as IEnumerable;
            if (loop == null)
            {
                throw new InvalidCastException("model is not IEnumerable");
            }

            StringBuilder sb = new StringBuilder();
            foreach (var item in loop)
            {
                sb.Append(blockNode.Children.Render(item));
            }

            return sb.ToString();
        }

        public string Render(AbstractNode node)
        {
            return Render(node, null);
        }
    }
}
