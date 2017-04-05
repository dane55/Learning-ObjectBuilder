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
    /// �Զ��� <see cref="IBuilderPolicy"/> ���϶������ڴ洢ObjectBuilder���󴴽�����ж������ԣ�
    /// �����б�����������������������ɣ������������������Ͱ�������ʵ���������Լ�ID��
    /// </summary>
    public class PolicyList
    {
        private Dictionary<BuilderPolicyKey, IBuilderPolicy> policies = new Dictionary<BuilderPolicyKey, IBuilderPolicy>();
        private object lockObject = new object();

        /// <summary>
        /// �����µ� <see cref="PolicyList"/> ��ʵ��
        /// </summary>
        /// <param name="policiesToCopy">Ҫ���Ƶ������б��еĲ���</param>
        public PolicyList(params PolicyList[] policiesToCopy)
        {
            if (policiesToCopy != null)
                foreach (PolicyList policyList in policiesToCopy)
                {
                    AddPolicies(policyList);
                }
        }

        /// <summary>
        /// �����б��еĲ�������
        /// </summary>
        public int Count
        {
            get
            {
                lock (lockObject)
                {
                    return policies.Count;
                }
            }
        }

        /// <summary>
        /// ������������ӵ������б��С����б��е��κβ��Խ������Ѵ����ڲ����б��еĲ��ԡ�
        /// </summary>
        /// <param name="policiesToCopy">Ҫ���Ƶ������б��еĲ���</param>
        public void AddPolicies(PolicyList policiesToCopy)
        {
            lock (lockObject)
            {
                if (policiesToCopy != null)
                    foreach (KeyValuePair<BuilderPolicyKey, IBuilderPolicy> kvp in policiesToCopy.policies)
                    {
                        policies[kvp.Key] = kvp.Value;
                    }
            }
        }

        /// <summary>
        /// �Ƴ���������
        /// </summary>
        /// <typeparam name="TPolicyInterface">��������(������IBuilderPolicy�Ľӿ�)</typeparam>
        /// <param name="typePolicyAppliesTo"></param>
        /// <param name="idPolicyAppliesTo">��ǰ���Ե�Ψһ��ʶ�������Դ�null</param>
        public void Clear<TPolicyInterface>(Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            Clear(typeof(TPolicyInterface), typePolicyAppliesTo, idPolicyAppliesTo);
        }

        /// <summary>
        /// �Ƴ���������
        /// </summary>
        /// <param name="policyInterface">��������(������IBuilderPolicy�Ľӿ�)</param>
        /// <param name="typePolicyAppliesTo">��ҪObjectBuilder�����Ķ��������</param>
        /// <param name="idPolicyAppliesTo">��ǰ���Ե�Ψһ��ʶ�������Դ�null</param>
        public void Clear(Type policyInterface, Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            lock (lockObject)
            {
                policies.Remove(new BuilderPolicyKey(policyInterface, typePolicyAppliesTo, idPolicyAppliesTo));
            }
        }

        /// <summary>
        /// �Ƴ����в���
        /// </summary>
        public void ClearAll()
        {
            lock (lockObject)
            {
                policies.Clear();
            }
        }

        /// <summary>
        /// �Ƴ�һ��Ĭ�ϲ���
        /// </summary>
        /// <typeparam name="TPolicyInterface">��ע����Ե�����</typeparam>
        public void ClearDefault<TPolicyInterface>()
        {
            ClearDefault(typeof(TPolicyInterface));
        }

        /// <summary>
        /// �Ƴ�һ��Ĭ�ϲ���
        /// </summary>
        /// <param name="policyInterface">��ע����Ե�����</param>
        public void ClearDefault(Type policyInterface)
        {
            Clear(policyInterface, null, null);
        }

        /// <summary>
        /// ��ȡһ����������
        /// </summary>
        /// <typeparam name="TPolicyInterface">��������(������IBuilderPolicy�Ľӿ�)</typeparam>
        /// <param name="typePolicyAppliesTo">��ҪObjectBuilder�����Ķ��������</param>
        /// <param name="idPolicyAppliesTo">��ǰ���Ե�Ψһ��ʶ�������Դ�null.</param>
        /// <returns>�ö����������б��У�������ڷ��ظò��ԣ����򷵻�null</returns>
        public TPolicyInterface Get<TPolicyInterface>(Type typePolicyAppliesTo, string idPolicyAppliesTo)
            where TPolicyInterface : IBuilderPolicy
        {
            return (TPolicyInterface)Get(typeof(TPolicyInterface), typePolicyAppliesTo, idPolicyAppliesTo);
        }

        /// <summary>
        /// ��ȡһ����������
        /// </summary>
        /// <param name="policyInterface">��������(������IBuilderPolicy�Ľӿ�)</param>
        /// <param name="typePolicyAppliesTo">��ҪObjectBuilder�����Ķ��������</param>
        /// <param name="idPolicyAppliesTo">��ǰ���Ե�Ψһ��ʶ�������Դ�null.</param>
        /// <returns>�ö����������б��У�������ڷ��ظò��ԣ����򷵻�null</returns>
        public IBuilderPolicy Get(Type policyInterface, Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            BuilderPolicyKey key = new BuilderPolicyKey(policyInterface, typePolicyAppliesTo, idPolicyAppliesTo);
            lock (lockObject)
            {
                IBuilderPolicy policy;
                if (policies.TryGetValue(key, out policy))
                {
                    return policy;
                }
                BuilderPolicyKey defaultKey = new BuilderPolicyKey(policyInterface, null, null);
                if (policies.TryGetValue(defaultKey, out policy))
                {
                    return policy;
                }
                return null;
            }
        }

        /// <summary>
        /// ����һ����������
        /// </summary>
        /// <typeparam name="TPolicyInterface">��������(������IBuilderPolicy�Ľӿ�)</typeparam>
        /// <param name="policy">��������ʵ��(ʵ��������IBuilderPolicy�ĽӿڵĽӿ�)</param>
        /// <param name="typePolicyAppliesTo">��ҪObjectBuilder�����Ķ��������</param>
        /// <param name="idPolicyAppliesTo">��ǰ���Ե�Ψһ��ʶ�������Դ�null.</param>
        public void Set<TPolicyInterface>(TPolicyInterface policy, Type typePolicyAppliesTo, string idPolicyAppliesTo)
            where TPolicyInterface : IBuilderPolicy
        {
            Set(typeof(TPolicyInterface), policy, typePolicyAppliesTo, idPolicyAppliesTo);
        }

        /// <summary>
        ///  ����һ����������
        /// </summary>
        /// <param name="policyInterface">��������(������IBuilderPolicy�Ľӿ�)</param>
        /// <param name="policy">��������ʵ��(ʵ��������IBuilderPolicy�ĽӿڵĽӿ�)</param>
        /// <param name="typePolicyAppliesTo">��ҪObjectBuilder�����Ķ��������</param>
        /// <param name="idPolicyAppliesTo">��ǰ���Ե�Ψһ��ʶ�������Դ�null.</param>
        public void Set(Type policyInterface, IBuilderPolicy policy, Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            BuilderPolicyKey key = new BuilderPolicyKey(policyInterface, typePolicyAppliesTo, idPolicyAppliesTo);
            lock (lockObject)
            {
                policies[key] = policy;
            }
        }

        /// <summary>
        /// ����Ĭ�ϲ��ԡ�������ʱ�����û���ض��ĸ��˲��Կ��ã���ʹ��Ĭ��ֵ
        /// </summary>
        /// <typeparam name="TPolicyInterface">��������(������IBuilderPolicy�Ľӿ�)</typeparam>
        /// <param name="policy">��������ʵ��(ʵ��������IBuilderPolicy�ĽӿڵĽӿ�)</param>
        public void SetDefault<TPolicyInterface>(TPolicyInterface policy)
            where TPolicyInterface : IBuilderPolicy
        {
            SetDefault(typeof(TPolicyInterface), policy);
        }

        /// <summary>
        /// ����Ĭ�ϲ��ԡ�������ʱ�����û���ض��ĸ��˲��Կ��ã���ʹ��Ĭ��ֵ
        /// </summary>
        /// <param name="policyInterface">��������(������IBuilderPolicy�Ľӿ�).</param>
        /// <param name="policy">��������ʵ��(ʵ��������IBuilderPolicy�ĽӿڵĽӿ�)</param>
        public void SetDefault(Type policyInterface, IBuilderPolicy policy)
        {
            Set(policyInterface, policy, null, null);
        }
    }
}