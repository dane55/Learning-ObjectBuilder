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
	/// ��ʾ���ڹ��캯���ͷ������õĵ����������Լ��������ã�����ӿ������ڴ�����������Ҫ���ݵĲ�����������ע��ģʽ�У���һ����ͨ�Ĺ��򣬾�����Ҫ�в�����ע�룬
	/// Constructor Injection��͸�����캯������ע�룬
	/// ��Interface Injection����͸����������ע�룬
	/// Setter Injection����͸������ע�룬��˲�����������ע��ģʽ�����õ��Ĺ������ObjectBuilder������IParameter�ӿڣ����ṩһ��ʵ�ִ˽ӿڵĲ���������ע��ʱ������Щ����������ȡ�ò���ֵ
    /// </summary>
    public interface IParameter
    {
        /// <summary>
        /// ��ȡ����ֵ������
        /// </summary>
        /// <param name="context">���󴴽�����������</param>
        /// <returns>��������</returns>
        Type GetParameterType(IBuilderContext context);

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="context">���󴴽�����������</param>
        /// <returns>����ֵ</returns>
        object GetValue(IBuilderContext context);
    }
}