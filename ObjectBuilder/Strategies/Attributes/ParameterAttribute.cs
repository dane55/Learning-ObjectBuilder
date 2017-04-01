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
    /// ���Ա�ʾ������ע��ĳ�Ա����ֵ�ڱ���ʱ���� <see cref="IParameter"/> �������� <see cref="ParameterAttribute.CreateParameter"/> ��������.
    /// </summary>
    public abstract class ParameterAttribute : Attribute
    {
        /// <summary>
        /// ʵ���� <see cref="ParameterAttribute"/> ��
        /// </summary>
        protected ParameterAttribute() { }

        /// <summary>
        /// ����һ���������ڸ��� <see cref="IBuilderPolicy"/> ʵ�ֿ��Դ��� <see cref="IParameter"/>
        /// </summary>
        /// <param name="memberType">��Ա�����ͣ������Ի��캯������</param>
        /// <returns>�������ֵ�Ĳ���ʵ��</returns>
        public abstract IParameter CreateParameter(Type memberType);
    }
}
