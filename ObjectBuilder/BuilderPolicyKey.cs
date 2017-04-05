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
    /// ���߷���(��������)��Key�������߽ӿ����͡�����ʵ����ID����
    /// ��ʾ��������������(Pllicy)ע�����������Ϣ�������ɽӿ��������͡�����ʵ��������Ψһ��ʶ
    /// </summary>
    public struct BuilderPolicyKey
    {
        /// <summary>
        /// ��ʼ���ṹ�� <see cref="BuilderPolicyKey"/> �����а�����������(�ӿ�)������ʵ���Ͳ���Ψһ��ʶ
        /// </summary>
        /// <param name="policyType">��������(������IBuilderPolicy�Ľӿ�)</param>
        /// <param name="typePolicyAppliesTo">��������ʵ��(ʵ��������IBuilderPolicy�ĽӿڵĽӿ�)</param>
        /// <param name="idPolicyAppliesTo">��ǰ���Ե�Ψһ��ʶ�������Դ�null.</param>
        public BuilderPolicyKey(Type policyType, Type typePolicyAppliesTo, string idPolicyAppliesTo)
        {
            PolicyType = policyType;
            BuildType = typePolicyAppliesTo;
            BuildID = idPolicyAppliesTo;
        }

        private Type PolicyType;
        private Type BuildType;
        private string BuildID;
    }
}