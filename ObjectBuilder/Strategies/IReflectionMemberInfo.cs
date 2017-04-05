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
    /// <see cref="IReflectionMemberInfo{TInfo}"/> ���͵�ö�ټ���
    /// </summary>
    public interface IReflectionMemberInfo<TInfo>
    {
        /// <summary>
        /// ��Ա����Ϣ���� MethodInfo�� ContructorInfo ��
        /// </summary>
        TInfo MemberInfo { get; }

        /// <summary>
        /// ��Ա����
        /// </summary>
        string Name { get; }

        /// <summary>
        /// �Կ�ܵ��������Է����ķ�װ ��������Աָ�����Զ������ԣ�������������дʱ�������� <see cref="Type"/> ��ʶ���Զ������Ե����顣
        /// </summary>
        /// <param name="attributeType">Ҫ�������������͡�ֻ���ؿɷ���������͵�����</param>
        /// <param name="inherit">ָ���Ƿ������ó�Ա�ļ̳����Բ�����Щ����</param>
        /// <returns></returns>
        object[] GetCustomAttributes(Type attributeType, bool inherit);

        /// <summary>
        /// ��ȡ���ݸ���Ա�Ĳ���
        /// </summary>
        /// <returns></returns>
        ParameterInfo[] GetParameters();
    }
}
