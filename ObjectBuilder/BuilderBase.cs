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
using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʵ��IBuilder�ӿڵĸ�����
    /// </summary>
    /// <typeparam name="TStageEnum">���ö�ٵķ��ͱ�ʾ���ʹ�������</typeparam>
    public class BuilderBase<TStageEnum> : IBuilder<TStageEnum>
    {
        /// <summary>
        /// ��������ʱ����Ҫ��һϵ��Χ�ƴ�������ʱ����Ҫ�ĸ��Ӳ�����Ϣ�����е����߷���
        /// </summary>
        private PolicyList policies = new PolicyList();
        /// <summary>
        /// ���Լ��ϣ����ڴ洢���󴴽�ʱ����Ҫ��һϵ�в���
        /// </summary>
        private StrategyList<TStageEnum> strategies = new StrategyList<TStageEnum>();
        /// <summary>
        /// �洢���������ϣ����洢���������ӳ��ԡ�
        /// </summary>
        private Dictionary<object, object> lockObjects = new Dictionary<object, object>();

        /// <summary>
        ///ʵ����һ�� <see cref="BuilderBase{T}"/> ��.
        /// </summary>
        public BuilderBase()
        {
        }

        /// <summary>
        /// ͨ��<see cref="IBuilderConfigurator{BuilderStage}"/>����ʵ����һ�� <see cref="BuilderBase{T}"/> ��.
        /// </summary>
        /// <param name="configurator">���������ö���ӿ�</param>
        public BuilderBase(IBuilderConfigurator<TStageEnum> configurator)
        {
            configurator.ApplyConfiguration(this);
        }

        /// <summary>
        /// �� <see cref="IBuilder{TStageEnum}.Policies"/> �в鿴������Ϣ
        /// </summary>
        public PolicyList Policies
        {
            get { return policies; }
        }

        /// <summary>
        /// �� <see cref="IBuilder{TStageEnum}.Strategies"/> �в鿴������Ϣ
        /// </summary>
        public StrategyList<TStageEnum> Strategies
        {
            get { return strategies; }
        }

        /// <summary>
        /// �� <see cref="IBuilder{TStageEnum}.BuildUp{T}"/> �в鿴������Ϣ
        /// </summary>
        public TTypeToBuild BuildUp<TTypeToBuild>(IReadWriteLocator locator, string idToBuild, object existing, params PolicyList[] transientPolicies)
        {
            return (TTypeToBuild)BuildUp(locator, typeof(TTypeToBuild), idToBuild, existing, transientPolicies);
        }

        /// <summary>
        /// �� <see cref="IBuilder{TStageEnum}.BuildUp"/> �в鿴������Ϣ
        /// </summary>
        public virtual object BuildUp(IReadWriteLocator locator, Type typeToBuild, string idToBuild, object existing, params PolicyList[] transientPolicies)
        {
            if (locator != null)
            {
                //��ȡ�洢��������������
                lock (GetLock(locator))
                {
                    return DoBuildUp(locator, typeToBuild, idToBuild, existing, transientPolicies);
                }
            }
            else
            {
                return DoBuildUp(locator, typeToBuild, idToBuild, existing, transientPolicies);
            }

        }
        //ִ�й�������ȡ���������������������ģ����ò�����ͷ��������
        private object DoBuildUp(IReadWriteLocator locator, Type typeToBuild, string idToBuild, object existing, PolicyList[] transientPolicies)
        {
            ////�������б�����һ������������
            IBuilderStrategyChain chain = strategies.MakeStrategyChain();  //��ȡ�����������еĲ��Ժ�
            //�������������Ƿ�ɹ���
            ThrowIfNoStrategiesInChain(chain); //��⵱ǰ�������������Ƿ�������ԣ����������׳�һ���쳣
            //�������������ġ�����ʱ�����빹�����������ϡ�
            IBuilderContext context = MakeContext(chain, locator, transientPolicies);

            IBuilderTracePolicy trace = context.Policies.Get<IBuilderTracePolicy>(null, null);

            if (trace != null) trace.Trace(Properties.Resources.BuildUpStarting, typeToBuild, idToBuild ?? "(null)");
            //��ȡһ�����Լ����е�����Ϊ0�Ĳ���ִ�У�ȡ��һ�����Խ���ִ��
            object result = null;
            //��ʼ�������󣬴Ӵ˴���ʼ�Ὣ�����������еĲ��Զ�ִ��һ�飬���ǵ�������
            if (chain.Head != null) //Ĭ���ǲ������HeadΪnull���������Ϊ��ʼ��ʱ��ܻ��ʼ��һЩĬ�ϵĲ���
            {
                result = chain.Head.BuildUp(context, typeToBuild, existing, idToBuild);  //ִ�ж��󴴽���
            }
            if (trace != null) trace.Trace(Properties.Resources.BuildUpFinished, typeToBuild, idToBuild ?? "(null)");

            return result;
        }

        /// <summary>
        /// ObjectBuilder���󴴽������ģ��������Ϊÿһ������ִ��ʱ��һ����������Ϣ��ͨ����������Ŀ����ڲ����������д���ִ��
        /// </summary>
        /// <param name="chain">������������Ĭ�ϵĲ��Ի��Զ���Ĳ���</param>
        /// <param name="locator">���ɶ���Ķ�λ�����������Ѵ���ʱֱ���ڶ�λ���л�ȡ</param>
        /// <param name="transientPolicies">��ǰ���ӵĶ�������</param>
        /// <returns></returns>
        private IBuilderContext MakeContext(IBuilderStrategyChain chain, IReadWriteLocator locator, params PolicyList[] transientPolicies)
        {
            PolicyList policies = new PolicyList(this.policies); //����ǰ����ʱ������ӵ����Լ����У����������Ĭ�϶������Ժ���ʱ��������
            foreach (PolicyList policyList in transientPolicies)
            {
                policies.AddPolicies(policyList);
            }
            return new BuilderContext(chain, locator, policies);
        }

        private static void ThrowIfNoStrategiesInChain(IBuilderStrategyChain chain)
        {
            if (chain.Head == null)
                throw new InvalidOperationException(Properties.Resources.BuilderHasNoStrategies);
        }

        /// <summary>
        /// �� <see cref="IBuilder{TStageEnum}.TearDown{T}"/> �в鿴������Ϣ
        /// </summary>
        public TItem TearDown<TItem>(IReadWriteLocator locator, TItem item)
        {
            if (typeof(TItem).IsValueType == false && item == null)
                throw new ArgumentNullException("item");

            if (locator != null)
            {
                lock (GetLock(locator))
                {
                    return DoTearDown<TItem>(locator, item);
                }
            }
            else
            {
                return DoTearDown<TItem>(locator, item);
            }
        }

        private TItem DoTearDown<TItem>(IReadWriteLocator locator, TItem item)
        {
            //������ת��������
            IBuilderStrategyChain chain = strategies.MakeReverseStrategyChain();
            ThrowIfNoStrategiesInChain(chain);

            Type type = item.GetType();
            IBuilderContext context = MakeContext(chain, locator);
            IBuilderTracePolicy trace = context.Policies.Get<IBuilderTracePolicy>(null, null);

            if (trace != null)
                trace.Trace(Properties.Resources.TearDownStarting, type);
            //ִ������
            TItem result = (TItem)chain.Head.TearDown(context, item);

            if (trace != null)
                trace.Trace(Properties.Resources.TearDownFinished, type);

            return result;
        }
        //��ȡ�洢��������
        private object GetLock(object locator)
        {
            lock (lockObjects)
            {
                if (lockObjects.ContainsKey(locator))
                    return lockObjects[locator];

                object newLock = new object();
                lockObjects[locator] = newLock;
                return newLock;
            }
        }
    }
}
