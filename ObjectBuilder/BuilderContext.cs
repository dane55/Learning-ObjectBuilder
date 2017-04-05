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
    /// ʵ�� <see cref="IBuilderContext"/>���࣬<see cref="BuilderContext"/> �࣬Ĭ�ϵĹ��������ġ��洢�˲��������洢�������ߣ���Ϊ�������̵���Ϣ���壬��������һ����������һ������ʹ�õķ�����Լ�һ���������洴������Ķ�λ��
    /// </summary>
    public class BuilderContext : IBuilderContext
    {
        /// <summary>
        /// ����������
        /// </summary>
        private IBuilderStrategyChain chain;
        /// <summary>
        /// ���ɶ���Ķ�λ�����������Ѵ���ʱֱ���ڶ�λ���л�ȡ
        /// </summary>
        private IReadWriteLocator locator;
        /// <summary>
        /// ����ǰ����ʱ������ӵ����Լ����У����������Ĭ�϶������Ժ���ʱ��������
        /// </summary>
        private PolicyList policies;

        /// <summary>
        /// ��ֹʹ��Ĭ�Ϲ��캯����ʼ����ʵ����<see cref="BuilderContext"/> ��
        /// </summary>
        protected BuilderContext()
        {
        }

        /// <summary>
        /// ʵ���� <see cref="BuilderContext"/> ��
        /// </summary>
        /// <param name="chain">������������Ĭ�ϵĲ��Ի��Զ���Ĳ���</param>
        /// <param name="locator">���ɶ���Ķ�λ�����������Ѵ���ʱֱ���ڶ�λ���л�ȡ</param>
        /// <param name="policies">�������Լ��ϣ����������Ĭ�϶������Ժ���ʱ��������</param>
        public BuilderContext(IBuilderStrategyChain chain, IReadWriteLocator locator, PolicyList policies)
        {
            this.chain = chain;
            this.locator = locator;
            this.policies = new PolicyList(policies);
        }

        /// <summary>
        /// �� <see cref="IBuilderContext.HeadOfChain"/> �в鿴����
        /// </summary>
        public IBuilderStrategy HeadOfChain
        {
            get { return chain.Head; }
        }

        /// <summary>
        /// �� <see cref="IBuilderContext.Locator"/> �в鿴����
        /// </summary>
        public IReadWriteLocator Locator
        {
            get { return locator; }
        }



        /// <summary>
        /// �� <see cref="IBuilderContext.Policies"/> �в鿴����
        /// </summary>
        public PolicyList Policies
        {
            get { return policies; }
        }

        /// <summary>
        /// �������ɶ���Ķ�λ�����������Ѵ���ʱֱ���ڶ�λ���л�ȡ
        /// </summary>
        protected void SetLocator(IReadWriteLocator locator)
        {
            this.locator = locator;
        }

        /// <summary>
        /// ���ö������Լ��ϣ����������Ĭ�϶������Ժ���ʱ��������
        /// </summary>
        protected void SetPolicies(PolicyList policies)
        {
            this.policies = policies;
        }

        /// <summary>
        /// ����������
        /// </summary>
        protected IBuilderStrategyChain StrategyChain
        {
            get { return chain; }
            set { chain = value; }
        }

        /// <summary>
        /// �� <see cref="IBuilderContext.GetNextInChain"/> �в鿴����
        /// �����������е���һ����Ŀ������ڼ������в�����
        /// </summary>
        public IBuilderStrategy GetNextInChain(IBuilderStrategy currentStrategy)
        {
            return chain.GetNext(currentStrategy);
        }
    }
}
