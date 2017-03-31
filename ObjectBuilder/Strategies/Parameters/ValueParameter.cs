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
    /// ʵ�� <see cref="IParameter"/> ֱ�ӳ������ڲ�����ֵ
    /// </summary>
    /// <typeparam name="TValue">����ֵ����</typeparam>
    public class ValueParameter<TValue> : KnownTypeParameter
    {
        private TValue value;

        /// <summary>
        /// ʵ���� <see cref="ValueParameter{T}"/> ��
        /// </summary>
        /// <param name="value">����ֵ</param>
        public ValueParameter(TValue value)
            : base(typeof(TValue))
        {
            this.value = value;
        }

        /// <summary>
        /// ��д<see cref="IParameter.GetValue"/>.
        /// </summary>
        /// <param name="context">���󴴽�����������</param>
        /// <returns>����ֵ</returns>
        public override object GetValue(IBuilderContext context)
        {
            return value;
        }
    }

    /// <summary>
    /// ʵ�� <see cref="KnownTypeParameter"/> ֱ�ӳ������ڲ�����ֵ
    /// </summary>
    public class ValueParameter : KnownTypeParameter
    {
        private object value;

        /// <summary>
        /// ʵ���� <see cref="ValueParameter"/> ��
        /// </summary>
        /// <param name="valueType">����ֵ������</param>
        /// <param name="value">����ֵ</param>
        public ValueParameter(Type valueType, object value)
            : base(valueType)
        {
            this.value = value;
        }

        /// <summary>
        /// ��д<see cref="IParameter.GetValue"/>.
        /// </summary>
        /// <param name="context">���󴴽�����������</param>
        /// <returns>����ֵ</returns>
        public override object GetValue(IBuilderContext context)
        {
            return value;
        }
    }
}