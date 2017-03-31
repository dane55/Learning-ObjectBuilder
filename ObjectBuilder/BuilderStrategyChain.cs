//===============================================================================
// Microsoft patterns & practices
// ObjectBuilder Application Block
//===============================================================================
// Copyright ?Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    ///ʵ��<see cref="IBuilderStrategyChain"/>�ӿ�
    /// </summary>
    public class BuilderStrategyChain : IBuilderStrategyChain
    {
        private List<IBuilderStrategy> strategies;

        /// <summary>
        /// ʵ�������������� <see cref="BuilderStrategyChain"/> ��.
        /// </summary>
        public BuilderStrategyChain()
        {
            strategies = new List<IBuilderStrategy>();
        }

        /// <summary>
        /// �� <see cref="IBuilderStrategyChain.Head"/> �в鿴����
        /// </summary>
        public IBuilderStrategy Head
        {
            get
            {
                if (strategies.Count > 0)
                    return strategies[0];
                else
                    return null;
            }
        }

        /// <summary>
        /// �� <see cref="IBuilderStrategyChain.Add"/> �в鿴����
        /// </summary>
        public void Add(IBuilderStrategy strategy)
        {
            strategies.Add(strategy);
        }

        /// <summary>
        /// �� <see cref="IBuilderStrategyChain.AddRange"/>�в鿴����
        /// </summary>
        public void AddRange(IEnumerable strategies)
        {
            foreach (IBuilderStrategy strategy in strategies)
                Add(strategy);
        }

        /// <summary>
        /// �� <see cref="IBuilderStrategyChain.GetNext"/> �в鿴����
        /// </summary>
        public IBuilderStrategy GetNext(IBuilderStrategy currentStrategy)
        {
            for (int idx = 0; idx < strategies.Count - 1; idx++)
                if (ReferenceEquals(currentStrategy, strategies[idx])) //����Ƿ�����ͬ�Ķ���ʵ��
                    return strategies[idx + 1];

            return null;
        }
    }
}
