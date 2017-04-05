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
    /// ��װ����������
    /// </summary>
    public interface IPropertySetterInfo
	{
        /// <summary>
        /// ��ȡ���ø����Ե�ֵ
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="type">The type being built.</param>
        /// <param name="id">The ID being built.</param>
        /// <param name="propInfo">The property being set.</param>
        /// <returns>Ҫ����Ϊ���Ե�ֵ</returns>
        object GetValue(IBuilderContext context, Type type, string id, PropertyInfo propInfo);

        /// <summary>
        /// ��ȡ���õ�������Ϣ
        /// </summary>
        /// <param name="context">����������</param>
        /// <param name="type">The type being built.</param>
        /// <param name="id">The ID being built.</param>
        /// <returns>Ҫ���õ����ԣ�����Ҳ������ԣ��򷵻�null</returns>
        PropertyInfo SelectProperty(IBuilderContext context, Type type, string id);
	}
}
