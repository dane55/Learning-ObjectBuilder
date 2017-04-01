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
using System.Reflection;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// ʵ�� <see cref="IReflectionMemberInfo{T}"/> 
    /// </summary>
    /// <typeparam name="TMemberInfo"></typeparam>
    public class ReflectionMemberInfo<TMemberInfo> : IReflectionMemberInfo<TMemberInfo>
        where TMemberInfo : MethodBase
    {
        private TMemberInfo memberInfo;

        /// <summary>
        /// ʵ���� <see cref="ReflectionMemberInfo{T}"/> ��.
        /// </summary>
        /// <param name="memberInfo">The member used to satisfy the interface calls.</param>
        public ReflectionMemberInfo(TMemberInfo memberInfo)
        {
            this.memberInfo = memberInfo;
        }

        /// <summary>
        /// �� <see cref="IReflectionMemberInfo{T}.MemberInfo"/> �в鿴����
        /// </summary>
        public TMemberInfo MemberInfo
        {
            get { return memberInfo; }
        }

        /// <summary>
        /// �� <see cref="IReflectionMemberInfo{T}.Name"/> �в鿴����
        /// </summary>
        public string Name
        {
            get { return memberInfo.Name; }
        }

        /// <summary>
        /// �� <see cref="IReflectionMemberInfo{T}.GetCustomAttributes"/> �в鿴����
        /// </summary>
        public object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return memberInfo.GetCustomAttributes(attributeType, inherit);
        }

        /// <summary>
        /// �� <see cref="IReflectionMemberInfo{T}.GetParameters"/> �в鿴����
        /// </summary>
        public ParameterInfo[] GetParameters()
        {
            return memberInfo.GetParameters();
        }
    }
}
