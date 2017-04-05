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

using System;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ��ʾ�������������������ġ��ṩ�˲������� Locator��
    /// ��ʾ���ɻ�ֽ�������е������ģ��ڶ��󴴽������еĴ��������Ķ��󣬿��ԶԱ�HttpContent�Լ��ܵ�����Content
	/// IBuilderContext���������ڣ��ڸ���Strategy����Strategy.BuildUp����ʱ�������Ǵ�������ʱʹ��Strategies��Policies�ȶ���
    /// �������ݺͱ����Ҫ��Ϣ
    /// </summary>
    public interface IBuilderContext
    {
        /// <summary>
        /// ָ������������ͷ
        /// </summary>
        /// <returns>���еĵ�һ���ԣ��������û�в��ԣ��򷵻�null</returns>
        IBuilderStrategy HeadOfChain { get; }

        /// <summary>
        /// ��ʾһ���������ڵĶ�λ��
        /// </summary>
        IReadWriteLocator Locator { get; }

        /// <summary>
        /// �ṩ������ʹ�õķ����һϵ�еĶ������ԣ����й����������ߣ��� Type/ID �����ߵ�ӳ�伯��
        /// </summary>
        PolicyList Policies { get; }

        /// <summary>
        /// ��ȡ��һ������
        /// </summary>
        /// <param name="currentStrategy">��ǰ�Ĳ��Զ���</param>
        /// <returns>������һ�����ԣ��������null��ô��ǰ����Ĳ��Զ���Ϊ���һ������</returns>
        IBuilderStrategy GetNextInChain(IBuilderStrategy currentStrategy);
    }
}