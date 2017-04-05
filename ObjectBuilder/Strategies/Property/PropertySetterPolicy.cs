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

using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder
{
	/// <summary>
	/// ʵ�� <see cref="IPropertySetterPolicy"/>.
	/// </summary>
	public class PropertySetterPolicy : IPropertySetterPolicy
	{
		private Dictionary<string, IPropertySetterInfo> properties = new Dictionary<string, IPropertySetterInfo>();

        /// <summary>
        /// ��ȡ�洢��Ҫ���õ����Ժ�ֵ
        /// </summary>
        public Dictionary<string, IPropertySetterInfo> Properties
		{
			get { return properties; }
		}
	}
}
