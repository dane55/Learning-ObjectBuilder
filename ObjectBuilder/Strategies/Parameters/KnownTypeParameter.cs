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
    /// ʵ�� <see cref="IParameter"/> �İ����࣬ ������ǰ֪������ֵ������ʱ������ʹ����������ʵ����Щ���Ѿ�Ԥ��֪������ֵ���͵Ĳ����������� 
    /// </summary>
    public abstract class KnownTypeParameter : IParameter
    {
        /// <summary>
        /// ��������
        /// </summary>
        protected Type type;

        /// <summary>
        ///  ʹ�ø�������ʵ���� <see cref="KnownTypeParameter"/> ��
        /// </summary>
        /// <param name="type">��������</param>
        protected KnownTypeParameter(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// ��ȡ����ֵ������
        /// </summary>
        public Type GetParameterType(IBuilderContext context)
        {
            return type;
        }

        /// <summary>
        /// ���󷽷�r <see cref="IParameter.GetValue"/> ������������ʵ��
        /// </summary>
        /// <param name="context">���󴴽�����������</param>
        /// <returns>����ֵ</returns>
        public abstract object GetValue(IBuilderContext context);
    }
}