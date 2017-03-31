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
    /// ��ʾ���ڹ��캯���ͷ������õĵ����������Լ��������ã�����ӿ������ڴ�����������Ҫ���ݵĲ���
    /// </summary>
    public interface IParameter
	{
        /// <summary>
        /// ��ȡ����ֵ������
        /// </summary>
        /// <param name="context">The build context.</param>
        /// <returns>The parameter's type.</returns>
        Type GetParameterType(IBuilderContext context);

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="context">The build context.</param>
        /// <returns>The parameter's value.</returns>
        object GetValue(IBuilderContext context);
	}
}