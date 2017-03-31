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
    /// ObjectBuilder����������Ե�������
    /// </summary>
    public interface IBuilderStrategyChain
    {
        /// <summary>
        /// �����������еĵ�һ���ԣ��������û�в��ԣ��򷵻�null
        /// </summary>
        IBuilderStrategy Head { get; }

        /// <summary>
        /// ��Ӳ���
        /// </summary>
        /// <param name="strategy">���Զ���.</param>
        void Add(IBuilderStrategy strategy);

        /// <summary>
        /// ������Ӳ���
        /// </summary>
        /// <param name="strategies">���Զ��󼯺�</param>
        void AddRange(IEnumerable strategies);

        /// <summary>
        /// ȡ���е���һ�����ԣ�����ڸ����Ĳ���
        /// </summary>
        /// <param name="currentStrategy">��ǰִ�еĲ���</param>
        /// <returns>������һ�����ԣ��������null��ô��ǰ����Ĳ��Զ���Ϊ���һ������</returns>
        IBuilderStrategy GetNext(IBuilderStrategy currentStrategy);
    }
}