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
    /// һ�����Ե��Զ��弯�����ͣ������б��ɲ���ִ�н׶κͲ����б����
    /// </summary>
    /// <typeparam name="TStageEnum">ObjectBuilder��������ʵ���Ĳ��Ե�ִ��˳��</typeparam>
    public class StrategyList<TStageEnum>
    {
        /// <summary>
        /// ObjectBuilder��������ʵ���Ĳ��Ե�ִ��˳�򣬴洢ִ��˳��
        /// </summary>
		private readonly static Array stageValues = Enum.GetValues(typeof(TStageEnum));
        /// <summary>
        /// ObjectBuilder��������ʵ���Ĳ��Ե�ִ��˳��ļ�ֵ�Լ��ϣ�ÿһ��ִ�в���ִ��˳���а������һ������
        /// </summary>
		private Dictionary<TStageEnum, List<IBuilderStrategy>> stages;
        /// <summary>
        /// ͬ����
        /// </summary>
		private object lockObject = new object();

        /// <summary>
        /// ʵ���� <see cref="StrategyList{T}"/> ��
        /// </summary>
        public StrategyList()
        {
            stages = new Dictionary<TStageEnum, List<IBuilderStrategy>>();
            foreach (TStageEnum stage in stageValues)
            {
                stages[stage] = new List<IBuilderStrategy>();
            }
        }

        /// <summary>
        /// ���һ������
        /// </summary>
        /// <param name="strategy">һ�����ԵĶ���ʵ��</param>
        /// <param name="stage">ObjectBuilder��������ʵ���Ĳ��Ե�ִ��˳��</param>
        public void Add(IBuilderStrategy strategy, TStageEnum stage)
        {
            lock (lockObject)
            {
                stages[stage].Add(strategy);
            }
        }

        /// <summary>
        /// �����²��Բ�������ӵ��б���
        /// </summary>
        /// <typeparam name="TStrategy">Ҫ�����Ĳ������͡�������һ���޲����Ĺ��캯��</typeparam>
        /// <param name="stage">ObjectBuilder��������ʵ���Ĳ��Ե�ִ��˳��</param>
        public void AddNew<TStrategy>(TStageEnum stage)
            where TStrategy : IBuilderStrategy, new()
        {
            lock (lockObject)
            {
                stages[stage].Add(new TStrategy());
            }
        }

        /// <summary>
        /// ���һ�������б�
        /// </summary>
        public void Clear()
        {
            lock (lockObject)
            {
                foreach (TStageEnum stage in stageValues)
                {
                    stages[stage].Clear();
                }
            }
        }

        /// <summary>
        /// �����б��еĲ��Դ���������������������ٲ��Բ��������в����ڽ����������򣩣������������
        /// </summary>
        /// <returns>�µĲ���������</returns>
        public IBuilderStrategyChain MakeReverseStrategyChain()
        {
            lock (lockObject)
            {
                List<IBuilderStrategy> tempList = new List<IBuilderStrategy>();
                foreach (TStageEnum stage in stageValues)
                {
                    tempList.AddRange(stages[stage]);
                }
                tempList.Reverse();
                BuilderStrategyChain result = new BuilderStrategyChain();
                result.AddRange(tempList);
                return result;
            }
        }

        /// <summary>
        /// �����嵥�еĲ��Դ���ս���������ڹ�������
        /// </summary>
        /// <returns>�µĲ���������</returns>
        public IBuilderStrategyChain MakeStrategyChain()
        {
            lock (lockObject)
            {
                BuilderStrategyChain result = new BuilderStrategyChain();
                foreach (TStageEnum stage in stageValues)
                {
                    result.AddRange(stages[stage]);
                }
                return result;
            }
        }
    }
}
