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
    /// 表示生成器策略注册所必需的信息。策略由接口策略类型、策略实例和策略唯一标识
    /// </summary>
    public struct BuilderPolicyKey
    {
        /// <summary>
        /// 初始化结构体 <see cref="BuilderPolicyKey"/> ，其中包括策略类型(接口)、策略实例和策略唯一标识
        /// </summary>
        /// <param name="policyType">策略类型(派生自IBuilderPolicy的接口)</param>
        /// <param name="typePolicyAppliesTo">策略类型实例(实现派生自IBuilderPolicy的接口的接口)</param>
        /// <param name="idPolicyAppliesTo">当前策略的唯一标识符，可以传null.</param>
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