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
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.ObjectBuilder
{
    /// <summary>
    /// �κ�ʵ����IBuiderAware�ӿڵĶ���������׶λ�õ�һ��OnBuilltUp���¼�֪ͨ��ͬʱ�ڶ���ж�ص�ʱ���õ�OnTearingDown��֪ͨ������֪ͨ�¼�����BuilderAwareStrategy�Ĺ���
    /// </summary>
    public interface IBuilderAware
	{
		/// <summary>
		/// Called by the <see cref="BuilderAwareStrategy"/> when the object is being built up.
		/// </summary>
		/// <param name="id">The ID of the object that was just built up.</param>
		void OnBuiltUp(string id);

		/// <summary>
		/// Called by the <see cref="BuilderAwareStrategy"/> when the object is being torn down.
		/// </summary>
		void OnTearingDown();
	}
}
