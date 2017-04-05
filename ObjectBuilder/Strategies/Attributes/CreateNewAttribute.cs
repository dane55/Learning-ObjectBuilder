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
    /// ����һ�����������½�ʵ��
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class CreateNewAttribute : ParameterAttribute
    {
        /// <summary>
        /// ����һ���������ڸ��� <see cref="IBuilderPolicy"/> ʵ�ֿ��Դ��� <see cref="IParameter"/>
        /// </summary>
        /// <param name="annotatedMemberType">��Ա�����ͣ������Ի��캯������</param>
        /// <returns>�������ֵ�Ĳ���ʵ��</returns>
        public override IParameter CreateParameter(Type annotatedMemberType)
        {
            return new CreationParameter(annotatedMemberType, Guid.NewGuid().ToString());
        }
    }
}
