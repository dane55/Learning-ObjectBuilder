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
    /// ʵ�� <see cref="IParameter"/> ��һ�����Ŀ�¡�Ĳ���
    /// </summary>
    public class CloneParameter : IParameter
	{
		private IParameter param;

        /// <summary>
        /// ʵ���� <see cref="CloneParameter"/> ��
        /// </summary>
        /// <param name="param">Ҫ��¡�Ĳ���.</param>
        public CloneParameter(IParameter param)
		{
			this.param = param;
		}

		/// <summary>
		/// �� <see cref="IParameter.GetParameterType"/> �в鿴����
		/// </summary>
		public Type GetParameterType(IBuilderContext context)
		{
			return param.GetParameterType(context);
		}

        /// <summary>
        /// �� <see cref="IParameter.GetValue"/> �в鿴����
        /// </summary>
        public object GetValue(IBuilderContext context)
		{
			object val = param.GetValue(context);

			if (val is ICloneable)
				val = ((ICloneable)val).Clone();

			return val;
		}
	}
}