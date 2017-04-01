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
    /// <see cref="IReflectionMemberInfo{TMemberInfo}"/> ���͵�ö�ټ���
    /// </summary>
    public interface IReflectionMemberInfo<TMemberInfo>
    {
        /// <summary>
        /// ԭ��Ա��Ϣ����
        /// </summary>
        TMemberInfo MemberInfo { get; }

        /// <summary>
        /// ��Ա����
        /// </summary>
        string Name { get; }

        /// <summary>
        /// ��Ա���Զ�������
        /// </summary>
        /// <returns></returns>
        object[] GetCustomAttributes(Type attributeType, bool inherit);

        /// <summary>
        /// ���ݸ���Ա�Ĳ���
        /// </summary>
        /// <returns></returns>
        ParameterInfo[] GetParameters();
    }
}
