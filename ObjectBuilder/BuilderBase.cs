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
        /// ��������ʱ����Ҫ��һϵ��Χ�ƴ�������ʱ����Ҫ�ĸ��Ӳ�����Ϣ
        /// </summary>
        private PolicyList policies = new PolicyList();
        /// <summary>
        /// ���Լ��ϣ����ڴ洢���󴴽�ʱ����Ҫ��һϵ�в���
        /// </summary>
        private StrategyList<TStageEnum> strategies = new StrategyList<TStageEnum>();
        /// <summary>
        /// 
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

        private object DoBuildUp(IReadWriteLocator locator, Type typeToBuild, string idToBuild, object existing, PolicyList[] transientPolicies)
        {
            IBuilderStrategyChain chain = strategies.MakeStrategyChain();
            ThrowIfNoStrategiesInChain(chain);

            IBuilderContext context = MakeContext(chain, locator, transientPolicies);
            IBuilderTracePolicy trace = context.Policies.Get<IBuilderTracePolicy>(null, null);

            if (trace != null)
                trace.Trace(Properties.Resources.BuildUpStarting, typeToBuild, idToBuild ?? "(null)");

            object result = chain.Head.BuildUp(context, typeToBuild, existing, idToBuild);

            if (trace != null)
                trace.Trace(Properties.Resources.BuildUpFinished, typeToBuild, idToBuild ?? "(null)");

            return result;
        }

        private IBuilderContext MakeContext(IBuilderStrategyChain chain, IReadWriteLocator locator, params PolicyList[] transientPolicies)
        {
            PolicyList policies = new PolicyList(this.policies);

            foreach (PolicyList policyList in transientPolicies)
                policies.AddPolicies(policyList);

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
            IBuilderStrategyChain chain = strategies.MakeReverseStrategyChain();
            ThrowIfNoStrategiesInChain(chain);

            Type type = item.GetType();
            IBuilderContext context = MakeContext(chain, locator);
            IBuilderTracePolicy trace = context.Policies.Get<IBuilderTracePolicy>(null, null);

            if (trace != null)
                trace.Trace(Properties.Resources.TearDownStarting, type);

            TItem result = (TItem)chain.Head.TearDown(context, item);

            if (trace != null)
                trace.Trace(Properties.Resources.TearDownFinished, type);

            return result;
        }

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
