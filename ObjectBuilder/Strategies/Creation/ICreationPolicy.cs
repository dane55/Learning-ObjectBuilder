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
    /// �������ԵĹ������ԣ�����һ������Ķ������� <see cref="CreationStrategy"/>.
    /// </summary>
    public interface ICreationPolicy : IBuilderPolicy
    {
        /// <summary>
        /// ѡ�����ڴ�������Ĺ��캯��
        /// </summary>
        /// <param name="context">����ִ��������</param>
        /// <param name="type">��Ҫ�����Ķ��������</param>
        /// <param name="id">��Ҫ�����Ķ����Ψһ���</param>
        /// <returns>Ҫʹ�õĹ��캯��������Ҳ������ʵĹ��캯�����򷵻�null</returns>
        ConstructorInfo SelectConstructor(IBuilderContext context, Type type, string id);

        /// <summary>
        /// ��ȡҪ���ݸ����캯���Ĳ���ֵ
        /// </summary>
        /// <param name="context">����ִ��������</param>
        /// <param name="type">��Ҫ�����Ķ��������</param>
        /// <param name="id">��Ҫ�����Ķ����Ψһ���</param>
        /// <param name="constructor">�����๹�캯�������Բ��ṩ�Թ��캯��Ԫ���ݵķ���Ȩ</param>
        /// <returns>���ݸ����캯���Ĳ�������</returns>
        object[] GetParameters(IBuilderContext context, Type type, string id, ConstructorInfo constructor);
    }
}